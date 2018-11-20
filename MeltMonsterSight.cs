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
        }
    }
    // Use this for initialization
    void Start ()
    {
        meltMonsterAnimator = transform.parent.gameObject.GetComponent<Animator>();
	}
	
}
