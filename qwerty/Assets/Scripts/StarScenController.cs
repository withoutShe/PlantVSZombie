using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StarScenController : MonoBehaviour
{
    public void OnStartButtonClick()
    {
        SceneManager.LoadScene("MenuScene");
        AudioManager.Instance.PlayClip(Config.btn_click);
    }
}
