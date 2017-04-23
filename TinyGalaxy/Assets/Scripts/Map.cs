using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {

    public List<Planet> planets = new List<Planet>();

	// Use this for initialization
	void Awake ()
    {
        AddPlanetsToList();
	}

    void AddPlanetsToList ()
    {
        foreach(Transform child in transform.GetChild(0))
        {
            planets.Add(child.GetComponent<Planet>());
        }
    }
}
