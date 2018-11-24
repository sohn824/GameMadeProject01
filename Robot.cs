using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    [SerializeField]
    private GameObject rocketLauncher;
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private Transform leftBulletTf;
    [SerializeField]
    private Transform rightBulletTf;
    private GameObject player;
    private Animator robotAnimator;
    private SpriteRenderer robotSprite;
    private Rigidbody2D robotRigid;
    [HideInInspector]
    public int RobotHP = 10;
    private bool isNewState = false;
    private Vector3 velocity = Vector3.zero;
    private float speed = 1.5f;
    private float jumpPower = 6.0f;
    private bool isFind = false;
    [HideInInspector]
    public bool isJumping = false;
    public enum RobotState
    {
        Idle,
        Trace,
        Jump,
        Shoot,
        Die
    }
    public RobotState robotState;
    private void OnEnable()
    {
        StartCoroutine("FSMMain");
    }
    void Start ()
    {
        player = GameObject.Find("Player");
        robotAnimator = GetComponent<Animator>();
        robotSprite = GetComponent<SpriteRenderer>();
        robotRigid = GetComponent<Rigidbody2D>();
	}	
	void Update ()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 18 && isFind == false)
        {
            isFind = true;
            SetState(RobotState.Trace);
        }
        if(Mathf.Abs(transform.position.x - player.transform.position.x) < 0.15f) //x좌표가 겹치면 멈추게 하기 (y축이 다른데 위에서 겹치는경우)
        {
            isFind = false;
            SetState(RobotState.Idle);
        }
        if(RobotHP <= 0)
        {
            SetState(RobotState.Die);
        }
        if(transform.position.y < -2.0f)
        {
            isJumping = true;
            SetState(RobotState.Jump);
        }
	}
    public void SetState(RobotState newState)
    {
        isNewState = true;
        robotState = newState;
    }
    IEnumerator FSMMain()
    {
        while (true)
        {
            isNewState = false;
            yield return StartCoroutine(robotState.ToString());
        }
    }
    IEnumerator Idle()
    {
        do
        {
            yield return null;
            if (isNewState)
            {
                break;
            }
        } while (!isNewState);
    }
    IEnumerator Trace()
    {
        do
        {
            transform.position += velocity * speed * Time.deltaTime;
            if (player.transform.position.x < transform.position.x)
            {
                velocity = Vector3.left;
                robotSprite.flipX = true;
            }
            else
            {
                velocity = Vector3.right;
                robotSprite.flipX = false;
            }
            yield return null;
        } while (!isNewState);
    }
    IEnumerator Jump()
    {
        do
        {
            if(!isJumping)
            {
                robotRigid.AddForce(Vector3.up * jumpPower, ForceMode2D.Impulse);
                isJumping = true;
                SetState(RobotState.Trace);
            }
            yield return new WaitForSeconds(0.1f);
        } while (!isNewState);
        isJumping = false;
    }
    IEnumerator Shoot()
    {
        robotAnimator.SetBool("isShoot", true);
        do
        {
            yield return null;
        } while (!isNewState);
        robotAnimator.SetBool("isShoot", false);
    }
    IEnumerator Die()
    {
        robotAnimator.SetBool("isDie", true);
        do
        {
            yield return null;
        } while (!isNewState);
    }
    public void RobotShoot() //Shoot 애니메이션 이벤트용
    {
        if (robotSprite.flipX == true)
        {
            Instantiate(bullet, leftBulletTf.position, Quaternion.identity, gameObject.transform);
        }
        else
        {
            Instantiate(bullet, rightBulletTf.position, Quaternion.identity, gameObject.transform);
        }
    }
    public void DestroySelf()
    {
        Destroy(gameObject);
        ScoreManager.instance.AddScore(2000);
        Instantiate(rocketLauncher, new Vector3(transform.position.x, transform.position.y - 0.3f, transform.position.z), Quaternion.identity);
    }
}
