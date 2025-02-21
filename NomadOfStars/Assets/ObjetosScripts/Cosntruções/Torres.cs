using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Torres", menuName = "Scriptable Objects/Torres")]
public class Torres : ScriptableObject
{
    public List<ObjectData> objectsData;
}

[Serializable]
public class ObjectData
{

    [field: SerializeField]public string Name { get; private set; }
    [field: SerializeField]public int ID { get; private set; }
    [field: SerializeField]public int WoodPrice { get; private set; }
    [field: SerializeField]public int StonePrice { get; private set; }
    [field: SerializeField]public int Damage { get; private set; }
    [field: SerializeField]public float AtkDelay { get; private set; }
    [field: SerializeField]public GameObject Struct { get; private set; }
    [field: SerializeField]public GameObject Munition { get; private set; }
}