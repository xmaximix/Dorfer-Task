using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GrassZone : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;

    [SerializeField] GrassBlock grassBlockPrefab;

    [HideInInspector] public int currentGrassCount;
    [HideInInspector] public GrassSpawner grassSpawner;

    private IEnumerator WaitAndSpawnGrass(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            text.text = i + "...";
            yield return new WaitForSeconds(1);
        }
        grassSpawner.SpawnGrass();
    }

    public void OnAllGrassDestroyed()
    {
        StartCoroutine(WaitAndSpawnGrass(10));
        SpawnGrassBlocks(Random.Range(4,8));
    }
    
    private void SpawnGrassBlocks(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Instantiate(grassBlockPrefab, transform.position, Quaternion.identity, transform);
        }
    }

    public void UpdateText()
    {
        text.text = currentGrassCount.ToString();
    }
}
