using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] TextMeshPro damageText;

    public void Trigger(int damage, bool isCritical)
    {
        animator.Play("Trigger");
        damageText.text = damage.ToString();
        damageText.color = Color.white;
        if (isCritical)
        {
            damageText.color = Color.red;
        }
    }
    
    public void Trigger(string dodge)
    {
        animator.Play("Trigger");
        damageText.text = dodge;
    }
}
