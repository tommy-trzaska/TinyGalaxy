using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public Planet highlightedPlanet;
    public bool pointerHold = false;

	void Awake ()
    {
        if (instance != null)
            Destroy(gameObject);

        instance = this;
	}
}
