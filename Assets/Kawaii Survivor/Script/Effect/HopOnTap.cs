using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HopOnTap : MonoBehaviour, IPointerDownHandler
{
    private Button button;
    private RectTransform rt;
    private Vector2 originalPosition;

    private void Awake()
    {
        button = GetComponent<Button>();
        rt = GetComponent<RectTransform>();
        originalPosition = rt.anchoredPosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        DOTween.Kill(button.gameObject);
        //button.transform.DOMoveY(transform.position.y + 100f, 0.5f)
        //    .SetEase(Ease.InBack)
        //    .OnComplete(() =>
        //{
        //    button.transform.DOMoveY(originalPosition.y, 0.5f).SetEase(Ease.InBack);
        //});

        rt.DOAnchorPos(originalPosition + Vector2.up * 25f, 0.1f)
            .SetEase(Ease.Flash)
            .OnComplete(() =>
            {
                rt.DOAnchorPos(originalPosition, 0.1f).SetEase(Ease.Flash);
            }); ;
    }
}
