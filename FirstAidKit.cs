using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstAidKit : MonoBehaviour
{
    GameObject player;
	// Use this for initialization
	void Start ()
    {
        player = GameObject.Find("Player");
        if(SceneManager.GetActiveScene().buildIndex != 1)
        {
            Destroy(gameObject);
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            player.GetComponent<Player>().playerHP += 3;
            if(player.GetComponent<Player>().playerHP > 10)
            {
                player.GetComponent<Player>().playerHP = 10;
            }
            Destroy(gameObject);
        }
    }
}
