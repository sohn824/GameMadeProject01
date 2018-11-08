using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Animator bulletAnimator;
    private SpriteRenderer playerSprite;
    private SpriteRenderer bulletSprite;
    private Vector3 moveVelocity;
    private float speed = 8.0f;
    GameObject player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            if (player.GetComponent<Player>().currentBullet == Player.CurrentBullet.Default)
            {
                Invoke("DestroySelf", 0.1f); //기본 총알일때 Enemy의 OnTriggerExit이 발동하기 전에 사라져서 체크가 안되길래 조금 늦게 해줌
                //DestroySelf();
            }
            else if(player.GetComponent<Player>().currentBullet == Player.CurrentBullet.RocketLauncher)
            {
                explosion();
            }
            else if(player.GetComponent<Player>().currentBullet == Player.CurrentBullet.FlameShot)
            {
                //나오자마자 애니메이션 재생되기 때문에 할 거 없음
            }
        }
        if(collision.tag == "Ground")
        {
            if(player.GetComponent<Player>().currentBullet == Player.CurrentBullet.Default)
            {
                Destroy(gameObject);
            }
            else if(player.GetComponent<Player>().currentBullet == Player.CurrentBullet.RocketLauncher)
            {
                explosion();
            }
        }
    }
    // Use this for initialization
    void Start ()
    {
        player = GameObject.Find("Player");
        playerSprite = player.GetComponent<SpriteRenderer>();
        bulletSprite = GetComponent<SpriteRenderer>();
        bulletAnimator = GetComponent<Animator>();
        if (playerSprite.flipX == false)
        {
            moveVelocity = Vector3.left;
            bulletSprite.flipX = true;
        }
        else
        {
            moveVelocity = Vector3.right;
            bulletSprite.flipX = false;
        }
        Invoke("DestroySelf", 2.5f);
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position += moveVelocity * speed * Time.deltaTime;
    }

    void explosion()
    {
        bulletAnimator.SetBool("isExplosion", true);
    }

    public void DestroySelf() //Bullet들의 애니메이션 이벤트로도 사용
    {
        Destroy(gameObject);
    }
}
