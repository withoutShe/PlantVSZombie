using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HandMannger : MonoBehaviour
{
    public static HandMannger Instance { get; private set; }
    public Plant currentPlant;  //��ǰҪ��ֲ��ֲ��
    public List<Plant> plantPrefabList;   //��ǰ��ӵ�е�ֲ��ļ���
    public TextMeshProUGUI Timer;
    public Image ShovelImage;
    bool awakening = false;
    public Vector3 ShovelPosition;
    private int awakeningCount = 0;
    public ShovelManager shovelManager;
    public GameObject shovel;
    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        FollowCursor();
    }
    public bool AddPlant(PlantType plantType)  //����bool�ж��Ƿ���ֲ�ɹ�,�Դ��ж������Ƿ���ֲ��
    {
        if(currentPlant!=null) { return false; }  //���������ֲ��ֱ�ӷ���false��ǲ��ɹ�
        Plant plantPrefab = GetPlantPrefab(plantType); 
        if(plantPrefab == null)
        {
            return false;
        }
      currentPlant = GameObject.Instantiate(plantPrefab);
        //ͨ��GameObject�е�Instantiate(ʵ����)ʵ����Ԥ�Ƽ�,������һ����ѡ�е�ֲ��
        return true;  //ʵ�����ɹ�return true;
    }
    public Plant GetPlantPrefab(PlantType plantType)
    {
        foreach(Plant plant in plantPrefabList)    //�������ϲ����Ƿ���ڿ���ֲ��ֲ������
        {      //��Plant plant��������һ����ʱ���� plant��������ѭ����ÿ�ε����б���ֵΪ�б��е�һ��Ԫ�ء�
            if (awakeningCount == 3) awakening = true;
            if (plantType == PlantType.PeaShooter && awakening)
            {
                awakeningCount = 0;
                Timer.text = awakeningCount + "/" + "3";
                awakening = false;
                return plantPrefabList[2];
            }
            else if (plant.plantType == plantType)
            {
                if (plantType == PlantType.PeaShooter)
                {
                    awakeningCount++;
                    Timer.text = awakeningCount + "/" + "3";
                }
                return plant;
            }
           
        }
        return null;
    }
    private void FollowCursor()
    {
        if (currentPlant == null) return;

        //��ȡ�����������긳ֵ��ѡ�е�ֲ��,��ֲ���������ǵ�����ƶ����ƶ�
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0; //��ά����
        currentPlant.transform.position = mouseWorldPosition;

    }

    public void OnCellClick(Cell cell)
    {
        if (currentPlant == null) return;

        bool isSuccess = cell.AddPlant(currentPlant);
        if(isSuccess)   //��ֲ�ɹ��������Ϊ��
        {
            AudioManager.Instance.PlayClip(Config.plant);
            currentPlant = null;
        }
    }

    public void OnShovelClick()
    {
        if (currentPlant!=null) return;
        shovel.gameObject.SetActive(true);
        shovel.GetComponent<Button>().enabled = true;
        shovelManager.HaveShovel = true;
        ShovelPosition = ShovelImage.transform.position;
        ShovelImage.gameObject.SetActive(false);
    }
}
