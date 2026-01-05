using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Instantiate game variables
    public int playerCount = 2;
    public List<GameObject> players;
    public IDictionary<string, IDictionary<string, List<string>>> ships = new Dictionary<string, IDictionary<string, List<string>>>();
    // public List<string> points;
    // public IDictionary<string, List<string>> shipPoints = new Dictionary<string, List<string>>();
    public GameObject button;
    public string gameState = "placement";
    private GameObject CurrentActivePlayer;

    // Start is called before the first frame update
    void Start()
    {

        // Instantiate reference player
        GameObject refrencePlayer = (GameObject)Instantiate(Resources.Load("Player"));

        // Loop through player count
        for (int i = 1; i <= playerCount; i++)
        {

            // Use reference player o instantiate a new player
            GameObject player = (GameObject)Instantiate(refrencePlayer);

            // Create new player name
            player.name = "Player" + i.ToString();

            // If player one set to active
            if (i == 1)
            {
                player.SetActive(true);
                CurrentActivePlayer = player;

            // Else set it to false
            } else
            {
                player.SetActive(false);
            }

            // Move Players to main scene
            SceneManager.MoveGameObjectToScene(player, SceneManager.GetSceneByName("MainScene"));

            // Add player object to the player list
            players.Add(player);

        }

        // Destroy reference player
        Destroy(refrencePlayer);
        
    }

    // Get ship positions
    public void GetPlayerShips()
    {

        // Loop through players
        for (int i = 0; i < players.Count; i++)
        {

            // If player is active
            if (players[i].gameObject.activeSelf)
            {

                IDictionary<string, List<string>> shipPoints = new Dictionary<string, List<string>>();
                List<string> points = new List<string>();

                // For each player object 
                foreach (Transform child in players[i].gameObject.transform)
                {

                    // If points is full clear it
                    if (points.Count > 0)
                    {
                        points.Clear();
                    }

                    // Get the box collider for the player
                    BoxCollider2D boxCollider = child.gameObject.GetComponent<BoxCollider2D>();

                    // Find which sprites it overlaps 
                    Collider2D[] overlap = Physics2D.OverlapAreaAll(boxCollider.bounds.min, boxCollider.bounds.max);

                    // If it has found any overlaps
                    if (overlap.Length > 1)
                    {

                        // Loop through overlaps
                        for (int j = 0; j < overlap.Length; j++)
                        {

                            // If the layer is equal to the water  then add the name of it to the points list
                            if (overlap[j].gameObject.layer.Equals(4))
                            {
                                GameObject tile = overlap[j].gameObject;

                                points.Add(tile.name.ToString());
                            
                            }

                        }

                        List<string> p = points;

                        // Add the points and ship name to the ships points dictionary
                        shipPoints.Add(child.gameObject.name, p);

                        Destroy(child.gameObject);

                    }


                }

                // Add the players ships to the ships dictionary 
                ships.Add(players[i].gameObject.name, shipPoints);

                // If the last player then change player ship movement 
                if (i == playerCount - 1)
                {
                    StartGame();
                } else
                {
                    if (i == playerCount - 2)
                    {
                        button.gameObject.GetComponentInChildren<Text>().text = "Start Game";
                    }
                    ChangePlayer();
                    break;
                }

            }

        }

    }

    // Function to change the active player
    public void ChangePlayer()
    {

        // Set current player to false
        CurrentActivePlayer.SetActive(false);

        // Get the next player in line
        int index = Int32.Parse(CurrentActivePlayer.name.Remove(0, 6)) - 1;

        index = index + 1 > playerCount - 1 ? 0 : index + 1;

        // Set new current player and set them to active
        CurrentActivePlayer = players[index];

        CurrentActivePlayer.SetActive(true);

    }

    public void StartGame()
    {

        button.SetActive(false);

        // Loop through players
        for (int i = 0; i < players.Count; i++)
        {

            players[i].SetActive(false);

            // Get the next player in line
            int index = Int32.Parse(CurrentActivePlayer.name.Remove(0, 6)) - 1;

            index = index + 1 > playerCount - 1 ? 0 : index + 1;

            players[i].GetComponent<HitScript>().enabled = true;

            // For each player object 
            foreach (Transform child in players[i].gameObject.transform)
            {
                child.gameObject.SetActive(false);
                child.GetComponent<ShipMovement>().enabled = false;
            }

        }

        players[0].SetActive(true);

        CurrentActivePlayer = players[0];

    }

    public IEnumerator Wait(float seconds)
    {
        while(true)
        {
            yield return new WaitForSeconds(seconds);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
