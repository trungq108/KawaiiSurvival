using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopManager : Singleton<ShopManager>, IGameStateListener
{
    [Header("Main Shop Element")]
    [SerializeField] ShopItemContainer containerPrefab;
    [SerializeField] Transform containersParent;

    [SerializeField] Button rerollButton;
    [SerializeField] int rerollPrice;
    [SerializeField] TextMeshProUGUI rerollPriceText;

    [Header("PlayerStats Element")]

    [SerializeField] Button playerStats_OpenButton;
    [SerializeField] EventTrigger close_PlayerStatsTab;
    [SerializeField] CanvasGroup playStats_TabParent;
    [SerializeField] RectTransform playerStats_Tab;
    private Vector2 playerStatsTab_OpenPos;
    private Vector2 playerStatsTab_ClosePos;

    [Header("Inventory Element")]

    [SerializeField] Button inventory_OpenButton;
    [SerializeField] EventTrigger close_InventoryTab;
    [SerializeField] CanvasGroup inventory_TabParent;
    [SerializeField] RectTransform inventory_Tab;
    [SerializeField] RectTransform itemInfo_Slide;
    private Vector2 inventory_OpenPos;
    private Vector2 inventory_ClosePos;
    private Vector2 itemInfo_OpenPos;
    private Vector2 itemInfo_ClosePos;


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

    private void ConfiguePlayerStatsTab()
    {
        playerStats_OpenButton.onClick.AddListener(() => OpenPlayerStatsTab(0.5f));

        EventTrigger.Entry entry = new EventTrigger.Entry { eventID = EventTriggerType.PointerDown };
        entry.callback.AddListener((eventData) => ClosePlayerStatsTab(0.5f));
        close_PlayerStatsTab.triggers.Add(entry);
        ClosePlayerStatsTab(0f);

        playerStatsTab_OpenPos = playerStats_Tab.anchoredPosition;
        playerStatsTab_ClosePos = playerStats_Tab.anchoredPosition - Vector2.right * playerStats_Tab.anchoredPosition.x;
    }

    public void OpenPlayerStatsTab(float openDuration)
    {
        playerStats_Tab.DOAnchorPos(playerStatsTab_OpenPos, openDuration).SetEase(Ease.InOutCubic)
            .OnUpdate(() => playStats_TabParent.DOFade(1, openDuration))
            .OnComplete(() =>
            {
                playStats_TabParent.interactable = true;
                playStats_TabParent.blocksRaycasts = true;
            });
    }

    public void ClosePlayerStatsTab(float closeDuration)
    {
        playerStats_Tab.DOAnchorPos(playerStatsTab_ClosePos, closeDuration).SetEase(Ease.InOutCubic)
            .OnUpdate(() => playStats_TabParent.DOFade(0, closeDuration))
            .OnComplete(() =>
            {
                playStats_TabParent.interactable = false;
                playStats_TabParent.blocksRaycasts = false;
            });
    }

    private void ConfigueInventory()
    {
        inventory_OpenButton.onClick.AddListener(() => OpenInventory(0.5f));

        EventTrigger.Entry entry = new EventTrigger.Entry { eventID = EventTriggerType.PointerDown };
        entry.callback.AddListener((eventData) => CloseInventory(0.5f));
        close_InventoryTab.triggers.Add(entry);
        CloseInventory(0f);

        inventory_OpenPos = inventory_Tab.anchoredPosition;
        inventory_ClosePos = inventory_Tab.anchoredPosition - Vector2.right * inventory_Tab.anchoredPosition.x;

        itemInfo_OpenPos = itemInfo_Slide.anchoredPosition;
        itemInfo_ClosePos = -itemInfo_OpenPos;
        CloseItemInfo(0);
    }

    public void OpenInventory(float openDuration)
    {
        inventory_Tab.DOAnchorPos(inventory_OpenPos, openDuration).SetEase(Ease.InOutCubic)
            .OnUpdate(() => inventory_TabParent.DOFade(1, openDuration))
            .OnComplete(() =>
            {
                inventory_TabParent.interactable = true;
                inventory_TabParent.blocksRaycasts = true;
            });
    }

    public void CloseInventory(float closeDuration)
    {
        inventory_Tab.DOAnchorPos(inventory_ClosePos, closeDuration).SetEase(Ease.InOutCubic)
            .OnUpdate(() =>
            {
                CloseItemInfo(closeDuration/4);
                inventory_TabParent.DOFade(0, closeDuration);
            })
            .OnComplete(() =>
            {
                inventory_TabParent.interactable = false;
                inventory_TabParent.blocksRaycasts = false;
            });
    }

    public void OpenItemInfo(float openDuration)
    {
        itemInfo_Slide.DOAnchorPos(itemInfo_OpenPos, openDuration).SetEase(Ease.InOutCubic);
    }
    public void CloseItemInfo(float closeDuration)
    {
        itemInfo_Slide.DOAnchorPos(itemInfo_ClosePos, closeDuration).SetEase(Ease.InOutCubic);
    }


    public void GameStateChangeCallBack(GameState gameState)
    {
        switch(gameState)
        {
            case GameState.SHOP:
                CreatShopIteamContainers();
                rerollButton.onClick.AddListener(() => Reroll());
                ConfiguePlayerStatsTab();
                ConfigueInventory();
                break;
        }
    }
}
