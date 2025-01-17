using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tower", menuName = "Tower")]
public class Tower : ScriptableObject
{
    public string towerName;
    public int cost;
    public GameObject tower;
    public float health = 3;
    public string description;
    public string upgrade1Desc;
    public string upgrade2Desc;
    public string upgrade3Desc;
    public Sprite picture;
    public float upgrade1Cost;
    public float upgrade2Cost;
    public float upgrade3Cost;
    public float upgrade1Health;
    public float upgrade2Health;
    public float upgrade3Health;




    
    public int CalculateSellValue(){
        return (int) GameManager.instance.SellPercentage * cost;
    }
}
