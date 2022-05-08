using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private GameObject bulletPref;
    [SerializeField] private Transform firePoint;
    
    [SerializeField] private float fireForce;

    [SerializeField] private float rotationSpeed;

    private void Update()
    {
        float inputX = Input.GetAxis("Horizontal");

        float newAngle = transform.eulerAngles.z + inputX * rotationSpeed * Time.deltaTime * -1;
        if ((newAngle < 75f || newAngle > 285f) && newAngle > -75f)
        {
            transform.eulerAngles = new Vector3(0, 0, newAngle);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        GameObject tempBullet = Instantiate(bulletPref);

        if (tempBullet == null)
            return;

        tempBullet.transform.position = firePoint.position;
        tempBullet.transform.rotation = firePoint.rotation;

        tempBullet.GetComponent<Rigidbody2D>().AddForce(tempBullet.transform.up * fireForce, ForceMode2D.Impulse);

        Debug.Log(transform.eulerAngles.z < 180 ? transform.eulerAngles.z : transform.eulerAngles.z - 360);
        AudioManager.Instance.BulletShotSide((transform.eulerAngles.z < 180 ? transform.eulerAngles.z : transform.eulerAngles.z - 360) / -75);
        AudioManager.Instance.BulletShot();
    }
}
