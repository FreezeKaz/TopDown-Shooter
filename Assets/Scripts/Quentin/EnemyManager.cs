using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    public Transform firePoint;
    public GameObject bulletPrefab;

    public Weapon weapon;

    private float fireRate;
    private float interval;
    public float bulletForce = 20f;

    void Start()
    {
        fireRate = weapon.FireRate;
        interval = 1f / fireRate;
    }

    public void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().AddForce(transform.up * bulletForce, ForceMode2D.Impulse);
        //SoundFXManager.instance.PlaySoundFXClip(shootingSoundClip, transform, volumeSFX);
        //SoundFXManager.instance.PlayRandomSoundFXClip(shootingSoundClips, transform, volumeSFX);
    }
}
