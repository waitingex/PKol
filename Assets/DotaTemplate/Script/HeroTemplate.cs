using UnityEngine;
using System.Collections;

public enum HeroType
{
    Strength = 0,
    Dexterity = 1,
    Intellect = 2,
}

public class HeroCfg
{
    public HeroType heroType = HeroType.Strength;
    public string name = "nobobdy";
    public float baseStrength = 1.0f;
    public float baseDexterity = 1.0f;
    public float baseIntellect = 1.0f;

    public float strengthGrowth = 1.0f;
    public float dexterityGrowth = 1.0f;
    public float intellectGrowth = 1.0f;

    public float baseDamage = 1.0f;
    public float baseArmor = 0.0f;
    public float BAT = 1.5f;

    public float baseVitality = 20.0f;
    public float baseMana = 20.0f;
    public int damageInteval = 2;
}
/// <summary>
/// 模拟dota的英雄模板
/// </summary>
public class HeroTemplate 
{
    public HeroType heroType = HeroType.Strength;
    public string name = "nobody";
    //base attribute
    public float baseStrength = 1.0f;
    public float baseDexterity = 1.0f;
    public float baseIntellect = 1.0f;

    public float strengthGrowth = 1.0f;
    public float dexterityGrowth = 1.0f;
    public float intellectGrowth = 1.0f;

    public float baseDamage = 1.0f;
    public float baseArmor = 0.0f;
    public float BAT = 1.5f;

    public float baseVitality = 20.0f;
    public float baseMana = 20.0f;
    public int damageInteval = 2;

    private float m_damage = 0;

    private uint m_level = 0;
    private float m_strength = 0;//每点力量增加19点体力，并增加每秒0.03的生命回复速度
    private float m_dexterity = 0;//每点敏捷提高1%的攻击间隔，每7点敏捷增加1点护甲。
    private float m_intellect = 0;//每点智力增加12点魔法值，并增加0.04的魔法回复速度。
    private float m_armor = 0;
    private float m_vitality = 0;
    private float m_mana = 0;

    //次/秒
    private float m_attInterval = 1.0f;
    public float equipIAS = 0;

    public HeroTemplate()
    {
       
    }

    public void Initialize(HeroCfg cfg)
    {
        name = cfg.name;
        heroType = cfg.heroType;
        baseStrength = cfg.baseStrength;
        baseDexterity = cfg.baseDexterity;
        baseIntellect = cfg.baseIntellect;

        strengthGrowth = cfg.strengthGrowth;
        dexterityGrowth = cfg.dexterityGrowth;
        intellectGrowth = cfg.intellectGrowth;
        baseDamage = cfg.baseDamage;
        baseArmor = cfg.baseArmor;
        BAT = cfg.BAT;

        baseVitality = cfg.baseVitality;
        baseMana = cfg.baseMana;
        damageInteval = cfg.damageInteval;

        OnLevelChange(1);
        ResetHp();
    }

    public uint level
    {
        get
        {
            return m_level;
        }
        set
        {
            m_level = value;
            OnLevelChange(value);
        }
    }

    public float strength
    {
        get
        {
            return m_strength;
        }
    }

    public float attInterval
    {
        get
        {
            return m_attInterval = BAT / 1 + 0.01f * m_dexterity + equipIAS;
        }
    }

    private void OnLevelChange(uint value)
    {
        m_strength = baseStrength + (value - 1) * strengthGrowth;
        m_dexterity = baseDexterity + (value - 1) * dexterityGrowth;
        m_intellect = baseIntellect + (value - 1) * intellectGrowth;

        switch(heroType)
        {
            case HeroType.Strength:
                m_damage = baseDamage + m_strength;
                break;
            case HeroType.Dexterity:
                m_damage = baseDamage + m_dexterity;
                break;
            case HeroType.Intellect:
                m_damage = baseDamage + m_intellect;
                break;
        }

        m_armor = baseArmor + m_dexterity / 7.0f;
        m_vitality = baseVitality + m_strength * 19;
        m_mana = baseMana + m_intellect * 12;

#if UNITY_EDITOR
        Debug.Log("strength: " + m_strength + " dexterity: " + m_dexterity + " intellect" + m_intellect + " armor: " + m_armor +
            " vitality: " + m_vitality + " mana: " + m_mana + " damage:" + m_damage + "-" + (m_damage + damageInteval));
#endif
    }

    //以下代码属于不科学摆放，但是方便
    private float m_hp;

    public void ResetHp()
    {
        m_hp = m_vitality;
    }
    public void UnderAttact(float damage)
    {
        if(m_hp <= 0)
        {
            return;
        }
        float trueDamage = BattleUtil.GetNormalDamage(damage, m_armor);
        m_hp -= trueDamage;

#if UNITY_EDITOR
        Debug.LogWarning(name + "承受了" + trueDamage + "点伤害");
        if (m_hp <= 0)
        {
            Debug.LogError(name + "已经死了");
        }  
#endif
    }

    public void NormalAttat(HeroTemplate role)
    {
        if(role == null)
        {
            return;
        }
        float damage = BattleUtil.CalcDamage(m_damage, (float)damageInteval);
#if UNITY_EDITOR
        Debug.Log(name + "对" + role.name + "造成了" + damage + "点伤害");
#endif
        role.UnderAttact(damage);
    }
}
