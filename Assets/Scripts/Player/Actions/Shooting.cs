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
    public GameObject bulletPrefab;

    public Weapon weapon;

    private float fireRate;
    private float interval;
    public float bulletForce = 20f;
    private bool _shooting = false;

    // Start is called before the first frame update
    void Start()
    {
        fireRate = weapon.FireRate;

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
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
                bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
                return;
            }
            interval -= Time.deltaTime;
        }
    }
    public void Shoot(InputAction.CallbackContext input)
    {
        _shooting = true;

        //SoundFXManager.instance.PlaySoundFXClip(shootingSoundClip, transform, volumeSFX);
        //SoundFXManager.instance.PlayRandomSoundFXClip(shootingSoundClips, transform, volumeSFX);
    }

    public void StopShooting(InputAction.CallbackContext input)
    {
        Debug.Log("wiao bye");
        _shooting = false;
    }
}
