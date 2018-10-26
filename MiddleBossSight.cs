﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleBossSight : MonoBehaviour
{
    private Transform targetTf;
    private int currentSeed = 1;
    private int beforeSeed = 0;
    //private IEnumerator randomCoroutine;
    //private bool coroutineIsRunning = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            randomPattern();
            if(gameObject.transform.parent.GetComponent<MiddleBoss>().middleBossState == MiddleBoss.MiddleBossState.Charge) //Charge가 걸렸을 경우 방향 정해주는 처리문
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

    void randomPattern()
    {
        while (true)
        {
            currentSeed = Random.Range(1, 5);
            if (currentSeed != beforeSeed)
            {
                break;
            }
        }
        switch(currentSeed)
        {
            case 1:
                gameObject.transform.parent.GetComponent<MiddleBoss>().SetState(MiddleBoss.MiddleBossState.Charge);
                break;
            case 2:
                gameObject.transform.parent.GetComponent<MiddleBoss>().SetState(MiddleBoss.MiddleBossState.Shoot);
                break;
            case 3:
                gameObject.transform.parent.GetComponent<MiddleBoss>().SetState(MiddleBoss.MiddleBossState.Smash);
                break;
            case 4:
                gameObject.transform.parent.GetComponent<MiddleBoss>().SetState(MiddleBoss.MiddleBossState.Stab);
                break;
        }
        beforeSeed = currentSeed;
    }

    
}
