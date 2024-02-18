using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrepareUI : MonoBehaviour
{
    public Animator anim;
    public CardListUI cardListUI;
    public LawnMovers lawnMovers;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        anim.enabled = false;
    }
    public void PlayPrepareUI()
    {
        lawnMovers.gameObject.SetActive(true);
      anim.enabled=true;
      cardListUI.gameObject.SetActive(true);   //启用卡槽对象
      cardListUI.show();       //启用卡槽
      ZombieManager.Instance.StartSpawn();   //开始生成僵尸
       SunManager.Instance.start_produceNaturalSun();  //开始产生自然阳光
        gameObject.SetActive(false);       //关闭PrepareUI
        AudioManager.Instance.PlayBgm(Config.bgm1);
    }
}
