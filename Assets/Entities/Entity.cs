using System;
using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class Entity : MonoBehaviour
{

    [SerializeField] public float HP;
    [SerializeField] public float Attack;
    [SerializeField] public float FireRateRatio;
    [SerializeField] public float MoveSpeedRatio;
    [SerializeField] public float XPGiven;
    [SerializeField] public float RangeOfSight = 15f;
    [SerializeField] public float RangeofAttack = 10f;

    [SerializeField] protected GameObject myParent;
    [SerializeField] protected UpgradeManager UpgradeManager;
    [SerializeField] protected EnemyManager EnemyManager;

    [SerializeField] UnityEvent _onTakeDamage;
    [SerializeField] UnityEvent _onHealthLow;
    [SerializeField] UnityEvent _onLevelUp;

    public static event Action _onEnemyDie;

    //Entity just holds statwise, weapon will be handled with player or enemy object

    public enum Attribute
    {
        HP, Attack, FireRateRatio, MoveSpeedRatio
    }

    public float CurrentHP { get; set; }


    public virtual Dictionary<Attribute, Stat<float>> Stats { get; set; }

    public int Level { get; private set; }
    private int nbOfLevel = 0;
    private int nbOfLeveled = 0;
    public int XP { get; set; }
    public float BaseXP { get; private set; }
    public float XPToGet { get; private set; }

    private float maxHp;

    public void Awake()
    {
        initStats();
    }

    public void Update()
    {
        if (myParent.layer == 6 )
        {
            checkLevel();
        }
      
    }

    private void initStats()
    {
        Stats = new Dictionary<Attribute, Stat<float>>();
        Stats[Attribute.HP] = new(HP);
        Stats[Attribute.Attack] = new(Attack);
        Stats[Attribute.FireRateRatio] = new(FireRateRatio);
        Stats[Attribute.MoveSpeedRatio] = new(MoveSpeedRatio);

        CurrentHP = Stats[Attribute.HP].Value;
        maxHp = CurrentHP;

        XPToGet = 50;
        BaseXP = 50;

    }
    private void checkLevel()
    {
       
        while (XP >= XPToGet)
        {
            XP = XP - (int)XPToGet;
            XPToGet = BaseXP * math.pow(1.5f, Level);
            XPToGet = (int)XPToGet; //take off decimal part
            nbOfLevel++;

        }
        if(UpgradeManager.ChoseUpgrade == 0)
        {
            for (int i = 0; i < nbOfLevel; i++)
            {
                if (UpgradeManager.ChoseUpgrade == 0)
                {
                    levelUp();
                }
                StartCoroutine(LevelUpCoroutine());
            }
        }

        if(nbOfLevel == nbOfLeveled )
        {
            nbOfLevel = 0;
            nbOfLeveled = 0;
            UpgradeManager.ChoseUpgrade = 0;
        }

    }
    IEnumerator LevelUpCoroutine()
    {

        while (UpgradeManager.ChoseUpgrade == 1) 
        {
            yield return null;
        }
        levelUp();
        nbOfLeveled++;
        yield return null;

    }
    private void levelUp()
    {
        Level++;
        _onLevelUp.Invoke();
        UpgradeManager.OnLevelUp();
    }

    IEnumerator KillEnemy()
    {

        int originalLayer = EnemyManager.Physics.gameObject.layer;
        EnemyManager.Physics.layer = LayerMask.NameToLayer("Default");
        EnemyManager.myShootingScript.StopShooting();

        yield return new WaitForSecondsRealtime(0.3f);

        EnemyManager.Physics.layer = originalLayer;
        EnemyManager.Render.SetActive(true);
        myParent.SetActive(false);
        WaveGenerator.Instance.TotalEnemies--;
        GameManager.Instance.HandleEnemyDefeat(this);
        yield return null;
    }

    public void TakeDamage(int amount)
    {
        CurrentHP -= amount;
        if (myParent.name == "Player")
        {
            if (CurrentHP <= maxHp / 2)
            {
                _onHealthLow.Invoke();
            }
            _onTakeDamage.Invoke();
        }
        if (CurrentHP <= 0)
        {
            if (myParent.name != "Player")
            {
                _onTakeDamage.Invoke();
                _onEnemyDie?.Invoke();
                EnemyManager.Render.SetActive(false);
                StartCoroutine(KillEnemy()); 
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
