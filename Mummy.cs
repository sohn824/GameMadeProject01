using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mummy : MonoBehaviour
{
    private Transform targetTf;
    private SpriteRenderer mummySprite;
    private Animator mummyAnimator;
    private int mummyLife = 5;
    private float enemySpeed = 1.5f;
    private bool isNewState = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Bullet")
        {
            mummyLife--;
        }
    }
    public enum EnemyState
    {
        Idle,
        Walk
    }
    [HideInInspector]
    public EnemyState enemyState;
    private void OnEnable()
    {
        StartCoroutine("FSMMain");  
    }

    // Use this for initialization
    void Start ()
    {
        targetTf = GameObject.Find("Player").GetComponent<Transform>();
        mummySprite = GetComponent<SpriteRenderer>();
        mummyAnimator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(mummyLife <= 0)
        {
            Destroy(gameObject);
        }

    }

    public void SetState(EnemyState newState)
    {
        isNewState = true;
        enemyState = newState;
    }

    IEnumerator FSMMain()
    {
        while(true)
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
            if(isNewState)
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
            if(isNewState)
            {
                break;
            }

        } while (!isNewState);
        mummyAnimator.SetBool("isWalk", false);
    }
}
