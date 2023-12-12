using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{

    [SerializeField] private AudioClip[] shootingSoundClips;
    [SerializeField][Range(0, 1)] private float volumeSFX;

    public Transform firePoint;
    public Weapon currentWeapon;
    private Bullet bullet;
    private float fireRate;
    private float interval;

    // Start is called before the first frame update
    void Start()
    {
        bullet = currentWeapon.bullet.GetComponent<Bullet>();
        fireRate = currentWeapon.FireRate;
        interval = 1f / fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && interval <= 0)
        {
            Shoot();
            interval = 1 / fireRate;
        }
        interval -= Time.deltaTime;
    }

    void Shoot()
    {
        GameObject bulletGO = Instantiate(currentWeapon.bullet, firePoint.position, Quaternion.identity);
        bulletGO.GetComponent<Rigidbody2D>().AddForce(firePoint.up * bullet.bulletForce, ForceMode2D.Impulse);
        //SoundFXManager.instance.PlaySoundFXClip(shootingSoundClip, transform, volumeSFX);
        SoundFXManager.instance.PlayRandomSoundFXClip(shootingSoundClips, transform, volumeSFX);
    }

    private void WeaponChange(Weapon newWeapon)
    {
        currentWeapon = newWeapon;
        bullet = currentWeapon.bullet.GetComponent<Bullet>();
        fireRate = currentWeapon.FireRate;
        interval = 1f / fireRate;
    }


    private void OnEnable()
    {
        global::WeaponChange.OnWeaponChange += WeaponChange;
    }

    private void OnDisable()
    {
        global::WeaponChange.OnWeaponChange -= WeaponChange;
    }
}
