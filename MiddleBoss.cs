using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleBoss : MonoBehaviour
{
    public SpriteRenderer MiddleBossSprite;
    private Animator middleBossAnimator;
    private int middleBossLife = 50;
    private float chargeSpeed = 10.0f;
    private bool isNewState = false;
    private GameObject player;
    private Transform targetTf;
    public Vector3 Velocity;
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
        MiddleBossSprite = GetComponent<SpriteRenderer>();
        middleBossAnimator = GetComponent<Animator>();
        Velocity = Vector3.zero;
	}
	void Update ()
    {
		if(middleBossLife <= 0)
        {

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
    public void MiddleBossDeath() //죽는 애니메이션 이벤트용
    {
        ScoreManager.instance.AddScore(5000);
        Destroy(gameObject);
    }
}
