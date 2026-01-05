using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShips : MonoBehaviour
{

    private Dictionary<string, string> ShipCoords = new Dictionary<string, string>()
    {
        { "AircraftCarrier",  "I6" },
        { "BattleShip",  "H7" },
        { "Submarine",  "G8" },
        { "Cruiser",  "F8" },
        { "Destroyer",  "E9" }
    };

    // Start is called before the first frame update
    void Start()
    {

        GridManager GridManager = (GridManager)GameObject.Find("GridManager").GetComponent("GridManager");
        IDictionary<string, GameObject> coords = GridManager.gridStore;

        foreach (KeyValuePair<string, string> Ship in ShipCoords)
        {
            string ShipType = Ship.Key;
            string ShipCoord = Ship.Value;
            GameObject ShipObject = (GameObject)Instantiate(Resources.Load(ShipType));
            SpriteRenderer Sprite = ShipObject.GetComponent<SpriteRenderer>();

            ShipObject.transform.SetParent(this.transform, true);


            ShipObject.GetComponent<ShipMovement>().MoveShip(coords[ShipCoord], Sprite);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
