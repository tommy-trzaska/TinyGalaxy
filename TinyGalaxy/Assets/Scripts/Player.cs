﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player
{
    public Color playerColor;
    public List<Planet> ownedPlanets = new List<Planet>();
}