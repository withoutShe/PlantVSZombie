using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum PlantState
{
    Disable,
    Enable,
}
public class Plant : MonoBehaviour
{
   PlantState plantState = PlantState.Disable;
    public PlantType plantType = PlantType.Sunflower;
    public int HP = 100;
    private void Start()
    {
        TransitionToDisable();
    }
    private void Update()
    {
        switch (plantState)
        {
            case PlantState.Disable:
                DisableUpdate();
                break;
            case PlantState.Enable:
                EnableUpdate();
                break;
        }
    }

    private void DisableUpdate()
    {

    }
  
   protected virtual void EnableUpdate()  //方便子类重写
    {

    }

    private void TransitionToDisable()
    {
        plantState = PlantState.Disable;
       
        
            GetComponent<Animator>().enabled = false;  //当植物拿到手上时将其Idle的状态关闭
            GetComponent<Collider2D>().enabled = false; // 防止植物拿在手中时与网格的碰撞体发生重叠
        
        
    }

    public void TransitionToEnable()
    {
        plantState = PlantState.Enable;
   
        
            GetComponent<Animator>().enabled = true;
            GetComponent<Collider2D>().enabled = true;
        
         
    }
   
    public void TakeDamage(int damage)
    {
        this.HP -= damage;
        if (HP <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        Destroy(gameObject);
    }
}
