using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum SpawnState
{
    NotStart,   //δ��ʼ������ʬ
    Spawning,   //����
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
        //StartSpawn();   //һ��ʼֱ�����ɽ�ʬ
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
    public void StopSpawn()   //Suspend��ť�������
    {
        StopCoroutine(stop_Cor);
    }
    IEnumerator SpawnZombie()
    {
        //��һ����ʬ����
        for(int i = 0; i < 5; i++)
        {
            if (ZombieCount > 5) break;
            ZombieCount++;
            SpawnARandomZombie();
            yield return new WaitForSeconds(4);  //�ȴ�������������ֻ�е���һֻ
        }
        yield return new WaitForSeconds(20);  ///ÿһ����ʱ����
        //�ڶ���
        for (int i = 0; i < 10; i++)
        {
            if (ZombieCount > 15) break;
            ZombieCount++;
            SpawnARandomZombie();
            yield return new WaitForSeconds(4);  //�ȴ�������������ֻ�е���һֻ
        }
        yield return new WaitForSeconds(25);
        //������
        AudioManager.Instance.PlayClip(Config.lastwave);
        for (int i = 0; i < 20; i++)
        {
            if (ZombieCount > 35) break;
            ZombieCount++;
            SpawnARandomZombie();
            yield return new WaitForSeconds(4);  //�ȴ�������������ֻ�е���һֻ
        }
        spawnState = SpawnState.End;
       
    }
    private void SpawnARandomZombie()   //�������һ����ʬ
    {
        int index = Random.Range(0, spawnPointList.Length);  //��Ԥ�ƺõ����ɵ��ȡ��ʬ��λ���Ա���ʵ������ʬ
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
        //�����·��ĵ��˵�order���,����ʾ���Ϸ��ĵ��˵�����
    }
}
