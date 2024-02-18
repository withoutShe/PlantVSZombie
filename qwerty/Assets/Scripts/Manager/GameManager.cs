using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {  get; private set; }
    public float speed = 1f;
    private float step;
    bool formalStart = false;
    public PrepareUI prepareUI;
    bool beginGame = false;
    public FailGameUI failGameUI;
    GameObject[] pauseAllZombie = null;
    GameObject[] pauseAllPlant = null;
    public CardListUI cardListUI;
    public GameSuspend gameSuspend;
    public GameObject WinUI;
    private void Awake()
    {
        Instance = this;
        step = speed* Time.deltaTime;
       failGameUI.gameObject.SetActive(false);
       
    }
    private void Update()
    {
        GameStart();
    }
    void GameStart()
    {
        if (!formalStart)  
        {
            Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, new Vector3(5.5f, 0, -10), step);
            if (Vector3.Distance(Camera.main.transform.position, new Vector3(5.5f, 0, -10)) < 0.001f)
            {
                formalStart = true;
            }
            return;
        }
        //开始让摄像机返回
        Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, new Vector3(0, 0, -10), step);
        if (Vector3.Distance(Camera.main.transform.position, new Vector3(0, 0, -10)) < 0.001f&&!beginGame)
        {
            prepareUI.gameObject.SetActive(true);
            prepareUI.anim.enabled = true;
            beginGame = true;
        }
    }

    public void endOfGame()
    {
        AudioManager.Instance.StopBgm();
        AudioManager.Instance.PlayClip(Config.lose_music);//播放当前关卡结束音乐并结束播放背景音乐
        SuspendOnClick();
        failGameUI.gameObject.SetActive(true);
        failGameUI.show();
        failGameUI.hide();
       
    }

    public void successOfGame()
    {
        AudioManager.Instance.StopBgm();
        AudioManager.Instance.PlayClip(Config.win_music);
        WinUI.gameObject.SetActive(true);
        SuspendOnClick();
        //TODO 制作一下胜利的UI 并且制作暂停游戏,失败或胜利后可以重新开始的按钮和退出的按钮
    }

    //Suspend按钮触发
    public void SuspendOnClick()   //激活退出游戏和重新开始的按钮并暂停阳光生成、僵尸生成、动画播放、
    {
        AudioManager.Instance.PlayClip(Config.pause);
        pauseAllZombie = GameObject.FindGameObjectsWithTag("Zombie");
        pauseAllPlant = GameObject.FindGameObjectsWithTag("Plant");
        #region 暂停僵尸的实现
        if (pauseAllZombie != null)
        {
            foreach (GameObject obj in pauseAllZombie)
            {
                Zombie zombie = obj.GetComponent<Zombie>();
                if (zombie != null) zombie.enabled = false;
                Animator anim = obj.GetComponent<Animator>();
                if (anim != null) anim.enabled = false;
            }
        }
        //暂停生成僵尸:
#endregion

        #region 暂停植物
        if (pauseAllPlant != null)
        {
            foreach (GameObject obj in pauseAllPlant)
            {
                SunFlower zombie = obj.GetComponent<SunFlower>();
                if (zombie != null) zombie.enabled = false;
                Animator anim = obj.GetComponent<Animator>();
                if (anim != null) anim.enabled = false;
                PeaShooter peaShooter = obj.GetComponent<PeaShooter>();
                if (peaShooter != null) peaShooter.enabled = false;
                ContinuousPeaShooter cps = obj.GetComponent<ContinuousPeaShooter>();
                if(cps != null) cps.enabled = false;
            }
        }
        #endregion
        SunManager.Instance.startProduceNaturalSun = false; //暂停生成阳光
                                                            
        cardListUI.DisableCardList();      //关闭卡槽
    }
    public void CancelSuspend()
    {
        pauseAllZombie = GameObject.FindGameObjectsWithTag("Zombie");
        pauseAllPlant = GameObject.FindGameObjectsWithTag("Plant");
        #region 恢复僵尸
        if (pauseAllZombie != null)
        {
            foreach (GameObject obj in pauseAllZombie)
            {
                Zombie zombie = obj.GetComponent<Zombie>();
                if (zombie != null) zombie.enabled = true;
                Animator anim = obj.GetComponent<Animator>();
                if (anim != null) anim.enabled = true;
            }
        }
        #endregion

        #region 恢复植物
        if (pauseAllPlant != null)
        {
            foreach (GameObject obj in pauseAllPlant)
            {
                SunFlower zombie = obj.GetComponent<SunFlower>();
                if (zombie != null) zombie.enabled = true;
                Animator anim = obj.GetComponent<Animator>();
                if (anim != null) anim.enabled = true;
                PeaShooter peaShooter = obj.GetComponent<PeaShooter>();
                if (peaShooter != null) peaShooter.enabled = true;
                ContinuousPeaShooter cps = obj.GetComponent<ContinuousPeaShooter>();
                if (cps != null) cps.enabled = true;
            }
        }
        #endregion
        SunManager.Instance.startProduceNaturalSun = true; //开启生成阳光

        cardListUI.show();      //开启卡槽
    }

    public void ReStart()
    {
        ZombieManager.Instance.GetComponent<ZombieManager>().enabled = false;
        ZombieManager.Instance.GetComponent<ZombieManager>().enabled = true;
        SceneManager.LoadScene("MenuScene");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void ShowSuspendUI()
    {
        gameSuspend.gameObject.SetActive(true);
    }
    public void CloseSuspendUI()
    {
        gameSuspend.gameObject.SetActive(false);
    }

}
