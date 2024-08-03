using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeThrower : MonoBehaviour
{
	[SerializeField] Transform bulletSpawnPoint;
	[SerializeField] GameObject bulletPrefab;
	[SerializeField] float bulletSpeed = 100.0f;


	// Update is called once per frame
	void Update()
	{
		if (Input.GetButtonDown("Fire1"))
		{
			Shoot();
		}
	}
	void Shoot()
	{
		var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
		var bulletRb = bullet.GetComponent<Rigidbody>();
		bulletRb.AddForce(bulletSpawnPoint.forward * bulletSpeed * Time.deltaTime, ForceMode.VelocityChange);
	}
}
