using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "My Assets/GrassSpawnParameters")]
public class GrassSpawnParameters : ScriptableObject
{
    [Header("Grass Blocks")]
    [SerializeField] public GrassBlock grassBlockPrefab;
    [SerializeField] public int grassBlocksMin;
    [SerializeField] public int grassBlocksMax;

    [Header("Grass")]
    [SerializeField] public Grass grassPrefab;
    [SerializeField] public int grassAmountMin;
    [SerializeField] public int grassAmountMax;
}
