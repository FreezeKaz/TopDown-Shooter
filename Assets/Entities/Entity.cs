using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
  

public class Entity : MonoBehaviour
{
    
    [SerializeField] public float HP;
    [SerializeField] public float Attack;
    [SerializeField] public float FireRateRatio;
    [SerializeField] public float MoveSpeedRatio;
    [SerializeField] public float XPGiven;

    [SerializeField] protected GameObject myParent;
    [SerializeField] protected UpgradeManager UpgradeManager;

    //Entity just holds statwise, weapon will be handled with player or enemy object

    public enum Attribute
    {
        HP, Attack, FireRateRatio, MoveSpeedRatio
    }

    public float CurrentHP { get; set; }


    public virtual Dictionary<Attribute, Stat<float>> Stats { get; set; }

    public int Level { get; private set; }
    public int XP { get; set; }
    public float BaseXP { get; private set; }
    public float XPToGet { get; private set; }

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
        Stats[Attribute.HP] = new(HP);
        Stats[Attribute.Attack] = new(Attack);
        Stats[Attribute.FireRateRatio] = new(FireRateRatio);
        Stats[Attribute.MoveSpeedRatio] = new(MoveSpeedRatio);

        CurrentHP = Stats[Attribute.HP].Value;

        XPToGet = 50;
        BaseXP = 50;

    }
    private void checkLevel()
    {
        if (XP >= XPToGet)
        {
            XP = XP - (int)XPToGet;
            XPToGet = BaseXP * math.pow(1.5f, Level);
            XPToGet = (int)XPToGet; //take off decimal part
            levelUp();
        }
    }

    private void levelUp()
    {
        Level++;
        UpgradeManager.OnLevelUp();
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
                GameManager.Instance.HandleEnemyDefeat(this);
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
