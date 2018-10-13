using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mummy : MonoBehaviour
{
    [SerializeField]
    private GameObject AttackEffect;
    [SerializeField]
    private Transform leftAttackTf;
    [SerializeField]
    private Transform rightAttackTf;
    private GameObject newAttackEffect;
    private Transform targetTf;
    private SpriteRenderer mummySprite;
    private Animator mummyAnimator;
    private int mummyLife = 5;
    private float enemySpeed = 1.5f;
    private bool isNewState = false;
    GameObject player;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            if (player.GetComponent<Player>().currentBullet == Player.CurrentBullet.Default)
            {
                mummyLife--;
            }
            else if (player.GetComponent<Player>().currentBullet == Player.CurrentBullet.RocketLauncher)
            {
                mummyLife -= 2;
            }
        }
    }
    public enum EnemyState
    {
        Idle,
        Walk,
        Attack,
        Die
    }
    [HideInInspector]
    public EnemyState enemyState;
    private void OnEnable()
    {
        StartCoroutine("FSMMain");
    }

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player");
        targetTf = player.GetComponent<Transform>();
        mummySprite = GetComponent<SpriteRenderer>();
        mummyAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (mummyLife <= 0)
        {
            SetState(EnemyState.Die);
        }
    }

    public void SetState(EnemyState newState)
    {
        isNewState = true;
        enemyState = newState;
    }

    IEnumerator FSMMain()
    {
        while (true)
        {
            isNewState = false;
            yield return StartCoroutine(enemyState.ToString());
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

    IEnumerator Walk()
    {
        mummyAnimator.SetBool("isWalk", true);
        do
        {
            Vector3 velocity = Vector3.zero;

            if (targetTf.position.x < transform.position.x)
            {
                mummySprite.flipX = true;
                velocity = Vector3.left;
                transform.localScale = new Vector3(3, 3, 1);
            }
            else
            {
                mummySprite.flipX = false;
                velocity = Vector3.right;
                transform.localScale = new Vector3(3, 3, 1);
            }
            transform.position += velocity * enemySpeed * Time.deltaTime;
            yield return null;
            if (isNewState)
            {
                break;
            }

        } while (!isNewState);
        mummyAnimator.SetBool("isWalk", false);
    }

    IEnumerator Attack()
    {
        mummyAnimator.SetBool("isAttack", true);
        do
        {
            if (targetTf.position.x < transform.position.x)
            {
                mummySprite.flipX = false; //이거 스프라이트가 공격만 반대쪽으로 되어 있어서 flipX를 공격때는 반대로 시켰음
            }
            else
            {
                mummySprite.flipX = true;
            }
            yield return null;
        } while (!isNewState);
        mummyAnimator.SetBool("isAttack", false);
    }

    IEnumerator Die()
    {
        mummyAnimator.SetBool("isDie", true);
        do
        {
            yield return null;
        } while (!isNewState);
    }

    public void DestroySelf() //죽는 애니메이션 이벤트용
    {
        ScoreManager.instance.AddScore(500);
        Destroy(gameObject);
    }

    public void InstantiateAttackEffect() //공격 이펙트 (애니메이션 이벤트)
    {
        if (targetTf.position.x < transform.position.x)
        {
            newAttackEffect = Instantiate(AttackEffect, leftAttackTf.position, Quaternion.identity);
        }
        else
        {
            newAttackEffect = Instantiate(AttackEffect, rightAttackTf.position, Quaternion.identity);
        }
        newAttackEffect.transform.parent = gameObject.transform;
    }
}