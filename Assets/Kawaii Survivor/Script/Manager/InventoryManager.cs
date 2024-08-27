using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : MonoBehaviour, IGameStateListener
{
    [SerializeField] Transform parent;
    [SerializeField] InventoryItemContainer prefab;
    [SerializeField] ItemInfoSlide infoSlide;


    private void Configue()
    {
        Player player = GameManager.Instance.Player;
        List<ObjectDataSO> playerObjects = player.GetObjects();
        List<Weapon> playerWeapons = player.GetWeapons();

        for(int i = 0; i < playerObjects.Count; i++)
        {
            InventoryItemContainer container = Instantiate(prefab, parent);
            Sprite icon = playerObjects[i].Icon;
            Color color = ColorHolder.GetColor(playerObjects[i].RareRate);
            container.Configue(playerObjects[i], () => OpenItemInfo(container));
        }

        for (int i = 0;i < playerWeapons.Count; i++)
        {
            InventoryItemContainer container = Instantiate(prefab, parent);
            Sprite icon = playerWeapons[i].Data.WeaponIcon;
            Color color = ColorHolder.GetColor(playerWeapons[i].weaponLevel);
            container.Configue(playerWeapons[i], () => OpenItemInfo(container));
        }
    }

    public void OpenItemInfo(InventoryItemContainer container)
    {
        if (container.weaponInfo != null) ShowWeaponInfo(container.weaponInfo);
        else if (container.objectInfo != null) ShowObjectInfo(container.objectInfo);
    }

    public void ShowWeaponInfo(Weapon weapon)
    {
        ShopManager.Instance.OpenItemInfo(0.5f);
        infoSlide.ConfigueWeapon(weapon);
    }

    public void ShowObjectInfo(ObjectDataSO objectData)
    {
        ShopManager.Instance.OpenItemInfo(0.5f);
        infoSlide.ConfigueObject(objectData);
    }

    public void GameStateChangeCallBack(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.SHOP:
                Configue();
                break;
        }
    }
}
