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
      cardListUI.gameObject.SetActive(true);   //���ÿ��۶���
      cardListUI.show();       //���ÿ���
      ZombieManager.Instance.StartSpawn();   //��ʼ���ɽ�ʬ
       SunManager.Instance.start_produceNaturalSun();  //��ʼ������Ȼ����
        gameObject.SetActive(false);       //�ر�PrepareUI
        AudioManager.Instance.PlayBgm(Config.bgm1);
    }
}
