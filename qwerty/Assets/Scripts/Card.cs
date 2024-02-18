using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

enum CardState
{
    Disable,
    Cooling,                       //��ȴ״̬
    WaitingSun,                    //�ȴ�����״̬
    Ready,                         //׼������
}
public enum PlantType
{
    Sunflower,
    PeaShooter,
    ContinuousPeaShooter,
}
public class Card : MonoBehaviour
{
    public PlantType plantType = PlantType.Sunflower;
    private CardState cardState = CardState.Disable;   //��Ƭ�ʼΪ��ȴ״̬
    public GameObject cardLight;
    public GameObject cardGary;
    public Image cardMask;

    public float cdTime = 2;        //��Ƭ��ȴʱ��
    private float cdTimer = 0;       //��ʱ��
    public ShovelManager shovelManager;
    [SerializeField]
    private int needSunPoint = 50;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            shovelManager.HaveShovel = false;
        }
            switch (cardState)
        {
            case CardState.Cooling:
                CoolingUpdate();
                break;
            case CardState.WaitingSun:
                WaitingSunUpdate();
                break;
            case CardState.Ready:
                ReadyUpdate();
                break;
            default:
                break;
        }

    }
    private void CoolingUpdate()
    {
        cdTimer += Time.deltaTime;    //cdTimer��Ϊ��0���ӵ�ʱ��
                                      //fillAmount��һ������,��ʣ����ȴʱ��ı���,����� = ʣ��ʱ��/�ܵ���ȴʱ��
                                      //ʣ��ʱ������ܵ���ȴʱ���ȥ��0�����������ӵ�ʱ��
        cardMask.fillAmount = (cdTime - cdTimer) / cdTime;

        if (cdTimer > cdTime)
        {
            TransitionToWaitingSun();  //��ȴʱ�����,�л����ȴ�����״̬
        }
    }

    private void WaitingSunUpdate()
    {
        if(needSunPoint<=SunManager.Instance.SunPoint)   //��Ҫ������ֵС���Ѿ�ӵ�е�����ֵ 
        {
            TransitionToReady();
        }
    }

    private void ReadyUpdate()
    {
        if (needSunPoint > SunManager.Instance.SunPoint)   
        {
            TransitionToWaitingSun();
        }
    }

    private void TransitionToWaitingSun()
    {
        cardState = CardState.WaitingSun;
        cardLight.SetActive(false);
        cardGary.SetActive(true);
        cardMask.gameObject.SetActive(false);
    }

    private void TransitionToReady()
    {
        cardState = CardState.Ready;
        cardLight.SetActive(true);
        cardGary.SetActive(false);
        cardMask.gameObject.SetActive(false);
    }
    private void TransitionToCooling()
    {
        cardState = CardState.Cooling;
        cdTimer = 0;
        cardLight.SetActive(false);
        cardGary.SetActive(true);
        cardMask.gameObject.SetActive(true);
    }
    public void OnClick()
    {
       
        if (shovelManager.HaveShovel) return;
        AudioManager.Instance.PlayClip(Config.btn_click);
        if (cardState == CardState.Disable) return;   //��Ϸ����,״̬������ֱ�ӷ���
        if (needSunPoint > SunManager.Instance.SunPoint) return;

      bool isSuccess =   HandMannger.Instance.AddPlant(plantType);
        if(isSuccess)   //�����ֲ�ɹ���ִ���������
        {
            SunManager.Instance.SubSun(needSunPoint);  //������ֲֲ�������ĵ�����
            TransitionToCooling();  //�л���ֲ��ȴ
        }
      
    }

   public void DisableCard()
    {
        cardState = CardState.Disable;
    }
    public void EnableCard()
    {
        TransitionToCooling();
    }
}
