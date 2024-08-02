using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
	int currentWeapon = 0;
	// Start is called before the first frame update
	void Start()
	{
		SelectWeapon();
	}

	// Update is called once per frame
	void Update()
	{
		int previousWeapon = currentWeapon;

		if (Input.GetAxis("Mouse ScrollWheel") > 0f)
		{
			if (currentWeapon >= transform.childCount - 1)
			{
				currentWeapon = 0;
			}
			else
			{
				currentWeapon++;
			}
		}
		else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
		{
			if (currentWeapon <= 0)
			{
				currentWeapon = transform.childCount - 1;
			}
			else
			{
				currentWeapon--;
			}
		}

		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			currentWeapon = 0;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
		{
			currentWeapon = 1;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3)
		{
			currentWeapon = 2;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha4) && transform.childCount >= 4)
		{
			currentWeapon = 3;
		}

		if (previousWeapon != currentWeapon)
		{
			SelectWeapon();
		}
	}

	void SelectWeapon()
	{
		int weaponIndex = 0;
		foreach (Transform weapon in transform)
		{
			if (weaponIndex == currentWeapon)
			{
				weapon.gameObject.SetActive(true);
			}
			else
			{
				weapon.gameObject.SetActive(false);
			}
			weaponIndex++;
		}
	}
}
