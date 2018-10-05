using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MummyAttackEffect : MonoBehaviour
{
    private Transform playerTf;
    private SpriteRenderer effectSprite;
    void Awake()
    {
        playerTf = GameObject.Find("Player").GetComponent<Transform>();
        effectSprite = gameObject.GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        if(playerTf.position.x > transform.position.x) //오른쪽을 보고있으면 이펙트를 뒤집어줌 (이펙트가 왼쪽으로 돼있어서)
        {
            effectSprite.flipX = true;
        }
        else
        {
            effectSprite.flipX = false;
        }
    }
    public void DestroySelf() //애니메이션 이벤트
    {
        Destroy(gameObject);
    }

}
