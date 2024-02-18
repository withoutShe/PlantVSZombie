using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunFlower : Plant   //控制阳光
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
        SunPosition.z = -1;  //因为Cell会与阳光的触发器重叠,所以需要让阳光离相机近点
        if (sunPrefab != null)
        {
            GameObject go = GameObject.Instantiate(sunPrefab, SunPosition, Quaternion.identity);
            go.GetComponent<Sun>().isNaturalSun(false);
        }
        
       
        //go.GetComponent<Sun>().JumpTo(position);
    }
}
