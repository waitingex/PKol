using UnityEngine;
using System.Collections;

public class HeroAccounting : MonoBehaviour 
{

	void Start()
    {
        Debug.LogWarning("Coco");
        //DotaHeroBaseCalcUtil.LogOutBaseVar("力量", 24, 3);
        //DotaHeroBaseCalcUtil.LogOutBaseVar("敏捷", 14, 1.3f);
        //DotaHeroBaseCalcUtil.LogOutBaseVar("智力", 18, 1.5f);
        DotaHeroBaseCalcUtil.LogOutBaseArmor(2, 14);
        DotaHeroBaseCalcUtil.LogOutBaseVita(606, 24);
        DotaHeroBaseCalcUtil.LogOutBaseMana(234, 18);
        DotaHeroBaseCalcUtil.LogOutBaseDamage(50, 24);

        Debug.LogWarning("magina");
        //DotaHeroBaseCalcUtil.LogOutBaseVar("力量", 20, 1.2);
        //DotaHeroBaseCalcUtil.LogOutBaseVar("敏捷", 22, 2.8f);
        //DotaHeroBaseCalcUtil.LogOutBaseVar("智力", 15, 1.8f);
        DotaHeroBaseCalcUtil.LogOutBaseArmor(2.1f, 22);
        DotaHeroBaseCalcUtil.LogOutBaseVita(530, 20);
        DotaHeroBaseCalcUtil.LogOutBaseMana(195, 15);
        DotaHeroBaseCalcUtil.LogOutBaseDamage(49, 22);
    }
}
