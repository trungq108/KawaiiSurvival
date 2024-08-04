using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    [SerializeField] Image upgradeIcon;
    [SerializeField] TextMeshProUGUI upgradeName;
    [SerializeField] TextMeshProUGUI upgradeValue;
    [field: SerializeField] public Button Button { get; private set; }

    public void Configure(Image upgradeIcon, string upgradeName, string upgradeValue)
    {
        this.upgradeIcon = upgradeIcon;
        this.upgradeName.text = upgradeName;
        this.upgradeValue.text = upgradeValue;
    }
}
