using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletManager : MonoBehaviour
{
    public static BulletManager instance;
    private Image currentImage; 
    [SerializeField]
    private Sprite[] bulletSprites;
    private Text remainAmmoText;
    private string remainAmmoString;
    public float RemainAmmo;
    GameObject player;
    void Awake()
    {
        if (!instance)
        {
            instance = this; //다른 곳에서도 쉽게 호출할 수 있도록
        }
    }
    void Start()
    {
        player = GameObject.Find("Player");
        currentImage = GameObject.Find("CurrentBullet").GetComponent<Image>();
        remainAmmoText = GameObject.Find("RemainAmmo").GetComponent<Text>();
        remainAmmoString = "INFINITY";
        RemainAmmo = Mathf.Infinity;
    }
    void Update()
    {
        remainAmmoString = RemainAmmo.ToString();
        remainAmmoText.text = "AMMO : " + remainAmmoString;
        if(RemainAmmo == 0)
        {
            player.GetComponent<Player>().currentBullet = Player.CurrentBullet.Default;
            ChangeBulletImage();
            BulletInitialize();
        }
    }
    public void BulletInitialize()
    {
        RemainAmmo = Mathf.Infinity;
        remainAmmoString = "INFINITY";
    }

    public void ChangeBulletImage()
    {
        currentImage.sprite = bulletSprites[(int)GameObject.Find("Player").GetComponent<Player>().currentBullet];
    }

    public void GetRocketLauncher() //UI 업데이트용
    {
        RemainAmmo = 10;
        remainAmmoString = RemainAmmo.ToString();
        remainAmmoText.text = "AMMO : " + remainAmmoString;
    }

    public void GetFlameShot()
    {
        RemainAmmo = 5;
        remainAmmoString = RemainAmmo.ToString();
        remainAmmoText.text = "AMMO : " + remainAmmoString;
    }

}
