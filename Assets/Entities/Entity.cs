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

    [SerializeField] protected GameObject myParent;
    [SerializeField] protected UpgradeManager UpgradeManager;

    [SerializeField] UnityEvent _onTakeDamage;
    [SerializeField] UnityEvent _onHealthLow;
    [SerializeField] UnityEvent _onLevelUp;

    [SerializeField] private AudioClip[] SFXSoundClips;
    [SerializeField][Range(0, 1)] private float _volumeSFX;


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

    private float maxHp;

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
        maxHp = CurrentHP;

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
        _onLevelUp.Invoke();
        UpgradeManager.OnLevelUp();
    }
    
    private void KillEnemy()
    {
        myParent.transform.GetChild(0).gameObject.SetActive(true);
        myParent.SetActive(false);

        GameManager.Instance.HandleEnemyDefeat(this);
        WaveGenerator.Instance.TotalEnemies--;
    }

    public void TakeDamage(int amount)
    {
        CurrentHP -= amount;
        if (myParent.name == "Player")  {
            if (CurrentHP <= maxHp / 2) {
                _onHealthLow.Invoke();
            }
            _onTakeDamage.Invoke();
        }
            if (CurrentHP <= 0) {
            if(myParent.name != "Player")
            {
                // enemy death
                _onTakeDamage.Invoke();
                Debug.Log(SFXSoundClips[0]);
                Debug.Log(_volumeSFX);
                //SoundFXManager.instance.PlaySoundFXClip(SFXSoundClips[0], transform, _volumeSFX);
               // for (int i = 0; i < myParent.transform.childCount; i++)
                   myParent.transform.GetChild(0).gameObject.SetActive(false);
                Invoke("KillEnemy", 0.3f);
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
