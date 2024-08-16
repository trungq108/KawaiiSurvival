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
    [SerializeField] TextMeshProUGUI price;
    [SerializeField] Transform statContainerParent;
    [field: SerializeField] public Button Button {  get; private set; }

    [SerializeField] Button lockButton;
    [SerializeField] Image lockImage;
    [SerializeField] Sprite lockSprite, unlockSprite;
    public bool IsLock { get; private set; }

    public void Confingue(WeaponDataSO weaponData, int level)
    {
        this.icon.sprite = weaponData.WeaponSprite;
        this.name.text = weaponData.WeaponName + "(Lv " + (level + 1).ToString() + ")";
        this.name.color = ColorHolder.GetColor(level);
        this.containerBG.color = ColorHolder.GetColor(level);
        this.outline.effectColor = ColorHolder.GetOutlineColor(level);
        this.price.text = weaponData.WeaponPrice.ToString();

        Dictionary<Stat, float> caculate = WeaponStatCaculator.Caculator(weaponData, level);
        StatContainerManager.Instance.CreatContainers(caculate, statContainerParent);

        lockImage.sprite = unlockSprite; 
        lockButton.onClick.AddListener(() => LockItemReroll());
    }

    public void Configue(ObjectDataSO objectData)
    {
        this.icon.sprite = objectData.Icon;
        this.name.text = objectData.Name;
        this.name.color = ColorHolder.GetColor(objectData.RareRate);
        this.price.text = objectData.BuyPrice.ToString();
        this.containerBG.color = ColorHolder.GetColor(objectData.RareRate);
        this.outline.effectColor = ColorHolder.GetOutlineColor(objectData.RareRate);

        Dictionary<Stat, float> caculate = objectData.BaseData;
        StatContainerManager.Instance.CreatContainers(caculate, statContainerParent);

        lockImage.sprite = unlockSprite;
        lockButton.onClick.AddListener(() => LockItemReroll());
    }

    private void LockItemReroll()
    {
        IsLock = !IsLock;
        lockImage.sprite = IsLock ? lockSprite : unlockSprite;
    }

}
