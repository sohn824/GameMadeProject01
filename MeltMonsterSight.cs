using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeltMonsterSight : MonoBehaviour
{
    Animator meltMonsterAnimator;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            meltMonsterAnimator.enabled = true;
            transform.parent.GetComponent<MeltMonster>().SetState(MeltMonster.MeltMonsterState.Attack);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            transform.parent.GetComponent<MeltMonster>().SetState(MeltMonster.MeltMonsterState.Idle);
        }
    }
    // Use this for initialization
    void Start ()
    {
        meltMonsterAnimator = transform.parent.gameObject.GetComponent<Animator>();
	}
	
}
