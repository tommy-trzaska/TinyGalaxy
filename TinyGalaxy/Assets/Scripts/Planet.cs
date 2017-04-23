using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour {

    public int units = 1;

    public int productionLevel = 1;
    public int defenceLevel = 1;

    public Player owner = null;
    public List<Planet> connections;
    public GameObject fleetPrefab;

    private TextMesh unitInfo;
    private Material planetMaterial;
    private Color planetColor;
    private bool producingUnits = false;

    private void Awake()
    {
        unitInfo = transform.GetChild(0).GetComponent<TextMesh>();
        planetMaterial = gameObject.GetComponent<Renderer>().material;
        planetColor = planetMaterial.color;
    }

    private void OnMouseDown()
    {
        if (GameManager.instance.highlightedPlanet != null)
            GameManager.instance.highlightedPlanet.DehighlightPlanet();

        GameManager.instance.highlightedPlanet = this;
        GameManager.instance.pointerHold = true;
        HighlightPlanet();
    }

    private void OnMouseUp()
    {
        if (!GameManager.instance.pointerHold)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, 100.0f) && GameManager.instance.players.Contains(owner))
        {
            if(hit.collider.gameObject.GetComponent<Planet>())
            {
                Planet hitPlanet = hit.collider.gameObject.GetComponent<Planet>();

                if(connections.Contains(hitPlanet))
                {
                    int unitsToSend = units / 2;

                    GameObject newFleet = Instantiate(fleetPrefab);
                    newFleet.GetComponent<Fleet>().CreateFleet(unitsToSend, owner, transform.position, hitPlanet.transform);

                    units -= unitsToSend;
                    UpdateText();

                    GameManager.instance.highlightedPlanet = null;
                    DehighlightPlanet();
                }
            }
        }

        GameManager.instance.pointerHold = false;
    }

    IEnumerator UnitProduction()
    {
        producingUnits = true;

        while (true)
        {
            UpdateText();

            if (units <= 99)
                units++;

            yield return new WaitForSeconds(3f / (float)productionLevel);
        }
    }

    public void SetNewOwner (Player p)
    {
        p.ownedPlanets.Add(this);
        owner = p;

        planetColor = p.playerColor;
        planetMaterial.color = planetColor;

        if (!producingUnits)
            StartCoroutine(UnitProduction());
    }

    public void UpdateText ()
    {
        unitInfo.text = units.ToString();
    }

    void HighlightPlanet ()
    {
        planetMaterial.SetColor("_SpecColor", new Color(1.0f, 1.0f, 100.0f/256.0f) * planetColor);
    }

    public void DehighlightPlanet ()
    {
        planetMaterial.SetColor("_SpecColor", Color.black);
    }
}
