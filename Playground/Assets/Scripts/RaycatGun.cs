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

	[Header("Gun Reloading Settings")]
	float reloadTime = 1f;
	bool isReloading = false;
	int currentAmmo = -1;
	int maxAmmo = 10;

	[Header("Gun animator Settings")]
	[SerializeField] Animator animator;
	void Start()
	{
		if (currentAmmo <= -1)
		{
			currentAmmo = maxAmmo;
		}
	}
	void OnEnable()
	{
		isReloading = false;
		animator.SetBool("isReloading", false);
	}
	// Update is called once per frame
	void Update()
	{
		if (isReloading)
		{
			return;
		}

		if (currentAmmo <= 0)
		{
			StartCoroutine(Reload());
			return;
		}
		if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
		{
			nextTimeToFire = Time.time + 1f / fireRate;
			Shoot();
		}
	}

	IEnumerator Reload()
	{
		isReloading = true;
		Debug.Log("Reloading...");

		animator.SetBool("isReloading", true);

		yield return new WaitForSeconds(reloadTime - .25f);
		animator.SetBool("isReloading", false);
		yield return new WaitForSeconds(.25f);

		currentAmmo = maxAmmo;
		isReloading = false;
	}
	void Shoot()
	{
		muzzleFlash.Play();

		currentAmmo--;

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
