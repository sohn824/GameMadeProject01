using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleBoss : MonoBehaviour
{
    [SerializeField]
    private GameObject[] enemyLazer;
    [SerializeField]
    private GameObject enemyBomb;
    [SerializeField]
    private GameObject enemyColumn;
    [SerializeField]
    private GameObject enemyShield;
    [SerializeField]
    private Transform leftShootTf;
    [SerializeField]
    private Transform rightShootTf;
    [SerializeField]
    private GameObject explosionEffect;
    [SerializeField]
    private GameObject swordSlashEffect;
    private Animator middleBossAnimator;
    private float chargeSpeed = 8.0f;
    private bool isNewState = false;
    private GameObject player;
    private Transform targetTf;
    [HideInInspector]
    public SpriteRenderer MiddleBossSprite;
    [HideInInspector]
    public bool IsAngry = false; //이건 광폭화 시 무적 체크용
    private bool isNotAngry = true; //이건 광폭화 진입 시 한번만 호출되게 하기용
    public Vector3 Velocity;
    [HideInInspector]
    public int MiddleBossHP = 50;
    [HideInInspector]
    public int MiddleBossHPMax = 50;
    int beforeSeed = -1;
    int currentSeed = 0;
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Bullet" && IsAngry == false)
        {
            if (player.GetComponent<Player>().currentBullet == Player.CurrentBullet.Default)
            {
                MiddleBossHP--;
            }
            else if (player.GetComponent<Player>().currentBullet == Player.CurrentBullet.RocketLauncher)
            {
                MiddleBossHP -= 2;
            }
            else if (player.GetComponent<Player>().currentBullet == Player.CurrentBullet.FlameShot)
            {
                MiddleBossHP -= 3;
            }
            Debug.Log(MiddleBossHP);
        }
    }
    public enum MiddleBossState
    {
        Idle,
        Charge,
        Shoot,
        Smash,
        Stab,
        Gathering,
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
        middleBossAnimator = GetComponent<Animator>();
        MiddleBossSprite = GetComponent<SpriteRenderer>();
        Velocity = Vector3.zero;
	}
	void Update ()
    {
		if(MiddleBossHP <= 0)
        {
            SetState(MiddleBossState.Die);
        }
        if(transform.position.x < player.transform.position.x && IsAngry == false) //Charge로 왼쪽으로 넘어가버리면 다시 오른쪽으로 오게
        {
            if(Vector3.Distance(transform.position, player.transform.position) > 7)
            {
                MiddleBossSprite.flipX = false;
                SetState(MiddleBossState.Charge);
                Velocity = Vector3.right;
            }
        }
        if(MiddleBossHP <= 25 && isNotAngry)
        {
            SetState(MiddleBossState.Gathering);
            isNotAngry = false;
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

    IEnumerator Shoot() //안쓰는 동작
    {
        middleBossAnimator.SetBool("isShoot", true);
        do
        {
            if (targetTf.position.x < transform.position.x)
            {
                MiddleBossSprite.flipX = true;
            }
            else
            {
                MiddleBossSprite.flipX = false;
            }
            yield return new WaitForSeconds(1.0f);
        } while (!isNewState);
        middleBossAnimator.SetBool("isShoot", false);
    }

    IEnumerator Smash() //안쓰는 동작
    {
        middleBossAnimator.SetBool("isSmash", true);
        do
        {
            if (targetTf.position.x < transform.position.x)
            {
                MiddleBossSprite.flipX = true;
                for (int i = 0; i < 5; i++)
                {
                    Instantiate(enemyColumn, new Vector3(leftShootTf.position.x - i, leftShootTf.position.y + 0.9f, leftShootTf.position.z), Quaternion.identity, gameObject.transform);
                    yield return new WaitForSeconds(0.4f);
                }
            }
            else
            {
                MiddleBossSprite.flipX = false;
                for (int i = 0; i < 5; i++)
                {
                    Instantiate(enemyColumn, new Vector3(rightShootTf.position.x + i, rightShootTf.position.y + 0.9f, leftShootTf.position.z), Quaternion.identity, gameObject.transform);
                    yield return new WaitForSeconds(0.4f);
                }
            }
        } while (!isNewState);
        middleBossAnimator.SetBool("isSmash", false);
    }

    IEnumerator Stab()
    {
        middleBossAnimator.SetBool("isStab", true);
        do
        {
            if (targetTf.position.x < transform.position.x)
            {
                MiddleBossSprite.flipX = true;
                Instantiate(swordSlashEffect, new Vector3(leftShootTf.position.x, leftShootTf.position.y +0.45f, leftShootTf.position.z), Quaternion.identity, gameObject.transform);
                yield return new WaitForSeconds(1.0f);
            }
            else
            {
                MiddleBossSprite.flipX = false;
                Instantiate(swordSlashEffect, new Vector3(rightShootTf.position.x, rightShootTf.position.y +0.45f, rightShootTf.position.z), Quaternion.identity, gameObject.transform);
                yield return new WaitForSeconds(1.0f);
            }
        } while (!isNewState);
        middleBossAnimator.SetBool("isStab", false);
    }

    IEnumerator Gathering()
    {
        middleBossAnimator.SetBool("isGathering", true);
        IsAngry = true;
        enemyShield.SetActive(true);
        Invoke("gatheringEnd", 6.0f);
        do
        {
            for(int i=0; i<6; i++)
            {
                if(i%2 ==0)
                {
                    Instantiate(explosionEffect, new Vector3(leftShootTf.position.x - i, leftShootTf.position.y, leftShootTf.position.z), Quaternion.identity, gameObject.transform);
                    Instantiate(explosionEffect, new Vector3(rightShootTf.position.x + i, rightShootTf.position.y, rightShootTf.position.z), Quaternion.identity, gameObject.transform);
                    Instantiate(explosionEffect, new Vector3(transform.position.x, transform.position.y -(i+1), leftShootTf.position.z), Quaternion.identity, gameObject.transform);
                    Instantiate(explosionEffect, new Vector3(transform.position.x, transform.position.y +(i+1), leftShootTf.position.z), Quaternion.identity, gameObject.transform);
                    yield return new WaitForSeconds(0.5f);
                }
                else
                {
                    Instantiate(explosionEffect, new Vector3(leftShootTf.position.x - (i-1), transform.position.y - (i+1), leftShootTf.position.z), Quaternion.identity, gameObject.transform);
                    Instantiate(explosionEffect, new Vector3(rightShootTf.position.x + (i-1), transform.position.y - (i+1), rightShootTf.position.z), Quaternion.identity, gameObject.transform);
                    Instantiate(explosionEffect, new Vector3(leftShootTf.position.x - (i-1), transform.position.y + (i+1), leftShootTf.position.z), Quaternion.identity, gameObject.transform);
                    Instantiate(explosionEffect, new Vector3(rightShootTf.position.x + (i-1), transform.position.y + (i+1), rightShootTf.position.z), Quaternion.identity, gameObject.transform);
                    yield return new WaitForSeconds(0.5f);
                }
            }
        } while (!isNewState);
        middleBossAnimator.SetBool("isGathering", false);
        IsAngry = false;
        enemyShield.SetActive(false);
    }
    IEnumerator Die()
    {
        middleBossAnimator.SetBool("isDie", true);
        do
        {
            yield return null;
        } while (!isNewState);
    }

    public void MiddleBossRandomShoot() //Shoot 애니메이션 이벤트용
    {
        while (true)
        {
            currentSeed = Random.Range(0, 2); //반환값에 max 미포함
            if(currentSeed != beforeSeed)
            {
                break;
            }
        }
        switch (currentSeed)
        {
            case 0:
                if (targetTf.position.x < transform.position.x)
                {
                    Instantiate(enemyLazer[0], leftShootTf.position, Quaternion.identity, gameObject.transform);
                }
                else
                {
                    Instantiate(enemyLazer[0], rightShootTf.position, Quaternion.identity, gameObject.transform);
                }
                break;
            case 1:
                if (targetTf.position.x < transform.position.x)
                {
                    Instantiate(enemyLazer[1], new Vector3(leftShootTf.position.x, leftShootTf.position.y + 1.5f, leftShootTf.position.z), Quaternion.identity, gameObject.transform);
                    Instantiate(enemyLazer[1], leftShootTf.position, Quaternion.identity, gameObject.transform);
                    Instantiate(enemyLazer[1], new Vector3(leftShootTf.position.x, leftShootTf.position.y - 1.5f, leftShootTf.position.z), Quaternion.identity, gameObject.transform);
                }
                else
                {
                    Instantiate(enemyLazer[1], new Vector3(rightShootTf.position.x, leftShootTf.position.y + 1.5f, leftShootTf.position.z), Quaternion.identity, gameObject.transform);
                    Instantiate(enemyLazer[1], rightShootTf.position, Quaternion.identity, gameObject.transform);
                    Instantiate(enemyLazer[1], new Vector3(rightShootTf.position.x, leftShootTf.position.y - 1.5f, leftShootTf.position.z), Quaternion.identity, gameObject.transform);
                }
                break;
            //case 2:
            //    if (targetTf.position.x < transform.position.x)
            //    {
            //        Instantiate(enemyBomb, new Vector3(leftShootTf.position.x - 1.0f, leftShootTf.position.y - 0.6f, leftShootTf.position.z), Quaternion.identity);
            //    }
            //    else
            //    {
            //        Instantiate(enemyBomb, new Vector3(leftShootTf.position.x + 1.0f, leftShootTf.position.y - 0.6f, leftShootTf.position.z), Quaternion.identity);
            //    }
            //    break;
        }
        beforeSeed = currentSeed;

    }
    public void MiddleBossDeath() //죽는 애니메이션 이벤트용
    {
        ScoreManager.instance.AddScore(5000);
        Destroy(gameObject);
    }

    private void gatheringEnd() //광폭화 끝내기 함수
    {
        SetState(MiddleBossState.Idle);
    }
}
