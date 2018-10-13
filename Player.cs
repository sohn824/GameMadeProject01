using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [HideInInspector]
    public SpriteRenderer playerSprite;
    Rigidbody2D playerRigid;
    Animator playerAnimator;
    BoxCollider2D playerCollider;
    BoxCollider2D crouchCollider;
    private float speed = 3.0f;
    private float crouchSpeed = 1.5f;
    private Vector3 movement;
    private Vector3 moveVelocity;
    private bool isJump = false;
    private bool isCrouch = false;
    private float jumpPower = 6.0f;
    [HideInInspector]
    public int playerHP = 5;
    [HideInInspector]
    public int playerHPMax = 5;
    [SerializeField]
    private GameObject[] bullet;
    [SerializeField]
    private Transform leftBulletTf;
    [SerializeField]
    private Transform rightBulletTf;
    [SerializeField]
    private Transform leftCrouchBulletTf;
    [SerializeField]
    private Transform rightCrouchBulletTf;

    public enum CurrentBullet
    {
        Default = 0,
        RocketLauncher = 1
    }

    public CurrentBullet currentBullet;

	// Use this for initialization
	void Start ()
    {
        playerSprite = GetComponent<SpriteRenderer>();
        playerRigid = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerCollider = GetComponent<BoxCollider2D>();
        crouchCollider = GameObject.Find("CrouchCollider").GetComponent<BoxCollider2D>();
        currentBullet = CurrentBullet.Default;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(playerHP <= 0)
        {
            playerAnimator.SetBool("isDie", true);
        }
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            playerAnimator.SetBool("isJump", false);
            isJump = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            playerAnimator.SetBool("isDamaged", true);
            playerHP--;
        }
    }

    private void FixedUpdate()
    {
        process();
    }

    private void process()
    {
        moveVelocity = Vector3.zero;
        if(moveVelocity == Vector3.zero)
        {
            playerAnimator.SetBool("isWalk", false);
        }
        if(Input.GetKey(KeyCode.A))
        {
            playerSprite.flipX = false;
            playerAnimator.SetBool("isWalk", true);
            moveVelocity = Vector3.left;
        }
        if(Input.GetKey(KeyCode.D))
        {
            playerSprite.flipX = true;
            playerAnimator.SetBool("isWalk", true);
            moveVelocity = Vector3.right;
        }
        if(Input.GetKey(KeyCode.S))
        {
            playerAnimator.SetBool("isCrouch", true);
            isCrouch = true;
            moveVelocity = Vector3.zero;
            playerCollider.enabled = false;
            crouchCollider.enabled = true;
            if (Input.GetKey(KeyCode.A))
            {
                playerSprite.flipX = false;
                playerAnimator.SetBool("isWalk", true);
                moveVelocity = Vector3.left;
            }
            if (Input.GetKey(KeyCode.D))
            {
                playerSprite.flipX = true;
                playerAnimator.SetBool("isWalk", true);
                moveVelocity = Vector3.right;
            }
        }
        if(Input.GetKeyUp(KeyCode.S))
        {
            playerAnimator.SetBool("isCrouch", false);
            isCrouch = false;
            playerCollider.enabled = true;
            crouchCollider.enabled = false;
        }
        if(Input.GetKey(KeyCode.W))
        {
            playerAnimator.SetBool("isUpside", true);
        }
        if(Input.GetKeyUp(KeyCode.W))
        {
            playerAnimator.SetBool("isUpside", false);
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (isJump == false)
            {
                playerAnimator.SetBool("isJump", true);
                isJump = true;
                playerRigid.AddForce(Vector3.up * jumpPower, ForceMode2D.Impulse);
            }
        }
        if(Input.GetKeyDown(KeyCode.J))
        {
            playerAnimator.SetBool("isShoot", true);
        }
        if (!isCrouch)
        {
            transform.position += moveVelocity * speed * Time.deltaTime;
        }
        else
        {
            transform.position += moveVelocity * crouchSpeed * Time.deltaTime;
        }
    }

    public void ShootBullet() //Animation Event용 함수
    {
        if(playerSprite.flipX == true)
        {
            if (!isCrouch)
            {
                Instantiate(bullet[(int)currentBullet], rightBulletTf.position, Quaternion.identity);
            }
            else
            {
                Instantiate(bullet[(int)currentBullet], rightCrouchBulletTf.position, Quaternion.identity);
            }
        }
        else
        {
            if(!isCrouch)
            {
                Instantiate(bullet[(int)currentBullet], leftBulletTf.position, Quaternion.identity);
            }
            else
            {
                Instantiate(bullet[(int)currentBullet], leftCrouchBulletTf.position, Quaternion.identity);
            }
        }
    }
    public void SetPlayerIdle() //Animation Event용 함수
    {
        playerAnimator.SetBool("isDamaged", false);
        playerAnimator.SetBool("isShoot", false);
    }

    public void FlipSprite() //죽는 애니메이션 스프라이트가 반대로 되어있어서 죽는 애니메이션 처음에 호출해서 뒤집으려고 만든 함수
    {
        playerSprite.flipX = !playerSprite.flipX;
    }
    public void GoToDeathScene() //죽는 애니메이션 끝나면 데스 씬 가는 이벤트
    {
        SceneManager.LoadScene("DeathScene");
    }
}
