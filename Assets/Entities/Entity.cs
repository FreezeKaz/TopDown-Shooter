using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] public EntityStat stat;
    [SerializeField] protected GameObject myParent;

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

    private void initStats()
    {
        Stats = new Dictionary<Attribute, Stat<float>>();
        float hp = Stats[Attribute.HP].Value;
        Stats[Attribute.HP] = new(stat.HP);
        Stats[Attribute.Attack] = new(stat.Attack);
        Stats[Attribute.FireRateRatio] = new(stat.FireRateRatio);
        Stats[Attribute.MoveSpeedRatio] = new(stat.MoveSpeedRatio);

        CurrentHP = Stats[Attribute.HP].Value;

    }
    private void checkLevel()
    {
        if (XP >= XPToGet)
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

    public void TakeDamage(int amount)
    {
        //Debug.Log(CurrentHP);
        CurrentHP -= amount;

        if (CurrentHP <= 0)
        {
            if(myParent.name != "Player")
            {
                myParent.SetActive(false);
                WaveGenerator.Instance.TotalEnemies--;
                //GameOverFromGameInstance;
            }
            else
            {
                myParent.SetActive(false);
                GameManager.Instance.GameOver();
                ScenesManager.Instance.SetScene("GameTitle");
                ScenesManager.Instance.ChangeScene();
            }
            
           
        }

    }
}
