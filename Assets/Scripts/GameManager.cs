using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public int players = 2;

    // Start is called before the first frame update
    void Start()
    {

        GameObject refrencePlayer = (GameObject)Instantiate(Resources.Load("Player"));

        for (int i = 1; i <= players; i++)
        {

            GameObject player = (GameObject)Instantiate(refrencePlayer);

            player.name = "Player" + i.ToString();

            if (i == 1)
            {
                player.SetActive(true);
            } else
            {
                player.SetActive(false);
            }

            SceneManager.MoveGameObjectToScene(player, SceneManager.GetSceneByName("MainScene"));

        }

        Destroy(refrencePlayer);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
