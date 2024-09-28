using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="ItemData",menuName ="STO/ItemData")]
public class ItemData : ScriptableObject
{
    public List<ItemInfor> data = new List<ItemInfor>();
}
[System.Serializable]
public class ItemInfor
{
    public int id;
    public GameObject itemPrefab;
}
