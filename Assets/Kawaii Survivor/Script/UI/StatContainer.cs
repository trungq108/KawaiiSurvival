using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatContainer : MonoBehaviour
{
    [SerializeField] Image statIcon;
    [SerializeField] TextMeshProUGUI statName;
    [SerializeField] TextMeshProUGUI statValue;

    public void Confingue(Sprite icon, string name, float value)
    {
        this.statIcon.sprite = icon;
        this.statName.text = name;
        this.statValue.text = ((int)value).ToString();
    }
}
