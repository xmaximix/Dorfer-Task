using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class GameUI : MonoBehaviour
{
    [SerializeField] Joystick joystick;

    [SerializeField] Canvas canvas;

    [SerializeField] Image coinImage;
    [SerializeField] RectTransform coinSpawnPoint;
    [SerializeField] RectTransform coinDestination;

    [SerializeField] TextMeshProUGUI blocksText;
    [SerializeField] TextMeshProUGUI moneyText;

    [SerializeField] UIAnimations uIAnimations;

    [HideInInspector] public int money;
    private int blocks;

    private void Awake()
    {
        PlayerInput.InitJoystick(joystick);
        DOTween.SetTweensCapacity(500, 50);
    }

    private void Start()
    {
        moneyText.text = "0";
        uIAnimations.defaultMoneyTextPosition = moneyText.rectTransform.anchoredPosition;
        uIAnimations.coinsDestinationPosition = coinDestination.anchoredPosition;
        uIAnimations.gameUI = this;
        UpdateBlocksText();

        EventManager.OnGrassBlockCollected.AddListener(CollectBlock);
        EventManager.OnGrassBlockSold.AddListener(SellBlock);
    }

    private void SellBlock()
    {
        money += 15;
        blocks--;
        UpdateBlocksText();
        Image coin = SpawnCoin();
        StartCoroutine(uIAnimations.AnimateCoin(coin));
        Destroy(coin.gameObject, 3f);
        StartCoroutine(uIAnimations.AnimateMoneyText(moneyText));
    }

    private Image SpawnCoin()
    {
        var spawnPosition = (Vector2)coinSpawnPoint.position + new Vector2(Random.Range(-100, 100), Random.Range(-100, 100));
        var coin = Instantiate(coinImage, spawnPosition, Quaternion.identity, canvas.transform);
        return coin;
    }

    private void UpdateBlocksText()
    {
        blocksText.text = blocks.ToString() + "/" + 40;
    }

    private void CollectBlock()
    {
        blocks++;
        UpdateBlocksText();
    }
}
