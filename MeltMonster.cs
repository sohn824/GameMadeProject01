using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeltMonster : MonoBehaviour
{
    [SerializeField]
    private Transform leftSpawnTf;
    [SerializeField]
    private Transform rightSpawnTf;
    GameObject player;
    Animator meltMonsterAnimator;
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
        player = GameObject.Find("Player");
	}
	void Update ()
    {
		
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
        meltMonsterAnimator.SetBool("isAttack", true);
        do
        {

            yield return new WaitForSeconds(1.0f);
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

}
