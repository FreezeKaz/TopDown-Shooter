using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{

    [SerializeField] protected EntityStat stat;

    //Entity just holds statwise, weapon will be handled with player or enemy object

    public enum Attribute
    {
        HP, Attack, FireRateRatio, MoveSpeedRatio
    }

    public float CurrentHP { get; set; }


    public virtual Dictionary<Attribute, Stat<float>> Stats { get; set; }

    public int Level { get; private set; }
    public int XP { get; private set; }
    public int XPToGet { get; private set; }

    public void Awake()
    {
        initStats();
    }

    public void Update()
    {
        checkLevel();
    }

    private void initStats ()
    {
        new Dictionary<Attribute, Stat<float>>();

        Stats[Attribute.HP] = new(stat.HP);
        Stats[Attribute.Attack] = new(stat.Attack);
        Stats[Attribute.FireRateRatio] = new(stat.FireRateRatio);
        Stats[Attribute.MoveSpeedRatio] = new(stat.MoveSpeedRatio);

        CurrentHP = Stats[Attribute.HP].Value;

    }
    private void checkLevel()
    {
        if(XP >= XPToGet)
        {
            XP = XP - XPToGet;
            //actionToUILevelUp -> what to level up as a param of level up
            levelUp();
        }
    }   

    private void levelUp()
    {
        Level++;
        //increase value 
    }
}
