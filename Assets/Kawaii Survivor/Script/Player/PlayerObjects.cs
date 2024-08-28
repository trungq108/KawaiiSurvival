using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObjects : MonoBehaviour
{
    public List<ObjectDataSO> Objects = new List<ObjectDataSO>();

    public void AddObject(ObjectDataSO objectData)
    {
        Objects.Add(objectData);
        PlayerStatManager.Instance.AddObject(objectData.BaseData);
        InventoryManager.Instance.Configue();
    }

    public void RemoveObject(ObjectDataSO objectData)
    {
        Objects.Remove(objectData);
        PlayerStatManager.Instance.RenmoveObject(objectData.BaseData);
    }
}
