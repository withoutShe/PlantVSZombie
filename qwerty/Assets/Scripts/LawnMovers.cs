using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;

public class LawnMovers : MonoBehaviour
{
    private float speed = 5f;
    public bool begining;
    private void Update()
    {
        if (begining==true)
        {
            AudioManager.Instance.PlayClip(Config.lawnmower);
            transform.Translate(Vector3.right * speed * Time.deltaTime);
            Destroy(gameObject, 10f);
        }
           
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Zombie")
        {
           this.begining = true;
        }
    }

}
