using TMPro;
using UnityEditor.Build.Content;
using UnityEngine;

public enum DamagableMaterial
{
	Wood = 0,
	Stone = 1,
	Steel = 2
}

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapons")]
public class WeaponBase : ScriptableObject
{
	[Header("Weapon")]
	public byte Damage;
	public byte MagazineSize;
	private byte bulletsInMagazine;
	public float FireRate;
	private float timeBtwShoots = 0;
	public DamagableMaterial Material;
	[HideInInspector] public Animator animator;

	[Header("Bullet")]
	public GameObject bulletPrefab;
	[HideInInspector] public Transform firePoint;
	public float bulletForce = 20;
	public float bulletLifeTime = 2f;

	private Ray ray;
	public RaycastHit hit;
	private TextMeshProUGUI weaponName;
	private TextMeshProUGUI weaponDamagableMaterial;
	private TextMeshProUGUI weaponAmmo;

	public void Setup(Transform _firepoint, Animator _animator)
	{
		bulletsInMagazine = MagazineSize;
		animator = _animator;
		firePoint = _firepoint;
		weaponName = GameObject.Find("WeaponName").GetComponent<TextMeshProUGUI>();
		weaponDamagableMaterial = GameObject.Find("DamagableMaterial").GetComponent<TextMeshProUGUI>();
		weaponAmmo = GameObject.Find("Ammo").GetComponent<TextMeshProUGUI>();
	}

	public void OnWeaponSelected()
	{
		weaponName.text = name;
		weaponDamagableMaterial.text = $"{Material}";
		weaponAmmo.text = $"{bulletsInMagazine} / {MagazineSize}";
	}

	public void Aim()
	{
		ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

		if (Physics.Raycast(ray, out hit))
		{
			firePoint.LookAt(hit.point);
		}
		else
		{
			firePoint.LookAt(firePoint.position + firePoint.forward);
		}

		Fire();
	}

	public void Fire()
	{
		if (timeBtwShoots <= 0 && InputManager.instance.GetFirePressed() && bulletsInMagazine > 0)
		{
			Debug.Log("Fire");
			GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation.normalized);
			animator.Play("Fire");
			bullet.GetComponentInChildren<Bullet>().Damage = Damage;
			bullet.GetComponentInChildren<Bullet>().DamagableMaterial = Material;
			Rigidbody bulletRB = bullet.GetComponentInChildren<Rigidbody>();
			bulletRB.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse);
			Destroy(bullet, bulletLifeTime);
			timeBtwShoots = 1 / FireRate;
			bulletsInMagazine -= 1;
			weaponAmmo.text = $"{bulletsInMagazine} / {MagazineSize}";
		}
		else
		{
			timeBtwShoots -= Time.deltaTime;
		}
	}

	public void Reload()
	{
		if (bulletsInMagazine == MagazineSize) return;

		if(bulletsInMagazine > 0)
		{
			animator.Play("Reload");
		}
		else if(bulletsInMagazine == 0)
		{
			animator.Play("ReloadNoAmmo");
		}

	}

	public void SetAmmo()
	{
		bulletsInMagazine = MagazineSize;
		weaponAmmo.text = $"{bulletsInMagazine} / {MagazineSize}";
	}
}