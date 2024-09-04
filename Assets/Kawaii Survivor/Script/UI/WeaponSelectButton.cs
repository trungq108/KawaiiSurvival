using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSelectButton : MonoBehaviour
{
    [field: SerializeField] public Button Button { get; private set; }

    [SerializeField] Image buttonImage;
    [SerializeField] Image weaponIcon;
    [SerializeField] TextMeshProUGUI weaponName;
    [SerializeField] Outline outline;
    [SerializeField] Transform statContainerParent;

    Vector3 originalScale = Vector3.one;

    public void Configue(WeaponDataSO data, int Level)
    {   
        this.weaponIcon.sprite = data.WeaponIcon;
        this.weaponName.text = data.WeaponName + "(Lv " + (Level + 1).ToString() + ")";
        this.weaponName.color = ColorHolder.GetColor(Level);
        this.buttonImage.color = ColorHolder.GetColor(Level);
        this.outline.effectColor = ColorHolder.GetOutlineColor(Level);

        Dictionary<Stat, float> caculate = Calculator.WeaponStats(data, Level);
        StatContainerManager.Instance.CreatContainers(caculate, statContainerParent);
    }

    public void Select()
    {
        DOTween.Kill(this.gameObject);
        Button.transform.localScale = originalScale;
        Button.transform.DOScale(originalScale * 1.25f, 0.25f).SetEase(Ease.InCirc);
    }

    public void DeSelect()
    {
        DOTween.Kill(this.gameObject);
        Button.transform.DOScale(originalScale, 0.25f).SetEase(Ease.InCirc);
    }
}
