using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    Player player;
    Slider hpBar;
	// Use this for initialization
	void Start ()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        hpBar = GameObject.Find("HPBar").GetComponent<Slider>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        hpBar.value = (float)player.playerHP / (float)player.playerHPMax;
	}
}
