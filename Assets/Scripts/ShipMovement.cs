using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{

    // Positional varibles
    private float startPosX;
    private float startPosY;
    private bool isBeingHeld = false;
    public int shipSize = 2;
    public IDictionary<string, GameObject> coords;
    private SpriteRenderer Sprite;

    // Start is called before the first frame update
    void Start()
    {
        // Get Ship Sprite
        Sprite = GetComponent<SpriteRenderer>();

        // Fetch Grid Manager Co-ordinates
        GridManager GridManager = (GridManager)GameObject.Find("GridManager").GetComponent("GridManager");
        coords = GridManager.gridStore;
    }

    // Update is called once per frame
    void Update()
    {

        // Checks if the ship is being held
        if (this.isBeingHeld == true)
        {

            // Collect the mosue position
            Vector2 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            // Use the mouse position to create the new imaginary location for the ship
            Vector2 ImaginaryLocation = new Vector2(mousePos.x - startPosX, mousePos.y - startPosY);

            // Get the top left vector of the imaginary ship
            Vector2 TopLeft = new Vector2(ImaginaryLocation.x - Sprite.bounds.extents.x, ImaginaryLocation.y + Sprite.bounds.extents.y);

            // Set a large closest distance and point variables
            float closestDistance = 1000f;
            string closestPoint = "";

            // Loop through the grid manager to get the grid coords
            foreach (KeyValuePair<string, GameObject> entry in coords)
            {
                // Get the distance between the imaginary top left vector and the tiles position
                float distance = Vector2.Distance(TopLeft, entry.Value.transform.position);

                // If the distance is less than the current closest distance
                if (distance <= closestDistance)
                {
                    closestDistance = distance;
                    closestPoint = entry.Key;
                }

            }


            MoveShip(coords[closestPoint], Sprite);

        }
        
    }

    // When the mouse is clicked down
    private void OnMouseOver()
    {
        
        // If the left mouse button key down
        if (Input.GetMouseButtonDown(0))
        {

            // Get the positional mouse location data
            Vector2 MousePos = Input.mousePosition;
            MousePos = Camera.main.ScreenToWorldPoint(MousePos);

            // Get the start vecotrs
            startPosX = MousePos.x - this.transform.localPosition.x;
            startPosY = MousePos.y - this.transform.localPosition.y;

            this.isBeingHeld = true;

        } else if (Input.GetMouseButtonDown(1))
        {

            float angle = 0;
            angle += 90;

            transform.Rotate(0, 0, angle);

        }

    }

    // When mouse has stopped being held down
    private void OnMouseUp()
    {
        this.isBeingHeld = false;    
    }

    public void MoveShip(GameObject Coords, SpriteRenderer Sprite)
    {
        // Get the sprite of the closest tile
        SpriteRenderer SpriteR = Coords.gameObject.GetComponent<SpriteRenderer>();

        // Get the top left vector of the tile
        float tileTopLeftX = (float)(Coords.transform.position.x - SpriteR.bounds.extents.x);
        float tileTopLeftY = (float)(Coords.transform.position.y + SpriteR.bounds.extents.y);

        // Get and set the new central position from the top left vector of the tile
        this.gameObject.transform.position = new Vector2(tileTopLeftX + Sprite.bounds.extents.x, tileTopLeftY - Sprite.bounds.extents.y);
    }
}
