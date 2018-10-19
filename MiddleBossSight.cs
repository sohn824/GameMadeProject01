using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleBossSight : MonoBehaviour
{
    private Transform targetTf;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            gameObject.transform.parent.GetComponent<MiddleBoss>().SetState(MiddleBoss.MiddleBossState.Charge);
            if(gameObject.transform.parent.GetComponent<MiddleBoss>().middleBossState == MiddleBoss.MiddleBossState.Charge)
            {
                if(targetTf.position.x < transform.parent.position.x)
                {
                    gameObject.transform.parent.GetComponent<MiddleBoss>().MiddleBossSprite.flipX = true;
                    gameObject.transform.parent.GetComponent<MiddleBoss>().Velocity = Vector3.left;
                    transform.parent.transform.localScale = new Vector3(2, 2, 1);
                }
                else
                {
                    gameObject.transform.parent.GetComponent<MiddleBoss>().MiddleBossSprite.flipX = false;
                    gameObject.transform.parent.GetComponent<MiddleBoss>().Velocity = Vector3.right;
                    transform.parent.transform.localScale = new Vector3(2, 2, 1);
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            gameObject.transform.parent.GetComponent<MiddleBoss>().SetState(MiddleBoss.MiddleBossState.Idle);
        }
    }
    void Start()
    {
        targetTf = GameObject.Find("Player").GetComponent<Transform>();
    }
}
