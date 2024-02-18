using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum SpawnState
{
    NotStart,   //未开始生产僵尸
    Spawning,   //生产
    End,

}
public class ZombieManager : MonoBehaviour
{
    public static ZombieManager Instance { get; private set; }
    private SpawnState spawnState = SpawnState.NotStart;
    public Transform[] spawnPointList;
    public GameObject zombiePrefab;
    GameObject[] zombieCounter;
    public static int ZombieCount = 0;
    private Coroutine stop_Cor;
    private void Awake()
    {
        Instance = this;
        ZombieCount = 0;
    }
    private void Start()
    {
        //StartSpawn();   //一开始直接生成僵尸
    }
    private void Update()
    {
        zombieCounter = GameObject.FindGameObjectsWithTag("Zombie");
        
        if (spawnState == SpawnState.End&&zombieCounter.Length==0 )
        {          
            GameManager.Instance.successOfGame();
        }
    }
    public void StartSpawn()
    {
        spawnState = SpawnState.Spawning;
      stop_Cor  =  StartCoroutine("SpawnZombie");
    }
    public void StopSpawn()   //Suspend按钮点击触发
    {
        StopCoroutine(stop_Cor);
    }
    IEnumerator SpawnZombie()
    {
        //第一波僵尸进攻
        for(int i = 0; i < 5; i++)
        {
            if (ZombieCount > 5) break;
            ZombieCount++;
            SpawnARandomZombie();
            yield return new WaitForSeconds(4);  //等待三秒钟生成五只中的下一只
        }
        yield return new WaitForSeconds(20);  ///每一波的时间间隔
        //第二波
        for (int i = 0; i < 10; i++)
        {
            if (ZombieCount > 15) break;
            ZombieCount++;
            SpawnARandomZombie();
            yield return new WaitForSeconds(4);  //等待三秒钟生成五只中的下一只
        }
        yield return new WaitForSeconds(25);
        //第三波
        AudioManager.Instance.PlayClip(Config.lastwave);
        for (int i = 0; i < 20; i++)
        {
            if (ZombieCount > 35) break;
            ZombieCount++;
            SpawnARandomZombie();
            yield return new WaitForSeconds(4);  //等待三秒钟生成五只中的下一只
        }
        spawnState = SpawnState.End;
       
    }
    private void SpawnARandomZombie()   //随机生产一个僵尸
    {
        int index = Random.Range(0, spawnPointList.Length);  //从预制好的生成点获取僵尸的位置以便于实例化僵尸
        GameObject go = GameObject.Instantiate(zombiePrefab, spawnPointList[index].position,Quaternion.identity);     
        go.GetComponent<SpriteRenderer>().sortingOrder = spawnPointList[index].GetComponent<SpriteRenderer>().sortingOrder;
        go.tag = "Zombie";
        //go.GetComponent<SpriteRenderer>().color = Color.red;
        int num = Random.Range(-1, 3);
        if (num < 0)
        {
            go.GetComponent<SpriteRenderer>().color = Color.red;
            go.GetComponent<Zombie>().HP = 300;
            go.GetComponent<Zombie>().currentHP = 300;
            go.GetComponent<Zombie>().atkValue = 25;
        }
        //让最下方的敌人的order最大,以显示在上方的敌人的上面
    }
}
