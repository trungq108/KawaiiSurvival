using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CurrencyManager : Singleton<CurrencyManager>
{
    [field: SerializeField] public int Currency {  get; private set; }

    public void Add(int sellPrice)
    {
        Currency += sellPrice;
    }
}
