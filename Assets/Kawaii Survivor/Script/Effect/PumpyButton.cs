using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PumpyButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    private Button button;
    private void Awake() => button = GetComponent<Button>();

    public void OnPointerDown(PointerEventData eventData)
    {
        DOTween.Kill(button.gameObject);
        button.transform.DOScale(transform.localScale * 1.25f, 0.25f)
                        .SetEase(Ease.OutCubic);

    }
    public void OnPointerUp(PointerEventData eventData)
    {
        DOTween.Kill(button.gameObject);
        button.transform.DOScale(Vector3.one, 0.25f)
                        .SetEase(Ease.OutCubic);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        DOTween.Kill(button.gameObject);
        button.transform.DOScale(Vector3.one, 0.25f)
                        .SetEase(Ease.OutCubic);
    }
}
