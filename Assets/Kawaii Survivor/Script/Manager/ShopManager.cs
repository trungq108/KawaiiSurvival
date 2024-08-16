using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour, IGameStateListener
{
    [SerializeField] ShopItemContainer containerPrefab;
    [SerializeField] Transform containersParent;

    [SerializeField] Button rerollButton;

    public void CreatShopIteamContainers()
    {
        List<ShopItemContainer> removeList = new List<ShopItemContainer>();

        for(int i = 0; i < containersParent.childCount; i++)
        {
            ShopItemContainer checkContainer = containersParent.GetChild(i).GetComponent<ShopItemContainer>();
            if (!checkContainer.IsLock)
            {
                removeList.Add(checkContainer);
            }
        }
        Debug.Log(removeList.Count);

        for(int i = removeList.Count - 1; i >= 0; i--)
        {
            removeList[i].transform.SetParent(null);
            Destroy(removeList[i].gameObject);
            removeList.RemoveAt(i);
        }
        Debug.Log(removeList.Count);
        
        int numberSpawn = 6 - containersParent.childCount; Debug.Log(numberSpawn);
        int numberWeapon = Random.Range(0, numberSpawn); 
        int numberObject = numberSpawn - numberWeapon; 

        for (int i = 0; i < numberWeapon; i++)
        {
            WeaponDataSO weaponData = GameAssets.GetRandomWeaponData();
            int randomLevel = Random.Range(0, 3);

            ShopItemContainer weaponItem = Instantiate(containerPrefab, containersParent);
            weaponItem.Confingue(weaponData, randomLevel);
        }

        for(int i = 0;i < numberObject; i++)
        {
            ObjectDataSO objectData = GameAssets.GetRandomObjectData();
            ShopItemContainer newObject = Instantiate(containerPrefab, containersParent);
            newObject.Configue(objectData);
        }
    }

    private void Reroll()
    {
        CreatShopIteamContainers();
    }


    public void GameStateChangeCallBack(GameState gameState)
    {
        switch(gameState)
        {
            case GameState.SHOP:
                Reroll();
                rerollButton.onClick.AddListener(() => Reroll());
                break;
        }
    }
}
