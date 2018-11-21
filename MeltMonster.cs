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
    public enum MeltMonsterState
    {
        Idle,
        Attack,
        Die
    }
    public MeltMonsterState meltMonsterState;
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
        do
        {
            yield return null;
            if (isNewState)
            {
                break;
            }
        } while (!isNewState);
    }

    public void MeltMonsterAttack()
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
}
