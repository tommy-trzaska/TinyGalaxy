using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public Map map;
    public List<Player> players = new List<Player>();
    public int[] startIndexes; //indexes of start planets for each player

    public Planet highlightedPlanet;
    public bool pointerHold = false;

	void Start ()
    {
        if (instance != null)
            Destroy(gameObject);

        instance = this;

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
}
