using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class Sun : MonoBehaviour
{
    private int point = 25;  //生产的阳光为25
    float speed = 5f;
    bool isNatural = false;
    Vector3 targetPosition;
    Vector3 position; //position是太阳花生成的阳光的目的地
    public float jumpMinDistance = 0.3f;
    public float jumpMaxDistance = 4;
    bool MouseDown = false;
    float ToTextSpeed = 8f;
    public void JumpTo(Vector3 position)
    {
        //TODO 阳光跳跃出来生成
       
    }
    public void isNaturalSun(bool judge)
    {
        isNatural = judge;
        if (judge)
        {
            float targetPosition_y = Random.Range(-4, 3.5f);
            targetPosition = new Vector3(transform.position.x, targetPosition_y, -1);
        }
        else
        {
         
            float distance = Random.Range(jumpMinDistance, jumpMaxDistance);
            distance = Random.Range(0, 2) < 1 ? -distance : distance;
             position = transform.position;     //position是随机生成的阳光的目的地
            position.x += distance;
        }
        
    }
    public void OnMouseDown()
    {
        speed = 0;
        MouseDown = true;
        AudioManager.Instance.PlayClip(Config.collectSun);
 
    }
    private void Update()
    {
        if(isNatural)
        {
            naturalSunMove();
        }
        else
        {
            unNaturalSunMove();   //植物产生的阳光的移动逻辑
        }
        if (MouseDown)
        {
            MoveToTextSun();
        }
    }
    public void MoveToTextSun()   //让收集的阳关飞向左上角
    {
        if (ToTextSpeed == 0) return;
        float step = ToTextSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position,new Vector3(-4.75f,4.52f,-1),step);
           
        if (Vector3.Distance(transform.position, new Vector3(-4.75f, 4.52f, -1)) < 0.1f)
        {
            GameObject.Destroy(gameObject);   //摧毁阳光
            SunManager.Instance.AddSun(point); //获取阳光数值并销毁
            ToTextSpeed = 0;
        }
    }
    public void naturalSunMove()
    {
        if (speed == 0) return;
        //计算每帧移动的距离:
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step); //线性移动函数,起始位置,终点,步频
        if (Vector3.Distance(transform.position, targetPosition) < 0.001f)
        {
            speed = 0;
        }
    }
    public void unNaturalSunMove()
    {
        if(speed == 0) return;
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position,position, step);
        if (Vector3.Distance(transform.position, targetPosition) < 0.001f)
        {
            speed = 0;      
        }
    }
}
