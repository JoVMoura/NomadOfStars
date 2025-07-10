using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldCreate : MonoBehaviour
{
    [SerializeField] private List<World> worlds;
    [SerializeField] private GameObject grid;
    [SerializeField] private GameObject tilemap;
    [SerializeField] private GameObject colidermap;
    [SerializeField] private Tile tileColider;

    [SerializeField] private GameControl gameControl;

    void Start()
    {
        CreateMap(0);
        CreateMap(1);
        CreateMap(2);
    }

    public void CreateMap(int nWorld)
    {
        int size = worlds[nWorld].size;
        int offSet = worlds[nWorld].offSet;
        bool[,] vet;
        bool igual;
        int i = 0, j = 0, k = 0, w = 0, n = 0, x = 0, y = 0;
        SetPiece[,] map;
        GameObject mapaAtual;
        Tilemap tileMapAtual;
        Tilemap tileMapcolider;
        List<Tile> tileRange;
        List<StructuresData> StructureRange;
        int somaTolal = 0;
        List<int> numUnique, numGeneric, localInvX, localInvY;
        List<SetPiece> essential, unique, generic;

        vet = new bool[size, size];
        map = new SetPiece[size, size];

        numUnique = new List<int>();
        numGeneric = new List<int>();
        localInvX = new List<int>();
        localInvY = new List<int>();


        essential = new List<SetPiece>();
        unique = new List<SetPiece>();
        generic = new List<SetPiece>();

        mapaAtual = Instantiate(tilemap,new Vector3(offSet*5376, 0, 0), Quaternion.identity, grid.transform);
        tileMapAtual = mapaAtual.GetComponent<Tilemap>();
        tileMapcolider = Instantiate(colidermap, new Vector3(offSet*5376, 0, 0), Quaternion.identity, grid.transform).GetComponent<Tilemap>();
        tileRange = new List<Tile>();
        StructureRange = new List<StructuresData>();

        for (i = 0; i < worlds[nWorld].setpieces.Count; i++)
        {
            if (worlds[nWorld].setpieces[i].essential)
            {
                essential.Add(worlds[nWorld].setpieces[i].piece);
            }
            else if (worlds[nWorld].setpieces[i].AmountRange)
            {
                numUnique.Add(worlds[nWorld].setpieces[i].rarity);
                unique.Add(worlds[nWorld].setpieces[i].piece);
                somaTolal += worlds[nWorld].setpieces[i].rarity;
            }
            else
            {
                numGeneric.Add(worlds[nWorld].setpieces[i].rarity);
                generic.Add(worlds[nWorld].setpieces[i].piece);
                somaTolal += worlds[nWorld].setpieces[i].rarity;
            }
        }

        i = 0;
        j = 0;
        do
        {
            n = Random.Range(0, somaTolal);

            k = 0;
            while (k < numGeneric.Count && n >= 0)
            {
                n -= numGeneric[k];
                k++;
            }

            if (n >= 0)
            {
                k = 0;
                while (k < numUnique.Count && n >= 0)
                {
                    n -= numUnique[k];
                    k++;
                }
                k--;
                map[i, j] = unique[k];
            }
            else
            {
                k--;
                map[i, j] = generic[k];
            }

            j++;
            if (j >= size)
            {
                i++;
                j = 0;
            }

        } while (i < size);

        i = 0;
        while (i < essential.Count)
        {
            if (essential[i].localRange.Count == 0)
            {
                do
                {
                    x = Random.Range(0, size);
                    y = Random.Range(0, size);
                } while (vet[x, y] == true);
            }
            else
            {
                do
                {
                    j = Random.Range(0, essential[i].localRange.Count);
                    x = Random.Range(essential[i].localRange[j].minX, essential[i].localRange[j].maxX + 1) - 1;
                    y = Random.Range(essential[i].localRange[j].minY, essential[i].localRange[j].maxY + 1) - 1;
                    Debug.Log("X: " + x + "Y: " + y);
                    Debug.Log("VetX: " + vet.GetLength(0) + "VetY: " + vet.GetLength(0));
                } while (vet[x, y] == true);
            }

            map[x, y] = essential[i];
            vet[x, y] = true;
            i++;
        }

        essential.Clear();
        unique.Clear();
        generic.Clear();

        i = 0;
        j = 0;
        do
        {
            for (k = 0; k < map[i, j].tiles.Count; k++)
            {
                tileRange.Add(map[i, j].tiles[k]);
            }

            for (x = 0; x < 4; x++)
            {
                for (y = 0; y < 4; y++)
                {
                    n = Random.Range(0, tileRange.Count);
                    tileMapAtual.SetTile(new Vector3Int((i * 4) + x, (j * 4) + y, 0), tileRange[n]);
                }
            }

            tileRange.Clear();

            j++;
            if (j >= size)
            {
                i++;
                j = 0;
            }
        } while (i < size);

        i = 0;
        j = 0;
        do
        {
            if (map[i, j].structure.Count != 0)
            {
                for (k = 0; k < map[i, j].structure.Count; k++)
                {
                    if (map[i, j].structure[k].center)
                    {
                        if (map[i, j].structure[k].prefab.name.Contains("Base"))
                        {
                            gameControl.SetSpawn(Instantiate(map[i, j].structure[k].prefab, new Vector3(128 + (i * 254) + (offSet*5376), 128 + (j * 254), 0), Quaternion.identity).transform.GetChild(2));
                        }
                        else
                        {
                            Instantiate(map[i, j].structure[k].prefab, new Vector3(128 + (i * 254) + (offSet*5376), 128 + (j * 254), 0), Quaternion.identity);
                        }
                    }
                    else
                    {
                        n = Random.Range(map[i, j].structure[k].minAmount, map[i, j].structure[k].maxAmount + 1);
                        for (w = 0; w < n; w++)
                        {
                            igual = false;
                            do
                            {
                                x = Random.Range(10, 247);
                                y = Random.Range(10, 247);
                                /*foreach (int invX in localInvX)
                                {
                                    if (x == invX)
                                    {
                                        igual = true;
                                    }
                                }
                                if (!igual)
                                {
                                    foreach (int invY in localInvY)
                                    {
                                        if (y == invY)
                                        {
                                            igual = true;
                                        }
                                    }
                                }*/
                            } while (igual);
                            Instantiate(map[i, j].structure[k].prefab, new Vector3(x + (i * 254) + (offSet*5376), y + (j * 254), 0), Quaternion.identity);
                            localInvX.Add(x);
                            localInvY.Add(y);
                        }
                    }
                }
                localInvX.Clear();
                localInvY.Clear();
            }

            j++;
            if (j >= size)
            {
                i++;
                j = 0;
            }
        } while (i < size);

        for (i = 0; i < size*4; i++)
        {
            tileMapcolider.SetTile(new Vector3Int(-1, i, 0), tileColider);
            tileMapcolider.SetTile(new Vector3Int(i, -1, 0), tileColider);
            tileMapcolider.SetTile(new Vector3Int(size*4, i, 0), tileColider);
            tileMapcolider.SetTile(new Vector3Int(i, size*4, 0), tileColider);
        }
        tileMapcolider.SetTile(new Vector3Int(-1, size*4, 0), tileColider);
        tileMapcolider.SetTile(new Vector3Int(size*4, -1, 0), tileColider);
        tileMapcolider.SetTile(new Vector3Int(-1, -1, 0), tileColider);
        tileMapcolider.SetTile(new Vector3Int(size*4, size*4, 0), tileColider);
    } 
}
