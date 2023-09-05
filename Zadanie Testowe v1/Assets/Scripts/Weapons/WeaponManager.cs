using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
	[SerializeField] private KeyCode[] keyCodes;
	[SerializeField] private GameObject[] weapons;
	private byte currentWeapon = 0;
	[SerializeField] private float weaponSwitchTime = 0.12f;
	private float lastWeaponSwitchTime = 0;

	// Start is called before the first frame update
	void Start()
	{
		SetWeapon();
		SelectWeapon(currentWeapon);
	}

	// Update is called once per frame
	void Update()
	{
		byte _previousWeapon = currentWeapon;

		for (byte i = 0; i < keyCodes.Length; i++)
		{
			if (Input.GetKeyDown(keyCodes[i]) && lastWeaponSwitchTime >= weaponSwitchTime)
			{
				currentWeapon = i;
			}
		}

		if (_previousWeapon != currentWeapon)
		{
			SelectWeapon(currentWeapon);
		}

		lastWeaponSwitchTime += Time.deltaTime;
	}

	public void SetWeapon()
	{
		weapons = new GameObject[transform.childCount];

		for (byte i = 0; i < transform.childCount; i++)
		{
			weapons[i] = transform.GetChild(i).gameObject;
		}
	}

	public void SelectWeapon(byte indexWeapon)
	{
		for (byte i = 0; i < weapons.Length; i++)
		{
			weapons[i].SetActive(i == indexWeapon);
			weapons[i].GetComponent<Gun>().OnWeaponSelect();
		}

		lastWeaponSwitchTime = 0;
	}
}