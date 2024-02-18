using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailGameUI : MonoBehaviour
{
    public Animator anim;
    public CardListUI cardListUI;

    private void Awake()
    {

        anim = GetComponent<Animator>();
        anim.enabled = false;
    }

    public void show()
    {
        anim.enabled = true;
    }
    public void hide()
    {
        cardListUI.DisableCardList();  //½ûÓÃ¿¨Æ¬
    }
}
