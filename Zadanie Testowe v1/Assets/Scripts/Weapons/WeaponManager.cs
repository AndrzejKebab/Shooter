using UnityEngine;

public class WeaponManager : MonoBehaviour
{
	[SerializeField] private KeyCode[] keyCodes;
	[SerializeField] private GameObject[] weapons;
	private byte currentWeapon = 0;
	[SerializeField] private float weaponSwitchTime = 0.12f;
	private float lastWeaponSwitchTime = 0;

	public void Start()
	{
		SetWeapon();
		SelectWeapon(currentWeapon);
	}

	public void Update()
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

	private void SetWeapon()
	{
		weapons = new GameObject[transform.childCount];

		for (byte i = 0; i < transform.childCount; i++)
		{
			weapons[i] = transform.GetChild(i).gameObject;
		}
	}

	private void SelectWeapon(byte indexWeapon)
	{
		for (byte i = 0; i < weapons.Length; i++)
		{
			bool _isRightWeapon = i == indexWeapon;
			weapons[i].SetActive(_isRightWeapon);
			if(_isRightWeapon) weapons[i].GetComponent<Gun>().OnWeaponSelected();
		}

		lastWeaponSwitchTime = 0;
	}
}