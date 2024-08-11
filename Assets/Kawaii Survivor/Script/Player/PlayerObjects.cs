using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObjects : MonoBehaviour
{
    private List<ObjectDataSO> Objects = new List<ObjectDataSO>();

    public void AddObject(ObjectDataSO objectData)
    {
        Objects.Add(objectData);
        PlayerStatManager.Instance.AddObject(objectData.BaseData);
    }

}
