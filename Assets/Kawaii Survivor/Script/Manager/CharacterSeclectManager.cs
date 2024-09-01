using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.U2D.Animation;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSeclectManager : Singleton<CharacterSeclectManager>, IGameStateListener
{
    [SerializeField] private Transform containerParent;
    [SerializeField] private CharacterSeclectInfo infoTab;
    [SerializeField] private CharacterSelectContainer containerPrefab;
    [SerializeField] private Image CenterImageIcon;

    private int saveIndex;
    private List<CharacterSelectContainer> containerList = new List<CharacterSelectContainer>();
    public CharacterSelectContainer SelectedContainer {  get; private set; }
    private CharacterDataSO[] characterDatas;

    void Awake()
    {
        OnInit();
    }

    private void OnInit()
    {
        characterDatas = GameAssets.LoadCharacters();
        for (int i = 0; i < characterDatas.Length; i++)
        {
            CharacterDataSO data = characterDatas[i];
            CreatContainer(data);
        }
        saveIndex = ES3.Load<int>("saveIndex", 0);
        EquipCallBack(containerList[saveIndex]);
    }

    private void CreatContainer(CharacterDataSO data)
    {
        CharacterSelectContainer container = Instantiate(containerPrefab, containerParent);
        container.Configue(data);
        container.Button.onClick.RemoveAllListeners();
        container.Button.onClick.AddListener(() => OnCharacterSelectCallBack(container));
        containerList.Add(container);
    }

    private void OnCharacterSelectCallBack(CharacterSelectContainer container)
    {
        CenterImageIcon.sprite = container.Data.PlayerSprite;
        if (container.isPurchase)
        {
            EquipCallBack(container);
        }
        else infoTab.Configue(container, () => OnCharacterPurchaseCallBack(container));
    }

    private void OnCharacterPurchaseCallBack(CharacterSelectContainer container)
    {
        CurrencyManager.Instance.PayMoney(container.Data.PlayerPrice);
        container.SetPurchase();
        SelectedContainer = container;
        EquipCallBack(container);
    }

    public void EquipCallBack(CharacterSelectContainer container)
    {
        saveIndex = containerList.IndexOf(container);
        ES3.Save<int>("saveIndex", saveIndex);

        SelectedContainer = containerList[saveIndex];
        CenterImageIcon.sprite = SelectedContainer.Data.PlayerSprite;
        infoTab.Configue(container, null);

        for (int i = 0; i < containerList.Count; i++)
        {
            if (containerList[i] != SelectedContainer)
            {
                containerList[i].UnEquip();
            }
            else containerList[i].Equip();
        }
    }

    public void GameStateChangeCallBack(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.MENU:
                EquipCallBack(containerList[saveIndex]);
                break;
            case GameState.GAME:
                GameManager.Instance.Player.SetCharacterSprite(SelectedContainer.Data.PlayerSprite);
                break;
        }
    }
}
