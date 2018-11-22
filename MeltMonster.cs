using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeltMonster : MonoBehaviour
{
    [SerializeField]
    private Transform leftSpawnTf;
    [SerializeField]
    private Transform rightSpawnTf;
    [SerializeField]
    private GameObject dirtyBubble;
    GameObject player;
    Animator meltMonsterAnimator;
    SpriteRenderer meltMonsterSprite;
    private bool isNewState = false;
    private int currentLife = 10; //extraLife 하나당 체력
    private int extraLife = 3; //이게 0이되면 완전히 죽음
    public enum MeltMonsterState
    {
        Idle,
        Attack,
        Die
    }
    public MeltMonsterState meltMonsterState;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            if (player.GetComponent<Player>().currentBullet == Player.CurrentBullet.Default)
            {
                currentLife--;
            }
            else if (player.GetComponent<Player>().currentBullet == Player.CurrentBullet.RocketLauncher)
            {
                currentLife -= 2;
            }
            else if (player.GetComponent<Player>().currentBullet == Player.CurrentBullet.FlameShot)
            {
                currentLife -= 3;
            }
            Debug.Log(currentLife);
        }
    }
    private void OnEnable()
    {
        StartCoroutine("FSMMain");
    }
    public void SetState(MeltMonsterState newState)
    {
        isNewState = true;
        meltMonsterState = newState;
    }
    void Start ()
    {
        meltMonsterAnimator = GetComponent<Animator>();
        meltMonsterSprite = GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player");
	}
	void Update ()
    {
		if(transform.position.x < player.transform.position.x)
        {
            meltMonsterSprite.flipX = true;
        }
        else
        {
            meltMonsterSprite.flipX = false;
        }
        if(currentLife <= 0)
        {
            SetState(MeltMonsterState.Die);
        }
	}
    IEnumerator FSMMain()
    {
        while (true)
        {
            isNewState = false;
            yield return StartCoroutine(meltMonsterState.ToString());
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
    IEnumerator Attack()
    {
        meltMonsterAnimator.SetBool("isAttack", true); //Animation Event로 공격
        do
        {
            yield return null;
            if (isNewState)
            {
                break;
            }
        } while (!isNewState);
        meltMonsterAnimator.SetBool("isAttack", false);
    }
    IEnumerator Die()
    {
        meltMonsterAnimator.SetBool("isDie", true);
        do
        {
            yield return null;
            if (isNewState)
            {
                break;
            }
        } while (!isNewState);
        meltMonsterAnimator.SetBool("isDie", false);
    }

    public void MeltMonsterAttack() //Attack Animation Event
    {
        if (player.transform.position.x < transform.position.x)
        {
            Instantiate(dirtyBubble, leftSpawnTf.position, Quaternion.identity, gameObject.transform);
        }
        else
        {
            Instantiate(dirtyBubble, rightSpawnTf.position, Quaternion.identity, gameObject.transform);
        }
    }

    public void MeltMonsterDie() //Die Animation Event
    {

    }
}
