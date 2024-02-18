using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinuousPeaShooter : Plant
{
    public float shootDuration = 2;
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
            StartCoroutine(Shoot());
                shoorTimer = 0;
            }
       
    }

    IEnumerator Shoot()   //三连发豌豆设计思路
    {
        AudioManager.Instance.PlayClip(Config.shoot);
        PeaBullet pb = GameObject.Instantiate(peaBulletPrefab, shootPointTransform.position, Quaternion.identity);
       
        pb.SetSpeed(bulletSpeed);
        pb.SetATKValue(atkValue);
        yield return new WaitForSeconds(0.1f);
        //Vector3 pb1Position = shootPointTransform.position;
        //pb1Position.x = shootPointTransform.position.x - 0.6f;

        PeaBullet pb1 = GameObject.Instantiate(peaBulletPrefab, shootPointTransform.position, Quaternion.identity);

        pb1.SetSpeed(bulletSpeed);
        pb1.SetATKValue(atkValue);
        yield return new WaitForSeconds(0.1f);
        //Vector3 pb2Position = shootPointTransform.position;
        //pb2Position.x = pb1Position.x - 0.6f;
        PeaBullet pb2 = GameObject.Instantiate(peaBulletPrefab, shootPointTransform.position, Quaternion.identity);

        pb2.SetSpeed(bulletSpeed);
        pb2.SetATKValue(atkValue);
    }
}
