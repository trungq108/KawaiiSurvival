using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemContainer : MonoBehaviour
{

    [SerializeField] Image containerBG;
    [SerializeField] Image icon;
    [SerializeField] Outline outline;
    [SerializeField] TextMeshProUGUI name;
    public int Price {  get; private set; }
    [SerializeField] TextMeshProUGUI priceText;
    [SerializeField] Transform statContainerParent;
    [field: SerializeField] public Button Button {  get; private set; }

    [SerializeField] Button lockButton;
    [SerializeField] Image lockImage;
    [SerializeField] Sprite lockSprite, unlockSprite;
    public bool IsLock { get; private set; }

    public WeaponDataSO WeaponData { get; private set; }
    public ObjectDataSO ObjectData { get; private set; }

    public void Confingue(WeaponDataSO weaponData, int level)
    {
        this.icon.sprite = weaponData.WeaponSprite;
        this.name.text = weaponData.WeaponName + "(Lv " + (level + 1).ToString() + ")";
        this.name.color = ColorHolder.GetColor(level);
        this.containerBG.color = ColorHolder.GetColor(level);
        this.outline.effectColor = ColorHolder.GetOutlineColor(level);
        this.Price = Calculator.WeaponPrice(weaponData, level);
        this.priceText.text = Price.ToString(); 
        this.WeaponData = weaponData;

        Dictionary<Stat, float> caculate = Calculator.WeaponStats(weaponData, level);
        StatContainerManager.Instance.CreatContainers(caculate, statContainerParent);

        lockImage.sprite = unlockSprite; 
        lockButton.onClick.AddListener(() => LockItemReroll());

        Button.interactable = CurrencyManager.IsEnoughMoney(Price);
    }

    public void Configue(ObjectDataSO objectData)
    {
        this.icon.sprite = objectData.Icon;
        this.name.text = objectData.Name;
        this.name.color = ColorHolder.GetColor(objectData.RareRate);
        this.Price = objectData.BuyPrice;
        this.priceText.text = Price.ToString();
        this.containerBG.color = ColorHolder.GetColor(objectData.RareRate);
        this.outline.effectColor = ColorHolder.GetOutlineColor(objectData.RareRate);
        this.ObjectData = objectData;

        Dictionary<Stat, float> caculate = objectData.BaseData;
        StatContainerManager.Instance.CreatContainers(caculate, statContainerParent);

        lockImage.sprite = unlockSprite;
        lockButton.onClick.AddListener(() => LockItemReroll());

        Button.interactable = CurrencyManager.IsEnoughMoney(Price);
    }

    private void LockItemReroll()
    {
        IsLock = !IsLock;
        lockImage.sprite = IsLock ? lockSprite : unlockSprite;
    }
}
