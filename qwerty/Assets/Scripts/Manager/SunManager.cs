using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using TMPro;   //TextMeshProUGUI�������ռ�


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

    [SerializeField] //�����������Unity�༭����
                     //������һ��˽���ֶΣ�private field��
                     //��Unity�༭���ļ�����壨Inspector������ʾ�ͱ༭��
                     //������Ҫ���ֶ�����Ϊpublic
    private int sunPoint;

    public int SunPoint
    {
        get { return sunPoint; }
    }
    public TextMeshProUGUI sunPointText;
    private void UpdateSunPointText()   //����UI����������ı�
    {
        sunPointText.text = SunPoint.ToString();
    }

    public void SubSun(int point)   //���¼�������ֵ
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

            GameObject go = GameObject.Instantiate(sunPrefab, startPosition, Quaternion.identity); //������������ʵ���������Ķ�����ת
            go.GetComponent<Sun>().isNaturalSun(true);
        }
        
    }
}
