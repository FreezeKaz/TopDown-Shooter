using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shooting : MonoBehaviour
{

    [SerializeField] private AudioClip[] shootingSoundClips;
    [SerializeField][Range(0, 1)] private float volumeSFX;

    public Transform firePoint;
    public Weapon currentWeapon;
    private Bullet bullet;
    private float fireRate;
    private float interval;
    private bool _shooting = false;
    // Start is called before the first frame update
    void Start()
    {
        bullet = currentWeapon.bullet.GetComponent<Bullet>();
        fireRate = currentWeapon.FireRate;
        interval = 1f / fireRate;
    }


    private void Update()
    {
        // Update is called once per frame
        if (_shooting)
        {
            Debug.Log("hahaha");
            if (interval <= 0)
            {
                interval = 1 / fireRate;
                GameObject myBullet = Instantiate(bullet.gameObject, firePoint.position, Quaternion.identity);
                myBullet.GetComponent<Rigidbody2D>().AddForce(firePoint.up * bullet.bulletForce, ForceMode2D.Impulse);
                SoundFXManager.instance.PlayRandomSoundFXClip(shootingSoundClips, transform, volumeSFX);

                return;
            }
            interval -= Time.deltaTime;
        }
    }
    public void Shoot(InputAction.CallbackContext input)
    {
        _shooting = true;

        //SoundFXManager.instance.PlaySoundFXClip(shootingSoundClip, transform, volumeSFX);

    }
    
    public void StopShooting(InputAction.CallbackContext input)
    {
        Debug.Log("wiao bye");
        _shooting = false;
    }

    private void ChangeWeapon(Weapon newWeapon)
    {
        currentWeapon = newWeapon;
        bullet = currentWeapon.bullet.GetComponent<Bullet>();
        fireRate = currentWeapon.FireRate;
        interval = 1f / fireRate;
    }


    private void OnEnable()
    {
        WeaponChange.OnWeaponChange += ChangeWeapon;
    }

    private void OnDisable()
    {
        WeaponChange.OnWeaponChange -= ChangeWeapon;
    }
}
