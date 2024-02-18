using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
   public  GameObject ChooseName;
    public GameObject StartGameToHide;
    public TMP_InputField ChangeName;
    public TextMeshProUGUI ShowName;
    public string UserName = "GOD_JIE";
    public Button Quit_button;
    public Button Cancel_button;

    public GameObject ShowTheHelp;
    public void chooseName()
    {
        AudioManager.Instance.PlayClip(Config.btn_click);
        ChooseName.SetActive(true);
        StartGameToHide.SetActive(false);
    }
    public void CancelChooseName()
    {
        AudioManager.Instance.PlayClip(Config.btn_click);
        ChooseName.SetActive(false);
        StartGameToHide.SetActive(true);
    }
    public void ChangeUserName()
    {
        AudioManager.Instance.PlayClip(Config.btn_click);
        UserName = ChangeName.text;
        ShowName.text = UserName;
        ChooseName.SetActive(false); //点击确定之后关闭
        StartGameToHide.SetActive(true);
    }
    public void OnStartButtonClick()
    {
        AudioManager.Instance.PlayClip(Config.btn_click);
        SceneManager.LoadScene("GameScene");
    }
    public void OnChooseQuit()
    {
        AudioManager.Instance.PlayClip(Config.btn_click);
        Quit_button.gameObject.SetActive(true);
        Cancel_button.gameObject.SetActive(true);
    }
   public void OnQuitButtonClick()
    {
        AudioManager.Instance.PlayClip(Config.btn_click);
        Application.Quit();
    }
    public void OnCancelButtonClick()
    {
        AudioManager.Instance.PlayClip(Config.btn_click);
        Quit_button.gameObject.SetActive(false);
        Cancel_button.gameObject.SetActive(false);
    }
    
    public void showthehelp()
    {
        AudioManager.Instance.PlayClip(Config.btn_click);
        ShowTheHelp.gameObject.SetActive(true);
    }
}
