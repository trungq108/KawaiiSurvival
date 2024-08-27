using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoSlide : MonoBehaviour
{
    [SerializeField] private Image itemIcon;
    [SerializeField] private Image itemBG;
    private int recyclePrice;
    [SerializeField] private TextMeshProUGUI recyclePriceText;
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private Transform startContainerParent;

    public void ConfigueWeapon(Weapon weapon)
    {
        this.itemIcon.sprite = weapon.Data.WeaponIcon;
        this.itemBG.color = ColorHolder.GetColor(weapon.weaponLevel);
        this.recyclePrice = Calculator.WeaponRecyclePrice(weapon.Data, weapon.weaponLevel);
        this.recyclePriceText.text = recyclePrice.ToString();
        this.itemNameText.text = weapon.Data.WeaponName;

        Dictionary<Stat, float> caculate = Calculator.WeaponStats(weapon.Data, weapon.weaponLevel);
        StatContainerManager.Instance.CreatContainers(caculate, startContainerParent);
    }

    public void ConfigueObject(ObjectDataSO objectData)
    {
        this.itemIcon.sprite = objectData.Icon;
        this.itemBG.color = ColorHolder.GetColor(objectData.RareRate);
        this.recyclePrice = objectData.SellPrice;
        this.recyclePriceText.text = recyclePrice.ToString();
        this.itemNameText.text = objectData.Name;

        Dictionary<Stat, float> caculate = objectData.BaseData;
        StatContainerManager.Instance.CreatContainers(caculate, startContainerParent);
    }
}
