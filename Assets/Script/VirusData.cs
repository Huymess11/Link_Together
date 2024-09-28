using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName ="VirusData", menuName ="STO/VirusData")]
public class VirusData : ScriptableObject
{
    public List<VirusInfo> virusData = new List<VirusInfo>();
}
[System.Serializable]
public class VirusInfo
{
    public int id;
    public GameObject virusPrefab;
}
