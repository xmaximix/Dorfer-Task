using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour
{
    [HideInInspector] public GrassZone grassZone;

    public void Destroy()
    {
        grassZone.currentGrassCount--;
        grassZone.UpdateText();
        if (grassZone.currentGrassCount <= 0)
        {
            grassZone.currentGrassCount = 0;
            grassZone.OnAllGrassDestroyed();
        }
        Destroy(gameObject);
    }
}
