using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycatGun : MonoBehaviour
{
	float damage = 10f;
	float range = 100f;
	float impactForce = 10f;
	[SerializeField] Transform bulletSpawnPoint;
	[SerializeField] ParticleSystem muzzleFlash;
	[SerializeField] GameObject impactEffect;

	[Header("Gun FireRate Settings")]
	float fireRate = 15f;
	float nextTimeToFire = 0f;

	// Update is called once per frame
	void Update()
	{
		if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
		{
			nextTimeToFire = Time.time + 1f / fireRate;
			Shoot();
		}
	}

	void Shoot()
	{
		muzzleFlash.Play();

		RaycastHit hit;
		if (Physics.Raycast(bulletSpawnPoint.position, bulletSpawnPoint.forward, out hit, range))
		{
			Debug.Log(hit.transform.name);

			Target target = hit.transform.GetComponent<Target>();
			if (target != null)
			{
				target.TakeDamage(damage);
			}
			if (hit.rigidbody != null)
			{
				hit.rigidbody.AddForce(-hit.normal * impactForce);
			}
			GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
			Destroy(impactGO, 1f);
		}
	}
}
