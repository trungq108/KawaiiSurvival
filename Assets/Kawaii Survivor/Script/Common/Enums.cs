public enum GameState
{
    MENU,
    GAME,
    WAVETRANSITION,
    SHOP,
    GAMEOVER,
    WEAPONSELECTION,
    STAGECOMPLETE,
}

[System.Serializable]
public enum Stat 
{
    Armor,
    AttackSpeed,
    Attack,
    CriticalChance,
    CriticalPercent,
    Dodge,
    HealthRegen,
    LifeSteal,
    Luck,
    MaxHealth,
    MoveSpeed,
    Range,
}

public static class Enums
{
    public static string FormatStatName(Stat stat)
    {
        string formatedString = "";
        string unFormatedString = stat.ToString();

        formatedString += unFormatedString[0];

        for (int i = 1; i < unFormatedString.Length; i++)
        {
            if (char.IsUpper(unFormatedString[i]))
            {
                formatedString += " ";
            }
            formatedString += unFormatedString[i];
        }

        return formatedString;
    }
}