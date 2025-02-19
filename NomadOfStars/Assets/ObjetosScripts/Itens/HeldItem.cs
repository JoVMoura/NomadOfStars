using UnityEngine;

[CreateAssetMenu(fileName = "HeldItem", menuName = "Scriptable Objects/HeldItem")]
public class HeldItem : ScriptableObject
{
    [Header("Header")]
    public int ID;
    public new string name;
    public GameObject prefab;
    [Space(10f)]
    [Header("Info")]
    public bool shoot;
}
