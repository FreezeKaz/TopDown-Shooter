using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{

    public Transform firePoint;
    public GameObject bulletPrefab;

    public Weapon weapon;

    private float fireRate;
    private float interval;
    public float bulletForce = 20f;

    // Start is called before the first frame update
    void Start()
    {
        fireRate = weapon.FireRate;
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
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }
}
