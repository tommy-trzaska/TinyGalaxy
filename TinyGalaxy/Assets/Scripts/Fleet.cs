using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fleet : MonoBehaviour {

    public int units = 10;
    public Planet target;
    public Player owner;
    public float speed = 5;

    private TextMesh unitInfo;
    private Vector3 direction;

    private void Awake()
    {
        unitInfo = transform.GetChild(0).GetComponent<TextMesh>();
    }

    private void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    public void CreateFleet (int fleetSize, Player commander, Vector3 fleetOrigin, Transform fleetTarget)
    {
        units = fleetSize;
        unitInfo.text = units.ToString();
        owner = commander;
        GetComponent<Renderer>().material.color = commander.playerColor;

        target = fleetTarget.GetComponent<Planet>();

        transform.position = fleetOrigin;
        direction = (fleetTarget.position - fleetOrigin).normalized;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Planet>())
        {
            if(other.GetComponent<Planet>() == target)
            {
                if (!GameManager.instance.players.Contains(other.GetComponent<Planet>().owner))
                    other.GetComponent<Planet>().SetNewOwner(owner);
                //TODO: Combat

                other.GetComponent<Planet>().units += units;
                other.GetComponent<Planet>().UpdateText();
                Destroy(gameObject);
            }
        }
    }
}
