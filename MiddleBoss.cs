using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleBoss : MonoBehaviour
{
    [SerializeField]
    private GameObject[] enemyLazer;
    [SerializeField]
    private GameObject enemyBomb;
    [SerializeField]
    private GameObject enemyColumn;
    [SerializeField]
    private Transform leftShootTf;
    [SerializeField]
    private Transform rightShootTf;
    private Animator middleBossAnimator;
    private int middleBossLife = 30;
    private float chargeSpeed = 8.0f;
    private bool isNewState = false;
    private GameObject player;
    private Transform targetTf;
    [HideInInspector]
    public SpriteRenderer MiddleBossSprite;
    public Vector3 Velocity;
    int beforeSeed = -1;
    int currentSeed = 0;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            if (player.GetComponent<Player>().currentBullet == Player.CurrentBullet.Default)
            {
                middleBossLife--;
            }
            else if (player.GetComponent<Player>().currentBullet == Player.CurrentBullet.RocketLauncher)
            {
                middleBossLife -= 2;
            }
        }
    }
    public enum MiddleBossState
    {
        Idle,
        Charge,
        Shoot,
        Smash,
        Stab,
        Die
    }
    public MiddleBossState middleBossState;
    private void OnEnable()
    {
        StartCoroutine("FSMMain");
    }
    void Start ()
    {
        player = GameObject.Find("Player");
        targetTf = player.GetComponent<Transform>();
        middleBossAnimator = GetComponent<Animator>();
        MiddleBossSprite = GetComponent<SpriteRenderer>();
        Velocity = Vector3.zero;
	}
	void Update ()
    {
		if(middleBossLife <= 0)
        {
            SetState(MiddleBossState.Die);
        }
	}
    public void SetState(MiddleBossState newState)
    {
        isNewState = true;
        middleBossState = newState;
    }

    IEnumerator FSMMain()
    {
        while (true)
        {
            isNewState = false;
            yield return StartCoroutine(middleBossState.ToString());
        }
    }

    IEnumerator Idle()
    {
        //상태 진입 동작
        do
        {
            //상태 중 동작
            yield return null;
            if (isNewState)
            {
                break;
            }
        } while (!isNewState);
        //상태 종료 후 종작
    }

    IEnumerator Charge()
    {
        middleBossAnimator.SetBool("isCharge", true);
        do
        {
            transform.position += Velocity * chargeSpeed * Time.deltaTime;
            yield return null;
            if (isNewState)
            {
                break;
            }
        } while (!isNewState);
        middleBossAnimator.SetBool("isCharge", false);
    }

    IEnumerator Shoot()
    {
        middleBossAnimator.SetBool("isShoot", true);
        do
        {
            if (targetTf.position.x < transform.position.x)
            {
                MiddleBossSprite.flipX = true;
            }
            else
            {
                MiddleBossSprite.flipX = false;
            }
            yield return new WaitForSeconds(0.5f);
        } while (!isNewState);
        middleBossAnimator.SetBool("isShoot", false);
    }

    IEnumerator Smash()
    {
        middleBossAnimator.SetBool("isSmash", true);
        do
        {
            if (targetTf.position.x < transform.position.x)
            {
                MiddleBossSprite.flipX = true;
                for (int i = 0; i < 5; i++)
                {
                    Instantiate(enemyColumn, new Vector3(leftShootTf.position.x - i, leftShootTf.position.y + 0.9f, leftShootTf.position.z), Quaternion.identity, gameObject.transform);
                    yield return new WaitForSeconds(0.5f);
                }
            }
            else
            {
                MiddleBossSprite.flipX = false;
                for (int i = 0; i < 5; i++)
                {
                    Instantiate(enemyColumn, new Vector3(rightShootTf.position.x + i, rightShootTf.position.y + 0.9f, leftShootTf.position.z), Quaternion.identity, gameObject.transform);
                    yield return new WaitForSeconds(0.5f);
                }
            }
        } while (!isNewState);
        middleBossAnimator.SetBool("isSmash", false);
    }

    IEnumerator Stab()
    {
        middleBossAnimator.SetBool("isStab", true);
        do
        {
            if (targetTf.position.x < transform.position.x)
            {
                MiddleBossSprite.flipX = true;
            }
            else
            {
                MiddleBossSprite.flipX = false;
            }
            yield return null;
        } while (!isNewState);
        middleBossAnimator.SetBool("isStab", false);
    }

    IEnumerator Die()
    {
        middleBossAnimator.SetBool("isDie", true);
        do
        {
            yield return null;
        } while (!isNewState);
    }

    public void MiddleBossRandomShoot() //Shoot 애니메이션 이벤트용
    {
        while (true)
        {
            currentSeed = Random.Range(0, 3); //반환값에 max 미포함
            if(currentSeed != beforeSeed)
            {
                break;
            }
        }
        switch (currentSeed)
        {
            case 0:
                if (targetTf.position.x < transform.position.x)
                {
                    Instantiate(enemyLazer[0], leftShootTf.position, Quaternion.identity, gameObject.transform);
                }
                else
                {
                    Instantiate(enemyLazer[0], rightShootTf.position, Quaternion.identity, gameObject.transform);
                }
                break;
            case 1:
                if (targetTf.position.x < transform.position.x)
                {
                    Instantiate(enemyLazer[1], new Vector3(leftShootTf.position.x, leftShootTf.position.y + 1.5f, leftShootTf.position.z), Quaternion.identity, gameObject.transform);
                    Instantiate(enemyLazer[1], leftShootTf.position, Quaternion.identity, gameObject.transform);
                    Instantiate(enemyLazer[1], new Vector3(leftShootTf.position.x, leftShootTf.position.y - 1.5f, leftShootTf.position.z), Quaternion.identity, gameObject.transform);
                }
                else
                {
                    Instantiate(enemyLazer[1], new Vector3(rightShootTf.position.x, leftShootTf.position.y + 1.5f, leftShootTf.position.z), Quaternion.identity, gameObject.transform);
                    Instantiate(enemyLazer[1], rightShootTf.position, Quaternion.identity, gameObject.transform);
                    Instantiate(enemyLazer[1], new Vector3(rightShootTf.position.x, leftShootTf.position.y - 1.5f, leftShootTf.position.z), Quaternion.identity, gameObject.transform);
                }
                break;
            case 2:
                if (targetTf.position.x < transform.position.x)
                {
                    Instantiate(enemyBomb, new Vector3(leftShootTf.position.x - 1.0f, leftShootTf.position.y - 0.6f, leftShootTf.position.z), Quaternion.identity);
                }
                else
                {
                    Instantiate(enemyBomb, new Vector3(leftShootTf.position.x + 1.0f, leftShootTf.position.y - 0.6f, leftShootTf.position.z), Quaternion.identity);
                }
                break;
        }
        beforeSeed = currentSeed;

    }
    public void MiddleBossDeath() //죽는 애니메이션 이벤트용
    {
        ScoreManager.instance.AddScore(5000);
        Destroy(gameObject);
    }
}
