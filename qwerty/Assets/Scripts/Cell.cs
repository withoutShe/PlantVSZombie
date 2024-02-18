using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public Plant currentPlant;
    private void OnMouseDown()   //获取User按下鼠标的那个格子并传给OnCellClick
    {
        HandMannger.Instance.OnCellClick(this);   
    }
    public bool AddPlant(Plant plant)
    {
        if (currentPlant != null) return false; //手上有植物,无法再种植
        currentPlant = plant;
        currentPlant.transform.position = transform.position;
        plant.TransitionToEnable();
        return true;
    }
}
