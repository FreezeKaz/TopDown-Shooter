using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shooting : MonoBehaviour
{

    [SerializeField] private AudioClip[] shootingSoundClips;
    [SerializeField] private Entity shootingEntity;
    [SerializeField][Range(0, 1)] private float volumeSFX;
    [SerializeField][Range(7, 8)] private int shooter;

    public FirePoints firePoint;
    public Weapon currentWeapon;
    private BulletSO bullet;

    private float fireRate;
    private float interval;
    private bool _shooting = false;
    // Start is called before the first frame update
    void Start()
    {
        bullet = currentWeapon.bullet;
        fireRate = currentWeapon.FireRate;
        interval = 1f / fireRate;
    }


    private void Update()
    {
        // Update is called once per frame
        if (_shooting)
        {
           
            if (interval <= 0)
            {
                interval = 1 / (fireRate * shootingEntity.Stats[Entity.Attribute.FireRateRatio].Value);
                foreach (var index in currentWeapon.firePoints)
                {
                    GameObject myBullet = BulletPoolManager.Instance.GetPoolObject();
                    myBullet.SetActive(true);
                    myBullet.transform.position = firePoint.points[index].transform.position;
                    myBullet.transform.localScale = new Vector3(bullet.scale, bullet.scale, bullet.scale);
                    myBullet.GetComponent<Rigidbody2D>().AddForce(firePoint.points[index].transform.up * currentWeapon.bulletForce, ForceMode2D.Impulse);
                    myBullet.GetComponent<BulletDamage>().damage = currentWeapon.Damage;
                    myBullet.layer = shooter;
                }

                //SoundFXManager.instance.PlayRandomSoundFXClip(shootingSoundClips, transform, volumeSFX);

                return;
            }
            interval -= Time.deltaTime;
        }
    }
    public void EnableShoot(InputAction.CallbackContext input)
    {
        StartShooting();
    }
    
    public void DisableShoot(InputAction.CallbackContext input)
    {
        StopShooting(); //only for input
    }

    public void StartShooting()
    {
        _shooting = true;

    }
    public void StopShooting()
    {
        _shooting = false;

    }
    public void ChangeWeapon(Weapon newWeapon)
    {
        currentWeapon = newWeapon;
        bullet = currentWeapon.bullet;
        fireRate = currentWeapon.FireRate;
        interval = 1f / fireRate;
    }

}
