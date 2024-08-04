using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSelectButton : MonoBehaviour
{
    [SerializeField] Image buttonImage;
    [SerializeField] Image weaponIcon;
    [SerializeField] TextMeshProUGUI weaponName;
    [field: SerializeField] public Button Button { get; private set; }
    Vector3 originalScale = Vector3.one;

    public void Configue(Sprite weaponIcon, string weaponName, int Level)
    {   
        this.weaponIcon.sprite = weaponIcon;
        this.weaponName.text = weaponName;
        this.buttonImage.color = ColorHolder.GetColor(Level);
    }

    public void Select()
    {
        Button.transform.DOScale(transform.localScale * 1.2f, 0.3f).SetEase(Ease.InOutBack);
    }

    public void DeSelect()
    {
        Button.transform.DOScale(originalScale, 0.3f).SetEase(Ease.InOutBack);
    }
}
