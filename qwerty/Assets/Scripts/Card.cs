using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

enum CardState
{
    Disable,
    Cooling,                       //冷却状态
    WaitingSun,                    //等待阳光状态
    Ready,                         //准备就绪
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
    private CardState cardState = CardState.Disable;   //卡片最开始为冷却状态
    public GameObject cardLight;
    public GameObject cardGary;
    public Image cardMask;

    public float cdTime = 2;        //卡片冷却时间
    private float cdTimer = 0;       //计时器
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
        cdTimer += Time.deltaTime;    //cdTimer作为从0增加的时间
                                      //fillAmount是一个比例,即剩余冷却时间的比例,其等于 = 剩余时间/总的冷却时间
                                      //剩余时间等于总的冷却时间减去从0到现在所增加的时间
        cardMask.fillAmount = (cdTime - cdTimer) / cdTime;

        if (cdTimer > cdTime)
        {
            TransitionToWaitingSun();  //冷却时间结束,切换到等待阳光状态
        }
    }

    private void WaitingSunUpdate()
    {
        if(needSunPoint<=SunManager.Instance.SunPoint)   //需要的阳光值小于已经拥有的阳光值 
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
        if (cardState == CardState.Disable) return;   //游戏结束,状态被禁用直接返回
        if (needSunPoint > SunManager.Instance.SunPoint) return;

      bool isSuccess =   HandMannger.Instance.AddPlant(plantType);
        if(isSuccess)   //如果种植成功才执行下面代码
        {
            SunManager.Instance.SubSun(needSunPoint);  //计算种植植物所消耗的阳光
            TransitionToCooling();  //切换种植冷却
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
