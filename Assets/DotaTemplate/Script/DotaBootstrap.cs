using UnityEngine;
using System.Collections;

public class DotaBootstrap : MonoBehaviour 
{
    HeroTemplate coco;
    HeroTemplate magina;
	// Use this for initialization
	void Start () 
    {
        coco = new HeroTemplate();
        HeroCfg cocoCfg = new HeroCfg();
        cocoCfg.name = "CoCo";
        cocoCfg.heroType = HeroType.Strength;
        cocoCfg.baseStrength = 24.0f;
        cocoCfg.baseDexterity = 14.0f;
        cocoCfg.baseIntellect = 18.0f;
        cocoCfg.strengthGrowth = 3.0f;
        cocoCfg.dexterityGrowth = 1.3f;
        cocoCfg.intellectGrowth = 1.5f;
        cocoCfg.baseDamage = 26.0f;
        cocoCfg.baseArmor = 0;
        cocoCfg.BAT = 1.7f;
        cocoCfg.baseVitality = 150.0f;
        cocoCfg.baseMana = 18.0f;
        cocoCfg.damageInteval = 10;

        coco.Initialize(cocoCfg);

        magina = new HeroTemplate();
        HeroCfg maginaCfg = new HeroCfg();
        maginaCfg.name = "Magina";
        maginaCfg.heroType = HeroType.Dexterity;
        maginaCfg.baseStrength = 20.0f;
        maginaCfg.baseDexterity = 22.0f;
        maginaCfg.baseIntellect = 15.0f;
        maginaCfg.strengthGrowth = 1.2f;
        maginaCfg.dexterityGrowth = 2.8f;
        maginaCfg.intellectGrowth = 1.8f;
        maginaCfg.baseDamage = 27.0f;
        maginaCfg.baseArmor = -1.042857f;
        maginaCfg.BAT = 1.4f;
        maginaCfg.baseVitality = 150.0f;
        maginaCfg.baseMana = 15.0f;
        maginaCfg.damageInteval = 4;

        magina.Initialize(maginaCfg);
	}

    private float m_curCoco = 0;
    private float m_curMagina = 0;
	// Update is called once per frame
	void Update () 
    {
	    if(m_curCoco < coco.attInterval)
        {
            m_curCoco += Time.deltaTime;
        }
        else
        {
            m_curCoco = 0;
            coco.NormalAttat(magina);
        }
        if (m_curMagina < magina.attInterval)
        {
            m_curMagina += Time.deltaTime;
        }
        else
        {
            m_curMagina = 0;
            magina.NormalAttat(coco);
        }
	}
}
