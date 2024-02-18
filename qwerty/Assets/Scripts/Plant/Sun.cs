using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class Sun : MonoBehaviour
{
    private int point = 25;  //����������Ϊ25
    float speed = 5f;
    bool isNatural = false;
    Vector3 targetPosition;
    Vector3 position; //position��̫�������ɵ������Ŀ�ĵ�
    public float jumpMinDistance = 0.3f;
    public float jumpMaxDistance = 4;
    bool MouseDown = false;
    float ToTextSpeed = 8f;
    public void JumpTo(Vector3 position)
    {
        //TODO ������Ծ��������
       
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
             position = transform.position;     //position��������ɵ������Ŀ�ĵ�
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
            unNaturalSunMove();   //ֲ�������������ƶ��߼�
        }
        if (MouseDown)
        {
            MoveToTextSun();
        }
    }
    public void MoveToTextSun()   //���ռ������ط������Ͻ�
    {
        if (ToTextSpeed == 0) return;
        float step = ToTextSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position,new Vector3(-4.75f,4.52f,-1),step);
           
        if (Vector3.Distance(transform.position, new Vector3(-4.75f, 4.52f, -1)) < 0.1f)
        {
            GameObject.Destroy(gameObject);   //�ݻ�����
            SunManager.Instance.AddSun(point); //��ȡ������ֵ������
            ToTextSpeed = 0;
        }
    }
    public void naturalSunMove()
    {
        if (speed == 0) return;
        //����ÿ֡�ƶ��ľ���:
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step); //�����ƶ�����,��ʼλ��,�յ�,��Ƶ
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
