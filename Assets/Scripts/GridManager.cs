using UnityEngine;
using System;
using System.Collections.Generic;

public class GridManager : MonoBehaviour
{

    public int rows = 10;
    public int cols = 10;
    public float tileSize = 0.8f;
    public IDictionary<string, GameObject> gridStore = new Dictionary<string, GameObject>();
    private string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    // Start is called before the first frame update
    void Start()
    {
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        char[] a = alphabet.ToCharArray();
        GameObject referenceTile = (GameObject)Instantiate(Resources.Load("water"));
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                GameObject tile = (GameObject)Instantiate(referenceTile, transform);

                float posX = col * tileSize;
                float posY = row * -tileSize;

                tile.name = a[col].ToString() + (row + 1).ToString();
                tile.transform.position = new Vector2(posX, posY);
                this.gridStore.Add(tile.name, tile);

            }
        }

        Destroy(referenceTile);

        float gridW = cols * tileSize;
        float gridH = rows * tileSize;
        transform.position = new Vector2(-gridW / 2 + tileSize / 2, gridH / 2 - tileSize / 2);

        foreach(Transform child in transform)
        {
            this.gridStore[child.gameObject.name] = child.gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
