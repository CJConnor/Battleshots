using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitScript : MonoBehaviour
{

    public List<string> hits;
    public List<string> miss;
    public IDictionary<string, List<string>> ships = new Dictionary<string, List<string>>();
    private GameObject hitReference;
    private GameObject missReference;
    private GameObject Opponent;

    // Start is called before the first frame update
    void Start()
    {

        GameManager gameManager = (GameManager)GameObject.Find("GameManager").GetComponent("GameManager");

        hitReference = (GameObject)Instantiate(Resources.Load("Hit"));
        missReference = (GameObject)Instantiate(Resources.Load("Miss"));

        // Get the next player in line
        int index = Int32.Parse(this.gameObject.name.Remove(0, 6)) - 1;

        index = index + 1 > gameManager.playerCount - 1 ? 0 : index + 1;

        Opponent = gameManager.players[index];

        ships = gameManager.ships[Opponent.name];

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {

            GameManager gameManager = (GameManager)GameObject.Find("GameManager").GetComponent("GameManager");
            GridManager gridManager = (GridManager)GameObject.Find("GridManager").GetComponent("GridManager");
            IDictionary<string, GameObject> gridStore = gridManager.gridStore;

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if (hit.collider != null)
            {


                if (hit.collider.gameObject.layer.Equals(4))
                {
                    string shot = "";

                    if (hits.Contains(hit.collider.gameObject.name) == false || miss.Contains(hit.collider.gameObject.name) == false)
                    {

                        string remove = "";

                        foreach (KeyValuePair<string, List<string>> entry in ships)
                        {

                            if (entry.Value.Contains(hit.collider.gameObject.name))
                            {
                                shot = entry.Key;

                                break;
                            }

                        }

                        if (shot != "")
                        {
                            hits.Add(hit.collider.gameObject.name);

                            ships[shot].Remove(hit.collider.gameObject.name);

                            GameObject token = (GameObject)Instantiate(hitReference, transform);

                            token.transform.position = gridStore[hit.collider.gameObject.name].transform.position;

                            token.SetActive(true);

                            if (ships[shot].Count == 0)
                            {
                                GameObject Ship = transform.Find(shot).gameObject;
                                Ship.transform.position = Opponent.transform.Find(shot).gameObject.transform.position;
                                Ship.SetActive(true);
                                
                                remove = shot;
                            }

                        }
                        else
                        {
                            GameObject token = (GameObject)Instantiate(missReference, transform);

                            token.transform.position = gridStore[hit.collider.gameObject.name].transform.position;

                            token.SetActive(true);

                            miss.Add(hit.collider.gameObject.name);

                            StartCoroutine(gameManager.Wait(2f));

                            gameManager.ChangePlayer();
                        }

                        if (remove != "")
                        {
                            ships.Remove(remove);
                        }

                    }
                }

            }
        }
        
    }
}
