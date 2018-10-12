using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    GameObject player;
    Vector3 offset;
	// Use this for initialization
	void Start ()
    {
        player = GameObject.Find("Player");
        offset = transform.position - player.transform.position;
	}

    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}
