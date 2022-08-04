using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GrassSpawner : MonoBehaviour
{
    [SerializeField] public GrassSpawnParameters grassSpawnParameters;
    private Grass grassPrefab;
    private int grassAmountMin;
    private int grassAmountMax;
    [SerializeField] GrassZone grassZone;

    private Vector3 spawnArea;
    private Vector3 spawnPosition;
    private List<Grass> spawnedGrass = new List<Grass>();
    private string sliceTag = "Sliceable";

    private void Awake()
    {
        grassZone.grassSpawner = this;
    }

    private void Start()
    {
        grassPrefab = grassSpawnParameters.grassPrefab;
        grassAmountMin = grassSpawnParameters.grassAmountMin;
        grassAmountMax = grassSpawnParameters.grassAmountMax;

        var zoneCollider = grassZone.GetComponent<Collider>();
        spawnArea = zoneCollider.bounds.size * .8f;
        SpawnGrass();
    }

    public void SpawnGrass()
    {
        spawnedGrass.Clear();
        grassZone.currentGrassCount = Random.Range(grassAmountMin, grassAmountMax + 1);
        grassZone.UpdateText();

        for (int i = 0; i < grassZone.currentGrassCount; i++)
        {
            CalculateRandomPositionInsideSpawnArea();
            bool uniqePos = true;
            int attempsCount = 0;
            while (true)
            {
                attempsCount++;
                foreach (Grass grass in spawnedGrass)
                {
                    if (Vector3.Distance(spawnPosition, grass.transform.position) < .1f)
                    {
                        uniqePos = false;
                    }
                }
                if (uniqePos || attempsCount >= 10)
                {
                    break;
                }
                CalculateRandomPositionInsideSpawnArea();
                uniqePos = true;
            }

            if (attempsCount < 10)
            {
                Spawn();
            }
        }
    }

    private void Spawn()
    {
        var newGrass = Instantiate(grassPrefab, spawnPosition, Quaternion.Euler(0, Random.Range(-90, 91), 0), transform);
        var time = Random.Range(0.5f, 2f);
        newGrass.transform.DOMoveY(.8f, time);
        newGrass.grassZone = grassZone;
        StartCoroutine(EnableSlicingAfterTime(time, newGrass));
        spawnedGrass.Add(newGrass);
    }

    IEnumerator EnableSlicingAfterTime(float time, Grass grass)
    {
        yield return new WaitForSeconds(time);
        grass.gameObject.layer = LayerMask.NameToLayer(sliceTag);
    }

    private void CalculateRandomPositionInsideSpawnArea()
    {
        var xPosition = Random.Range(-spawnArea.x / 2, spawnArea.x / 2);
        var yPosition = -2;
        var zPosition = Random.Range(-spawnArea.z / 2, spawnArea.z / 2);
        spawnPosition = new Vector3(xPosition, yPosition, zPosition) + transform.position;
    }
}
