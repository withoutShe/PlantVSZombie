using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using TMPro;   //TextMeshProUGUI的命名空间


public class SunManager : MonoBehaviour
{
    public static SunManager Instance { get; private set; }
    public GameObject sunPrefab;
    private float startPosition_y = 6;
    private float produceSunTime = 5f;
    private float produceSunTimer = 0;
    public bool startProduceNaturalSun = false;
    private void Awake()
    {
        Instance = this; 
    }
    private void Start()
    {
        UpdateSunPointText();
    }
    public void Update()
    {
        if (startProduceNaturalSun)
        {
            produceNaturalSun();
        }
    }

    [SerializeField] //这个属性用于Unity编辑器，
                     //它允许一个私有字段（private field）
                     //在Unity编辑器的检视面板（Inspector）中显示和编辑，
                     //而不需要把字段设置为public
    private int sunPoint;

    public int SunPoint
    {
        get { return sunPoint; }
    }
    public TextMeshProUGUI sunPointText;
    private void UpdateSunPointText()   //更新UI界面的阳光文本
    {
        sunPointText.text = SunPoint.ToString();
    }

    public void SubSun(int point)   //更新计算阳光值
    {
        sunPoint -= point;
        UpdateSunPointText();
    }
    public void AddSun(int point)
    {
        sunPoint+= point;
        UpdateSunPointText();
    }
    public void start_produceNaturalSun()
    {
        startProduceNaturalSun = true;
    }
    public void produceNaturalSun()
    {
        produceSunTimer += Time.deltaTime;
        if (produceSunTimer > produceSunTime)
        {
            produceSunTimer = 0;

            float startPosition_x = Random.Range(-5, 8);
            Vector3 startPosition = new Vector3(startPosition_x, startPosition_y, -1);

            GameObject go = GameObject.Instantiate(sunPrefab, startPosition, Quaternion.identity); //第三个参数让实例化出来的对象不旋转
            go.GetComponent<Sun>().isNaturalSun(true);
        }
        
    }
}
