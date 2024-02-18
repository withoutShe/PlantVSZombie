using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaBullet : MonoBehaviour
{
    private float speed = 3;
    private int atkValue = 30;
    public GameObject peaBulletHitPrefab;
   
    public void SetATKValue(int atkValue)
    {
        this.atkValue = atkValue;
    }
    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }
    private void Start()
    {
        Destroy(gameObject, 10); //�㶹���ɺ�ʮ���Զ�����
    }
    private void Update()
    {
        transform.Translate(Vector3.right * speed*Time.deltaTime,Space.World);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Zombie")
        {
            Destroy(this.gameObject);
            collision.GetComponent<Zombie>().TakeDamage(atkValue);
           GameObject tmp =  GameObject.Instantiate(peaBulletHitPrefab,transform.position,Quaternion.identity);
            Destroy(tmp, 1); //�㶹���Ѻ�һ������ٱ���Ч��
        }
    }
}
