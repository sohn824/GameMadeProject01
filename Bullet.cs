using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Animator bulletAnimator;
    private SpriteRenderer playerSprite;
    private Vector3 moveVelocity;
    private float speed = 8.0f;
    GameObject player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            if (GameObject.Find("Player").GetComponent<Player>().currentBullet == Player.CurrentBullet.Default)
            {
                DestroySelf();
            }
            else if(GameObject.Find("Player").GetComponent<Player>().currentBullet == Player.CurrentBullet.RocketLauncher)
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
        bulletAnimator = GetComponent<Animator>();
        if (playerSprite.flipX == false)
        {
            moveVelocity = Vector3.left;
        }
        else
        {
            moveVelocity = Vector3.right;
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

    public void DestroySelf() //explosion의 애니메이션 이벤트로도 사용
    {
        Destroy(gameObject);
    }
}
