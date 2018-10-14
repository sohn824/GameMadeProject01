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
    void Awake()
    {
        if (!instance)
        {
            instance = this; //다른 곳에서도 쉽게 호출할 수 있도록
        }
    }
    void Start()
    {
        currentImage = GameObject.Find("CurrentBullet").GetComponent<Image>();
    }

    public void ChangeBulletImage()
    {
        currentImage.sprite = bulletSprites[(int)GameObject.Find("Player").GetComponent<Player>().currentBullet];
    }

}
