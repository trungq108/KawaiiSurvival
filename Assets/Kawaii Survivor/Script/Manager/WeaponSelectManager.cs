using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelectManager : MonoBehaviour, IGameStateListener
{
    [SerializeField] WeaponDataSO[] weaponDatas;
    [SerializeField] WeaponSelectButton weaponSelectButtonPrefab;
    [SerializeField] Transform Group_WeaponSelectButton; // parent transfrom for Buttons

    private List<WeaponSelectButton> buttonList = new List<WeaponSelectButton>();
    private WeaponDataSO weaponData;
    private int initLevel;

    public void GameStateChangeCallBack(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.WEAPONSELECTION:
                CreatSelectButtons();
                break;
            case GameState.GAME:
                if(weaponData != null)
                {
                    GameManager.Instance.Player.AddWeapon(weaponData, initLevel);
                    weaponData = null;
                }
                break;
        }
    }

    private void CreatSelectButtons()
    {
        Group_WeaponSelectButton.Clear();
        for (int i = 0; i < 3; i++)
        {
            WeaponDataSO weaponData = weaponDatas[Random.Range(0, weaponDatas.Length)];
            int level = Random.Range(0, 4);

            WeaponSelectButton newButton = Instantiate(weaponSelectButtonPrefab, Group_WeaponSelectButton);
            newButton.Configue(weaponData, level);
            newButton.Button.onClick.RemoveAllListeners();
            newButton.Button.onClick.AddListener(() => WeaponSelectCallBack(newButton, weaponData, level));

            buttonList.Add(newButton);
        }
    }

    private void WeaponSelectCallBack(WeaponSelectButton button, WeaponDataSO weaponData, int level)
    {
        this.weaponData = weaponData;
        initLevel = level;
        for (int i = 0; i < buttonList.Count; i++) 
        {
            if(buttonList[i] == button)
            {
                buttonList[i].Select();
            }
            else buttonList[i].DeSelect();
        }
    }
}
