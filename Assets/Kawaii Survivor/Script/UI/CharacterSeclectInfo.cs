using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSeclectInfo : MonoBehaviour
{
    [SerializeField] private Transform statsContainerParent;
    [SerializeField] private TextMeshProUGUI charName;
    [SerializeField] private TextMeshProUGUI priceText;
    [field: SerializeField] public Button PurchaseButton { get; private set; }

    public void Configue(CharacterSelectContainer container, Action PurchaseCallBack)
    {
        CharacterDataSO data = container.Data;

        charName.text = data.PlayerName;
        priceText.text = data.PlayerPrice.ToString();
        StatContainerManager.Instance.CreatContainers(data.NonNetralStats, statsContainerParent);

        PurchaseButton.gameObject.SetActive(!container.isPurchase);
        PurchaseButton.interactable = CurrencyManager.IsEnoughMoney(container.Data.PlayerPrice);
        PurchaseButton.onClick.RemoveAllListeners();
        PurchaseButton.onClick.AddListener(() => PurchaseCallBack?.Invoke());
    }

}
