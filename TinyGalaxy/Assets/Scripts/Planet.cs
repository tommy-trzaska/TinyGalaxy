using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour {

    public int units = 1;

    public int productionLevel = 1;
    public int defenceLevel = 1;

    private TextMesh unitInfo;

    private void Awake()
    {
        unitInfo = transform.GetChild(0).GetComponent<TextMesh>();
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
}
