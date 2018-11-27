using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void OnClickRestartButton()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void OnClickMainMenuButton()
    {
        SceneManager.LoadScene("StartScene");
    }
}
