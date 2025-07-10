using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "World", menuName = "Scriptable Objects/World")]
public class World : ScriptableObject
{
    public int size;
    public int offSet;
    public List<SetPieceData> setpieces;
}

[Serializable]
public class SetPieceData
{
    [field: SerializeField] public SetPiece piece { get; private set; }
    [field: SerializeField] public bool essential { get; private set; }
    [field: Range(1, 100)][field: SerializeField] public int rarity { get; private set; }

    [field: SerializeField] public bool AmountRange { get; private set; }
    [field: SerializeField] public int minAmount { get; private set; }
    [field: SerializeField] public int maxAmount { get; private set; }
}

