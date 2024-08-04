using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelectManager : MonoBehaviour, IGameStateListener
{
    [SerializeField] WeaponDataSO[] weaponDatas;
    [SerializeField] WeaponSelectButton weaponSelectButtonPrefab;
    [SerializeField] Transform Group_WeaponSelectButton; // parent transfrom for Buttons

    private List<WeaponSelectButton> buttonList = new List<WeaponSelectButton>();

    public void GameStateChangeCallBack(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.WEAPONSELECTION:
                CreatSelectButtons();
                break;           
        }
    }

    private void CreatSelectButtons()
    {
        //Group_WeaponSelectButton.Clear();
        for (int i = 0; i < 3; i++)
        {
            WeaponDataSO weaponData = weaponDatas[Random.Range(0, weaponDatas.Length)];
            int weaponLevel = Random.Range(0, 4);

            WeaponSelectButton newButton = Instantiate(weaponSelectButtonPrefab, Group_WeaponSelectButton);
            newButton.Configue(weaponData.WeaponSprite, weaponData.WeaponName, weaponLevel);
            newButton.Button.onClick.RemoveAllListeners();
            newButton.Button.onClick.AddListener(() => WeaponSelectCallBack(newButton));

            buttonList.Add(newButton);
        }
    }

    private void WeaponSelectCallBack(WeaponSelectButton button)
    {
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
