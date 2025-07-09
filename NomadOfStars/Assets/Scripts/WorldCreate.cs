using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldCreate : MonoBehaviour
{
    [SerializeField] private List<World> worlds;
    [SerializeField] private GameObject grid;
    [SerializeField] private GameObject tilemap;

    void Start()
    {
        CreateMap(0);
    }

    public void CreateMap(int nWorld)
    {
        int size = worlds[nWorld].size;
        bool[,] vet;
        int i = 0, j = 0, k = 0, n = 0, x = 0, y = 0;
        SetPiece[,] map;
        GameObject mapaAtual;
        Tilemap tileMapAtual;
        List<Tile> tileRange;
        int somaTolal = 0;
        List<int> numUnique, numGeneric;
        List<SetPiece> essential, unique, generic;

        vet = new bool[size, size];
        map = new SetPiece[size, size];

        numUnique = new List<int>();
        numGeneric = new List<int>();

        essential = new List<SetPiece>();
        unique = new List<SetPiece>();
        generic = new List<SetPiece>();

        mapaAtual = Instantiate(tilemap, grid.transform);
        tileMapAtual = mapaAtual.GetComponent<Tilemap>();
        tileRange = new List<Tile>();

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
                    x = Random.Range(essential[i].localRange[j].minX, essential[i].localRange[j].maxX) + 1;
                    y = Random.Range(essential[i].localRange[j].minY, essential[i].localRange[j].maxY) + 1;
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

            j++;
            if (j >= size)
            {
                i++;
                j = 0;
            }
        } while (i < size);
    } 
}
