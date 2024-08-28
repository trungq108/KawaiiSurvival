using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemContainer : MonoBehaviour
{
    [SerializeField] Image item_BG;
    [SerializeField] Image item_Icon;
    [field: SerializeField] public Button Button {  get; private set; }

    public Weapon weaponInfo {  get; private set; }
    public ObjectDataSO objectInfo { get; private set; }

    public void Configue(Weapon weapon)
    {
        weaponInfo = weapon;
        item_Icon.sprite = weaponInfo.Data.WeaponIcon;
        item_BG.color = ColorHolder.GetColor(weapon.weaponLevel);
    }

    public void Configue(ObjectDataSO objectData)
    {
        objectInfo = objectData;
        item_Icon.sprite = objectData.Icon;
        item_BG.color = ColorHolder.GetColor(objectData.RareRate);

    }
}
