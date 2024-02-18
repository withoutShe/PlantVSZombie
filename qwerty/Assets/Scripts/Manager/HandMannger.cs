using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HandMannger : MonoBehaviour
{
    public static HandMannger Instance { get; private set; }
    public Plant currentPlant;  //当前要种植的植物
    public List<Plant> plantPrefabList;   //当前所拥有的植物的集合
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
    public bool AddPlant(PlantType plantType)  //返回bool判断是否种植成功,以此判断手上是否有植物
    {
        if(currentPlant!=null) { return false; }  //如果手上有植物直接返回false标记不成功
        Plant plantPrefab = GetPlantPrefab(plantType); 
        if(plantPrefab == null)
        {
            return false;
        }
      currentPlant = GameObject.Instantiate(plantPrefab);
        //通过GameObject中的Instantiate(实例化)实例化预制件,即创建一个所选中的植物
        return true;  //实例化成功return true;
    }
    public Plant GetPlantPrefab(PlantType plantType)
    {
        foreach(Plant plant in plantPrefabList)    //遍历集合查找是否存在可种植的植物类型
        {      //‘Plant plant’声明了一个临时变量 plant，它将在循环的每次迭代中被赋值为列表中的一个元素。
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

        //获取鼠标的世界坐标赋值给选中的植物,让植物随着我们的鼠标移动而移动
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0; //二维世界
        currentPlant.transform.position = mouseWorldPosition;

    }

    public void OnCellClick(Cell cell)
    {
        if (currentPlant == null) return;

        bool isSuccess = cell.AddPlant(currentPlant);
        if(isSuccess)   //种植成功后把手置为空
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
