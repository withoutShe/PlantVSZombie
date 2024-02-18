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
        //按鼠标右键将铲子放回原位
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
        //获取鼠标的世界坐标赋值给选中的植物,让植物随着我们的鼠标移动而移动
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0; //二维世界
      
        shovel.transform.position = mouseWorldPosition;

    }
}
