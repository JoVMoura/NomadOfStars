using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "SetPiece", menuName = "Scriptable Objects/SetPiece")]
public class SetPiece : ScriptableObject
{
    public string setPieceName;
    public List<LocalRangeData> localRange;
    [Space(10f)]
    [Header("TileMap")]
    public bool preSetTile;
    public List<Tile> tiles;
    [Space(10f)]
    [Header("Structures")]
    public List<StructuresData> structure;
}

[Serializable]
public class StructuresData
{
    [field: SerializeField] public GameObject prefab { get; private set; }
    [field: SerializeField] public int minAmount { get; private set; }
    [field: SerializeField] public int maxAmount { get; private set; }
    [field: SerializeField] public float marginX { get; private set; }
    [field: SerializeField] public float marginY { get; private set; }
    [field: SerializeField] public bool center { get; private set; }
}

[Serializable]
public class LocalRangeData
{

    [field: SerializeField] public int minX { get; private set; }
    [field: SerializeField] public int maxX { get; private set; }
    [field: SerializeField] public int minY { get; private set; }
    [field: SerializeField] public int maxY { get; private set; }
}