using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "Scriptable Objects/Wave")]
public class Wave : ScriptableObject
{
    public int waveDensity;
    public int waveTime;
    public List<WaveData> waveData;
}

[Serializable]
public class WaveData
{
    [field: SerializeField]public GameObject Enemy { get; private set; }
    [field: SerializeField]public int Amount { get; private set; }

}