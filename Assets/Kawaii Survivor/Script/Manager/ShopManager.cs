using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour, IGameStateListener
{
    [SerializeField] ShopItemContainer containerPrefab;
    [SerializeField] Transform containersParent;

    [SerializeField] Button rerollButton;
    [SerializeField] int rerollPrice;
    [SerializeField] TextMeshProUGUI rerollPriceText;

    

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

        for(int i = removeList.Count - 1; i >= 0; i--)
        {
            removeList[i].transform.SetParent(null);
            Destroy(removeList[i].gameObject);
            removeList.RemoveAt(i);
        }
        
        int numberSpawn = 6 - containersParent.childCount;
        int numberWeapon = numberSpawn / 2;
        int numberObject = numberSpawn - numberWeapon; 

        for (int i = 0; i < numberWeapon; i++)
        {
            WeaponDataSO weaponData = GameAssets.GetRandomWeaponData();
            int randomLevel = Random.Range(0, 4);

            ShopItemContainer weaponItem = Instantiate(containerPrefab, containersParent);
            weaponItem.Confingue(weaponData, randomLevel);
            weaponItem.Button.onClick.AddListener(() => PurchaseItemCallBack(weaponItem, randomLevel));
        }

        for(int i = 0;i < numberObject; i++)
        {
            ObjectDataSO objectData = GameAssets.GetRandomObjectData();
            ShopItemContainer objectItem = Instantiate(containerPrefab, containersParent);
            objectItem.Configue(objectData);
            objectItem.Button.onClick.AddListener(() => PurchaseItemCallBack(objectItem, 0));
        }
    }

    private void PurchaseItemCallBack(ShopItemContainer container, int level)
    {
        Player player = GameManager.Instance.Player;
        if(container.WeaponData != null && CurrencyManager.IsEnoughMoney(container.Price))
        {
            if (player.TryAddWeapon(container.WeaponData, level))
            {
                CurrencyManager.Instance.Pay(container.Price);
                UpdateRerollButton();
                Destroy(container.gameObject);
            }
        }
        if(container.ObjectData != null && CurrencyManager.IsEnoughMoney(container.Price))
        {
            player.AddObject(container.ObjectData);
            CurrencyManager.Instance.Pay(container.Price);
            UpdateRerollButton();
            Destroy(container.gameObject);
        }
    }

    private void Reroll()
    {
        CurrencyManager.Instance.Pay(rerollPrice);
        CreatShopIteamContainers();
        UpdateRerollButton();
    }

    private void UpdateRerollButton()
    {
        rerollPriceText.text = rerollPrice.ToString();
        rerollButton.interactable = CurrencyManager.IsEnoughMoney(rerollPrice);
    }

    public void GameStateChangeCallBack(GameState gameState)
    {
        switch(gameState)
        {
            case GameState.SHOP:
                CreatShopIteamContainers();
                rerollButton.onClick.AddListener(() => Reroll());
                break;
        }
    }
}
