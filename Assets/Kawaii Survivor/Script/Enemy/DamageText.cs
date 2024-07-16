using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] TextMeshPro damageText;

    public void Trigger(int damage)
    {
        animator.Play("Trigger");
        damageText.text = damage.ToString();
    }
}
