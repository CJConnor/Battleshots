using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{

    public int rows = 10;
    public int cols = 10;
    public float tileSize = 0.8f;
    public float maxValueX;
    public float minValueX;
    public float maxValueY;
    public float minValueY;
    public IDictionary<string, GameObject> gridStore = new Dictionary<string, GameObject>();
    private string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    public GameObject Canvas;

    // Start is called before the first frame update
    void Start()
    {
        GenerateGrid();
    }

    private void GenerateGrid()
    {

        /*rows += 1;
        cols += 1;*/

        char[] a = alphabet.ToCharArray();
        GameObject referenceTile = (GameObject)Instantiate(Resources.Load("water"));
        //GameObject referenceText = (GameObject)Instantiate(Resources.Load("Text"));

        for (int row = 0; row < rows; row++)
        {

            for (int col = 0; col < cols; col++)
            {

                /*if (row == 0 && col == 0)
                {
                    continue;
                }

                if (row == 0)
                {
                    GameObject text = (GameObject)Instantiate(referenceText, transform);

                    float posTX = col * tileSize;
                    float posTY = row * -tileSize;

                    text.transform.position = new Vector2(posTX, posTY);

                    text.transform.parent = Canvas.transform;

                    continue;
                }

                if (col == 0)
                {

                    GameObject text = (GameObject)Instantiate(referenceText, transform);

                    float posTX = col * tileSize;
                    float posTY = row * -tileSize;

                    text.transform.position = new Vector2(posTX, posTY);

                    text.transform.parent = Canvas.transform;

                    continue;

                }*/



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

            SpriteRenderer SpriteR = child.gameObject.GetComponent<SpriteRenderer>();

            Vector2 topLeft = new Vector2(child.gameObject.transform.position.x - SpriteR.bounds.extents.x, child.gameObject.transform.position.y + SpriteR.bounds.extents.y);
            Vector2 bottomRight = new Vector2(child.gameObject.transform.position.x + SpriteR.bounds.extents.x, child.gameObject.transform.position.y - SpriteR.bounds.extents.y);

            if (topLeft.x > maxValueX)
            {
                maxValueX = topLeft.x;
            }
            if (topLeft.y > maxValueY)
            {
                maxValueY = topLeft.y;
            }
            if (topLeft.x < minValueX)
            {
                minValueX = topLeft.x;
            }
            if (topLeft.y < minValueY)
            {
                minValueY = topLeft.y;
            }

            if (bottomRight.x > maxValueX)
            {
                maxValueX = bottomRight.x;
            }
            if (bottomRight.y > maxValueY)
            {
                maxValueY = bottomRight.y;
            }
            if (bottomRight.x < minValueX)
            {
                minValueX = bottomRight.x;
            }
            if (bottomRight.y < minValueY)
            {
                minValueY = bottomRight.y;
            }

        }

        //Debug.Log(maxValueX);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
