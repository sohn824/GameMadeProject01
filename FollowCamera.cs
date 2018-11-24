using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    //   GameObject player;
    //   Vector3 offset;
    //// Use this for initialization
    //void Start ()
    //   {
    //       player = GameObject.Find("Player");
    //       offset = transform.position - player.transform.position;
    //}

    //   void LateUpdate()
    //   {
    //       transform.position = player.transform.position + offset;
    //   }
    GameObject player;
    SpriteRenderer playerSprite;
    Vector3 leftCamPosition;
    Vector3 rightCamPosition;

    void Start()
    {
        player = GameObject.Find("Player");
        playerSprite = player.GetComponent<SpriteRenderer>();
    }

    void LateUpdate()
    {
        leftCamPosition = new Vector3(player.transform.position.x - 5f, player.transform.position.y + 1.63f, player.transform.position.z - 10f);
        rightCamPosition = new Vector3(player.transform.position.x + 5f, player.transform.position.y + 1.63f, player.transform.position.z - 10f);
        if(playerSprite.flipX == true)
        {
            transform.position = Vector3.Lerp(transform.position, rightCamPosition, Time.deltaTime * 2.0f);
        }
        else if(playerSprite.flipX == false)
        {
            transform.position = Vector3.Lerp(transform.position, leftCamPosition, Time.deltaTime * 2.0f);
        }
    }
}
