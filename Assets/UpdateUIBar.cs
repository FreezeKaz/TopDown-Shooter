using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateUIBar : MonoBehaviour
{
    [SerializeField] private RectTransform _hpBar;
    [SerializeField] private RectTransform _xpBar;
    [SerializeField] private Entity _player;

    public void Awake()
    {
        _hpBar.localScale = new Vector3(0,1f,1f);
        _xpBar.localScale = new Vector3(1.82f,1f,1f);
    }

    // Update is called once per frame
    void Update()
    {
        SetHPBar();
        SetXPBar();
    }

    private void SetHPBar()
    {

        float amount = 2f - (_player.CurrentHP / _player.Stats[Entity.Attribute.HP].Value) * 2f;
        _hpBar.localScale = new Vector3(amount, 1f, 1f);
    }
    private void SetXPBar()
    {

       float amount = 1.82f - (_player.XP / _player.XPToGet) * 1.82f;
       _xpBar.localScale = new Vector3(amount, 1f, 1f);
    }
}
