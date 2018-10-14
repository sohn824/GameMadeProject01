using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    Text scoreText;
    int score = 0;
    void Awake()
    {
        if(!instance)
        {
            instance = this; //다른 곳에서도 쉽게 호출할 수 있도록
        }
    }
    // Use this for initialization
    void Start ()
    {
        scoreText = GameObject.Find("Score").GetComponent<Text>();
        scoreText.text += score;
	}

    public void AddScore(int num)
    {
        score += num;
        scoreText.text = "SCORE : " + score.ToString();
    }

}
