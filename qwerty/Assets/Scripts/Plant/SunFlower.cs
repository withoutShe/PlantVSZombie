using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunFlower : Plant   //��������
{
    public float produceDuration = 8;
    private float produceTimer = 0;
    private Animator anim;
    public GameObject sunPrefab;

   
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    protected override void EnableUpdate()
    {
        produceTimer += Time.deltaTime;
        if (produceTimer > produceDuration)
        {
            produceTimer = 0;
            anim.SetTrigger("isGlowing");
        }
    }
    public void ProduceSun()
    {
        Vector3 SunPosition = transform.position;
        SunPosition.z = -1;  //��ΪCell��������Ĵ������ص�,������Ҫ���������������
        if (sunPrefab != null)
        {
            GameObject go = GameObject.Instantiate(sunPrefab, SunPosition, Quaternion.identity);
            go.GetComponent<Sun>().isNaturalSun(false);
        }
        
       
        //go.GetComponent<Sun>().JumpTo(position);
    }
}
