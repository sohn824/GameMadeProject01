using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RocketLauncher : MonoBehaviour
{
    GameObject player;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            player.GetComponent<Player>().currentBullet = Player.CurrentBullet.RocketLauncher;
            BulletManager.instance.ChangeBulletImage();
            BulletManager.instance.GetRocketLauncher();
            Destroy(gameObject);
        }
    }
    void Start()
    {
        player = GameObject.Find("Player");
        if (SceneManager.GetActiveScene().buildIndex != 1)
        {
            Destroy(gameObject);
        }
    }
}
