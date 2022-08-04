using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Backpack : MonoBehaviour
{
    [HideInInspector] public int maxBlocks = 40;
    [HideInInspector] public int blocksInside = 0;
    [HideInInspector] public int pickedBlocks = 0;
    private int heightCount = 0;
    GrassBlock[] grassBlocks;
    [HideInInspector] public bool selling;

    private void Start()
    {
        grassBlocks = new GrassBlock[maxBlocks];
    }

    public void AddBlock(GrassBlock grassBlock)
    {
        EventManager.SendGrassBlockCollected();
        MoveBlockInsideBackpack(grassBlock);
        grassBlocks[blocksInside] = grassBlock;
        blocksInside++;
    }

    private void MoveBlockInsideBackpack(GrassBlock grassBlock)
    {
        float zOffset, yOffset;
        CalculatePositionOffset(out zOffset, out yOffset);
        grassBlock.transform.DOLocalMoveY(yOffset, 0.4f);
        grassBlock.transform.DOLocalMoveZ(zOffset, 0.4f);
    }

    private void CalculatePositionOffset(out float zOffset, out float yOffset)
    {
        zOffset = 0;
        yOffset = 0.2f * heightCount;

        if (blocksInside % 2 == 0 && blocksInside != 0)
        {
            heightCount++;
            yOffset = 0.2f * heightCount;
        }

        if (blocksInside % 2 == 1)
        {
            zOffset = -0.4f;
        }
    }

    public void SellBlocks(Transform hangar)
    {
        if (selling)
        {
            return;
        }
        StartCoroutine(SellWithDelay(hangar));
    }

    IEnumerator SellWithDelay(Transform hangar)
    {
        selling = true;
        for (int i = blocksInside - 1; i >= 0; i--)
        {
            MoveBlockToHangar(hangar, i);
            Destroy(grassBlocks[i].gameObject, 3f);
            grassBlocks[i] = null;
            EventManager.SendGrassBlockSold();
            yield return new WaitForSeconds(0.15f);
        }
        ResetBackpack();
        selling = false;
    }

    private void ResetBackpack()
    {
        blocksInside = 0;
        heightCount = 0;
    }

    private void MoveBlockToHangar(Transform hangar, int i)
    {
        grassBlocks[i].transform.parent = null;
        grassBlocks[i].transform.DOMove(hangar.position, 2f);
        grassBlocks[i].transform.DORotate(hangar.rotation.eulerAngles, 2f);
    }
}
