using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardListUI : MonoBehaviour
{
    public List<Card> cardList;   
    private void Start()
    {
        DisableCardList();
       gameObject.SetActive(false);   //等摄像机移动结束后再启用
    }
    public void DisableCardList()
    {
        foreach(Card card in cardList)
        {
            card.DisableCard();
        }
    }
    private void EnableCardList()
    {
        foreach (Card card in cardList)
        {
            card.EnableCard();
        }
    }

    public void show()
    {
        EnableCardList();

    }
}
