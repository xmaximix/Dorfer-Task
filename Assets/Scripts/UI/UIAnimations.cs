using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

[System.Serializable]
public class UIAnimations
{
    [HideInInspector] public Vector3 defaultMoneyTextPosition;
    [HideInInspector] public Vector3 coinsDestinationPosition;
    [HideInInspector] public GameUI gameUI;

    public IEnumerator AnimateCoin(Image coin)
    {
        coin.DOFade(1, .2f);
        Tween tween = coin.rectTransform.DOAnchorPos(coinsDestinationPosition, 1.3f);
        yield return tween.WaitForPosition(.8f);
        coin.DOFade(0, 0.3f);
    }

    public IEnumerator AnimateMoneyText(TextMeshProUGUI moneyText)
    {
        Sequence sequence = DOTween.Sequence();
        moneyText.rectTransform.anchoredPosition = defaultMoneyTextPosition;
        sequence.Append(moneyText.rectTransform.DOShakePosition(.5f, 5, 30));
        sequence.Append(moneyText.rectTransform.DOAnchorPos(defaultMoneyTextPosition, .05f));
        sequence.Play();
        for (float t = 0; t < 1; t += Time.deltaTime * 4)
        {
            var curMoney = (int)Mathf.Lerp(int.Parse(moneyText.text), gameUI.money, t);
            moneyText.text = curMoney.ToString();
            yield return new WaitForSeconds(.05f);
        }
        moneyText.text = gameUI.money.ToString();
    }
}
