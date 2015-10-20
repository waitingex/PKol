using UnityEngine;
using System.Collections;

public class BattleUtil 
{

    public static float GetNormalDamage(float rawDamage, float armor)
    {
        float ret = 0;
        ret = rawDamage * (1 - 0.06f * armor / (1 + 0.06f * armor));
        return ret;
    }

    public static float CalcDamage(float rawDamage, float maxInteval)
    {
        return rawDamage + Random.Range(0, maxInteval);
    }
}
