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
        //��ʼ�����������
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
        AudioManager.Instance.PlayClip(Config.lose_music);//���ŵ�ǰ�ؿ��������ֲ��������ű�������
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
        //TODO ����һ��ʤ����UI ����������ͣ��Ϸ,ʧ�ܻ�ʤ����������¿�ʼ�İ�ť���˳��İ�ť
    }

    //Suspend��ť����
    public void SuspendOnClick()   //�����˳���Ϸ�����¿�ʼ�İ�ť����ͣ�������ɡ���ʬ���ɡ��������š�
    {
        AudioManager.Instance.PlayClip(Config.pause);
        pauseAllZombie = GameObject.FindGameObjectsWithTag("Zombie");
        pauseAllPlant = GameObject.FindGameObjectsWithTag("Plant");
        #region ��ͣ��ʬ��ʵ��
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
        //��ͣ���ɽ�ʬ:
#endregion

        #region ��ֲͣ��
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
        SunManager.Instance.startProduceNaturalSun = false; //��ͣ��������
                                                            
        cardListUI.DisableCardList();      //�رտ���
    }
    public void CancelSuspend()
    {
        pauseAllZombie = GameObject.FindGameObjectsWithTag("Zombie");
        pauseAllPlant = GameObject.FindGameObjectsWithTag("Plant");
        #region �ָ���ʬ
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

        #region �ָ�ֲ��
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
        SunManager.Instance.startProduceNaturalSun = true; //������������

        cardListUI.show();      //��������
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
