using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>, IGameStateListener
{
    [SerializeField] Transform shopPanelParent;
    [SerializeField] Transform pausePanelParent;
    [SerializeField] InventoryItemContainer prefab;
    [SerializeField] ItemInfoSlide infoSlide;
    private Player player;
    private void OnEnable()
    {
        GameEvent.OnWeaponMerge += MergeWeaponCallBack;
    }

    private void OnDisable()
    {
        GameEvent.OnWeaponMerge -= MergeWeaponCallBack;
    }

    public void Configue()
    {
        shopPanelParent.Clear();
        pausePanelParent.Clear();

        player = GameManager.Instance.Player;
        List<ObjectDataSO> playerObjects = player.GetObjects();
        List<Weapon> playerWeapons = player.GetWeapons();

        for (int i = 0;i < playerWeapons.Count; i++)
        {
            Weapon weapon = playerWeapons[i];

            InventoryItemContainer shopContainer = Instantiate(prefab, shopPanelParent);
            shopContainer.Configue(weapon);
            shopContainer.Button.onClick.RemoveAllListeners();
            shopContainer.Button.onClick.AddListener(() => OpenItemInfo(shopContainer));

            InventoryItemContainer pauseContainer = Instantiate(prefab, pausePanelParent);
            pauseContainer.Configue(weapon);
            pauseContainer.Button.interactable = false;
        }

        for(int i = 0; i < playerObjects.Count; i++)
        {
            ObjectDataSO data = playerObjects[i];

            InventoryItemContainer shopContainer = Instantiate(prefab, shopPanelParent);
            shopContainer.Configue(data);
            shopContainer.Button.onClick.RemoveAllListeners();
            shopContainer.Button.onClick.AddListener(() => OpenItemInfo(shopContainer));

            InventoryItemContainer pauseContainer = Instantiate(prefab, pausePanelParent);
            pauseContainer.Configue(data);
            pauseContainer.Button.interactable = false;
        }
    }

    public void OpenItemInfo(InventoryItemContainer container)
    {
        if (container.weaponInfo != null)
        {
            ShopManager.Instance.OpenItemInfo(0.5f);
            infoSlide.ConfigueWeapon(container.weaponInfo);
            infoSlide.RecycleButton.onClick.RemoveAllListeners();
            infoSlide.RecycleButton.onClick.AddListener(() => RecycleWeapon(container.weaponInfo));
        }

        else if (container.objectInfo != null)
        {
            ShopManager.Instance.OpenItemInfo(0.5f);
            infoSlide.ConfigueObject(container.objectInfo);
            infoSlide.RecycleButton.onClick.RemoveAllListeners();
            infoSlide.RecycleButton.onClick.AddListener(() => RecycleObject(container.objectInfo));
        }
    }

    public void RecycleWeapon(Weapon weapon)
    {
        int weaponRecyclePrice = Calculator.WeaponRecyclePrice(weapon.Data, weapon.weaponLevel);
        CurrencyManager.Instance.AddCandy(weaponRecyclePrice);
        ShopManager.Instance.CloseItemInfo(0.5f);
        player.RemoveWeapon(weapon);
        Configue();
    }

    public void RecycleObject(ObjectDataSO objectData)
    {
        CurrencyManager.Instance.AddCandy(objectData.SellPrice);
        ShopManager.Instance.CloseItemInfo(0.5f);
        player.RemoveObject(objectData);
        Configue();
    }

    private void MergeWeaponCallBack(Weapon weapon)
    {
        Configue();
        infoSlide.ConfigueWeapon(weapon);
    }

    public void GameStateChangeCallBack(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.GAME:
                Configue();
                break;
        }
    }
}
