using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObjectContainer : MonoBehaviour
{
    [field: SerializeField] public Button TakeButton { get; private set; }
    [field: SerializeField] public Button SellButton { get; private set; }

    [SerializeField] Image ContainerImage;
    [SerializeField] Image Icon;
    [SerializeField] TextMeshProUGUI Name;
    [SerializeField] TextMeshProUGUI sellPriceText;
    [SerializeField] Outline outline;
    [SerializeField] Transform containerParent;

    Vector3 originalScale = Vector3.one;

    public void Configue(ObjectDataSO data)
    {
        this.Icon.sprite = data.Icon;
        this.Name.text = data.Name;
        this.Name.color = ColorHolder.GetColor(data.RareRate);
        this.sellPriceText.text = data.SellPrice.ToString();
        this.ContainerImage.color = ColorHolder.GetColor(data.RareRate);
        this.outline.effectColor = ColorHolder.GetOutlineColor(data.RareRate);

        Dictionary<Stat, float> caculate = data.BaseData;
        StatContainerManager.Instance.CreatContainers(caculate, containerParent);
    }

}
