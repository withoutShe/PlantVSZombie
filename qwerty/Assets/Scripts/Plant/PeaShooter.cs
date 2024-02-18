using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaShooter : Plant
{
    public float shootDuration = 2f;
    private float shoorTimer = 0;
    public Transform shootPointTransform; //豌豆发出点的坐标
    public PeaBullet peaBulletPrefab;
    public float bulletSpeed = 5;
    public int atkValue = 20;
    protected override void EnableUpdate()
    {
            shoorTimer += Time.deltaTime;
            if (shoorTimer > shootDuration)
            {
                Shoot();
                shoorTimer = 0;
            }
        
        
    }

   private void Shoot()
    {

       PeaBullet pb =  GameObject.Instantiate(peaBulletPrefab, shootPointTransform.position, Quaternion.identity);
        AudioManager.Instance.PlayClip(Config.shoot);
        pb.SetSpeed(bulletSpeed);
        pb.SetATKValue(atkValue);
    }
}
