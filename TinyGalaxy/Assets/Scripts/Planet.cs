using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour {

    public int units = 1;

    public int productionLevel = 1;
    public int defenceLevel = 1;

    private TextMesh unitInfo;
    private Material planetMaterial;
    private Color planetColor;

    private void Awake()
    {
        unitInfo = transform.GetChild(0).GetComponent<TextMesh>();
        planetMaterial = gameObject.GetComponent<Renderer>().material;
        planetColor = planetMaterial.color;
        StartCoroutine(UnitProduction());
    }

    IEnumerator UnitProduction ()
    {
        while (true)
        {
            unitInfo.text = units.ToString();

            if (units <= 99)
                units++;

            yield return new WaitForSeconds(3f / (float)productionLevel);
        }
    }

    private void OnMouseDown()
    {
        if (GameManager.instance.highlightedPlanet != null)
            GameManager.instance.highlightedPlanet.DehighlightPlanet();

        GameManager.instance.highlightedPlanet = this;
        HighlightPlanet();
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
