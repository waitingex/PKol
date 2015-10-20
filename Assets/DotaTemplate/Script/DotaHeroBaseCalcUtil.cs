using UnityEngine;
using System.Collections;

public class DotaHeroBaseCalcUtil 
{
    public static void LogOutBaseVar(string type, float Var, float growthValue, int level = 1)
    {
        float baseStr = Var - growthValue * level;
        Debug.Log(type + ": " + baseStr);
    }

    public static void LogOutBaseArmor(float Armor, float dex, int level = 1)
    {
        Debug.Log("基础护甲: " + (Armor - dex / 7.0f));
    }

    public static void LogOutBaseVita(float vita, float str, int level = 1)
    {
        Debug.Log("基础生命: " + (vita - str * 19.0f));
    }


    public static void LogOutBaseMana(float mana, float inte, int level = 1)
    {
        Debug.Log("基础魔法: " + (mana - inte * 12.0f));
    }

    public static void LogOutBaseDamage(float damage, float mainVar)
    {
        Debug.Log("基础攻击: " + (damage - mainVar));
    }
}
