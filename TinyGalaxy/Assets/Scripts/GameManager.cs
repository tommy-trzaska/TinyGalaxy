using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public Map map;
    public List<Player> players = new List<Player>();
    public int[] startIndexes; //indexes of start planets for each player

    public GameObject endGameScreen;
    public Text endGameText;

    public Planet highlightedPlanet;
    public bool pointerHold = false;

	void Start ()
    {
        if (instance != null)
            Destroy(gameObject);

        instance = this;

        endGameScreen.SetActive(false);
        SetupPlayers();
	}

    //Setting start locations for players
    void SetupPlayers ()
    {
        for (int i = 0; i < players.Count; i++)
        {
            map.planets[startIndexes[i]].SetNewOwner(players[i]);
        }
    }

    public void CheckPlayersAlive ()
    {
        Player lastFoundPlayer = null;
        int playersAlive = 0;

        foreach (Player p in players)
        {
            if(p.ownedPlanets.Count > 0)
            {
                lastFoundPlayer = p;
                playersAlive++;
            }
        }

        if(playersAlive == 1)
        {
            Time.timeScale = 0;
            Debug.Log("game ended");
            ShowWinner(lastFoundPlayer);
        }
    }

    void ShowWinner (Player winner)
    {
        endGameScreen.SetActive(true);

        endGameText.color = new Color(winner.playerColor.r, winner.playerColor.g, winner.playerColor.b, 1.0f);
        endGameText.text = winner.name + " won!";
    }
}
