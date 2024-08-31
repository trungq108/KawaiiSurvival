using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectContainer : MonoBehaviour
{
    [SerializeField] private Image charIcon;
    [SerializeField] private GameObject lockIcon;
    [field: SerializeField] public Button Button { get; private set; }
    public CharacterDataSO Data { get; private set; }
    public bool isPurchase {  get; private set; }

    public void Configue(CharacterDataSO data)
    {
        Data = data;
        charIcon.sprite = Data.PlayerSprite;
        isPurchase = ES3.Load<bool>("IsPurchase_" + Data.PlayerName, false);

        if(Data.PlayerName == "Dave")
        {
            isPurchase = true;
        }

        if (isPurchase)
        {
            charIcon.color = Color.white;
            lockIcon.SetActive(false);
        }
        else
        {
            charIcon.color = Color.gray;
            lockIcon.SetActive(true);
        }
    }

    public void SetPurchase()
    {
        isPurchase = true;
        charIcon.color = Color.white;
        lockIcon.SetActive(false);
    }

    public void Equip()
    {
        transform.DOScale(Vector3.one * 1.25f, 0.5f)
            .SetLoops(-1, LoopType.Yoyo);
    }

    public void UnEquip()
    {
        DOTween.Kill(this.transform);
        transform.localScale = Vector3.one;
    }
}
