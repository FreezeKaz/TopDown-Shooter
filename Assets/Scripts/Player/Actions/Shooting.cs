using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shooting : MonoBehaviour
{
    [SerializeField] private AudioClip weaponChangeSoundClips;
    [SerializeField] private Entity shootingEntity;
    [SerializeField] private Animator _animator;
    [SerializeField][Range(0, 1)] private float _volumeShooting;
    [SerializeField][Range(0, 1)] private float _volumeWeaponChange;
    [SerializeField][Range(7, 8)] private int shooter;
    [SerializeField] private AudioSource audioSource;

    public FirePoints firePoint;
    public Weapon currentWeapon;
    private GameObject prefabBullet;

    private float fireRate;
    private float interval;
    private bool _shooting = false;
    
    // Start is called before the first frame update
    void Start()
    {
        prefabBullet = currentWeapon.bullet;
        fireRate = currentWeapon.FireRate;
        interval = 1 / (fireRate * shootingEntity.Stats[Entity.Attribute.FireRateRatio].Value);
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
                    
                    GameObject myBullet = ObjectPoolManager.Instance.GetPoolObject(prefabBullet.name);
                    Rigidbody2D rigidBody = myBullet.GetComponent<Rigidbody2D>();
                    myBullet.transform.position = firePoint.points[index].transform.position;
                    myBullet.SetActive(true);
                    rigidBody.AddForce(firePoint.points[index].transform.up * currentWeapon.bulletForce, ForceMode2D.Impulse);
                    myBullet.GetComponent<BulletDamage>().damage = currentWeapon.Damage;
                    myBullet.layer = shooter;
                    float angulo = Mathf.Atan2(rigidBody.velocity.y, rigidBody.velocity.x) * Mathf.Rad2Deg;
                    myBullet.transform.rotation = Quaternion.AngleAxis(angulo - 90, Vector3.forward);
                    SoundFXManager.instance.PlaySoundFXClip(currentWeapon._shootingSound.clip, transform, _volumeShooting);
                }


                return;
            }
            interval -= Time.deltaTime;
        }
    }
    public void EnableShoot(InputAction.CallbackContext input)
    {
        _animator.SetTrigger("isShooting");
        StartShooting();
    }
    
    public void DisableShoot(InputAction.CallbackContext input)
    {
        _animator.ResetTrigger("isShooting");
        _animator.SetTrigger("isIdle");
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
        if (weaponChangeSoundClips != null)
        {
            SoundFXManager.instance.PlaySoundFXClip(weaponChangeSoundClips, transform, _volumeWeaponChange);
        }
        currentWeapon = newWeapon;
        prefabBullet = currentWeapon.bullet;
        fireRate = currentWeapon.FireRate;
        interval = 1f / fireRate;
    }

}
