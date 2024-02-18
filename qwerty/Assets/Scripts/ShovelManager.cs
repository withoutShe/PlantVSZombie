using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShovelManager : MonoBehaviour
{
    public GameObject shovel;
    bool CheckSide = false;
    public Image ShovelImage;
    public bool HaveShovel=false;
    private void Update()
    {
        FollowCursor();
        if (Input.GetMouseButtonDown(0))
        {
            CheckSide= true;          
        }
        //������Ҽ������ӷŻ�ԭλ
        if(Input.GetMouseButtonDown(1))
        {
            HaveShovel = false;
            ShovelImage.gameObject.SetActive(true);
            ShovelImage.transform.position = HandMannger.Instance.ShovelPosition;
            shovel.SetActive(false);         
        }

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Plant"&&CheckSide)
        {
            AudioManager.Instance.PlayClip(Config.pause);
            Destroy(collision.gameObject);
            CheckSide = false;
        }      
            
        
    }
    private void FollowCursor()
    {
        if (shovel == null) return;
        //��ȡ�����������긳ֵ��ѡ�е�ֲ��,��ֲ���������ǵ�����ƶ����ƶ�
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0; //��ά����
      
        shovel.transform.position = mouseWorldPosition;

    }
}
