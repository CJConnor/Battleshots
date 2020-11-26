using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public int playerCount = 2;
    public List<GameObject> players;
    private GameObject CurrentActivePlayer;

    // Start is called before the first frame update
    void Start()
    {

        GameObject refrencePlayer = (GameObject)Instantiate(Resources.Load("Player"));

        for (int i = 1; i <= playerCount; i++)
        {

            GameObject player = (GameObject)Instantiate(refrencePlayer);

            player.name = "Player" + i.ToString();

            if (i == 1)
            {
                player.SetActive(true);
                CurrentActivePlayer = player;
            } else
            {
                player.SetActive(false);
            }

            SceneManager.MoveGameObjectToScene(player, SceneManager.GetSceneByName("MainScene"));

            players.Add(player);

        }

        Destroy(refrencePlayer);
        
    }

    public void ChangePlayer()
    {

        CurrentActivePlayer.SetActive(false);

        int index = Int32.Parse(CurrentActivePlayer.name.Remove(0, 6)) - 1;

        index = index + 1 > playerCount - 1 ? 0 : index + 1;

        CurrentActivePlayer = players[index];

        CurrentActivePlayer.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
