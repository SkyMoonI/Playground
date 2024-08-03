using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
	float timer = 3f;
	float countdown;
	[SerializeField] float explosionRadius = 5f;
	bool hasExploded = false;
	[SerializeField] float explosionForce = 700f;


	[SerializeField] GameObject explosionEffect;

	// Start is called before the first frame update
	void Start()
	{
		countdown = timer;
	}

	// Update is called once per frame
	void Update()
	{
		countdown -= Time.deltaTime;
		if (countdown <= 0 && !hasExploded)
		{
			Explode();
			hasExploded = true;
		}
	}

	private void Explode()
	{
		GameObject effect = Instantiate(explosionEffect, transform.position, transform.rotation);

		Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
		foreach (Collider nearbyObject in colliders)
		{
			Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
			if (rb != null)
			{
				rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
			}
		}

		Destroy(gameObject);
		Destroy(effect, 2f);
	}
}
