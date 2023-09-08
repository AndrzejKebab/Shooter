using TMPro;
using UnityEngine;

public enum PhysicMaterial
{
	Wood = 0,
	Stone = 1,
	Steel = 2
}

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapons")]
public class WeaponBase : ScriptableObject
{
	[Header("Weapon")]
	[HideInInspector] public string WeaponName;
	public byte Damage;
	public byte MagazineSize;
	private byte bulletsInMagazine;
	public float FireRate;
	private float timeBtwShoots = 0;
	private bool isReloading = false;
	public PhysicMaterial DamageableMaterial;
	public AudioClip WeaponFireSound;
	public AudioClip WeaponEmptyFireSound;
	public AudioClip WeaponReload;
	public AudioClip WeaponEmptyReload;
	private AudioSource weaponSoundSorce;
	private Animator animator;

	[Header("Bullet")]
	public GameObject BulletPrefab;
	private Transform firePoint;
	public float BulletForce = 20;
	public float BulletLifeTime = 2f;

	private Ray ray;
	public RaycastHit Hit;
	private Camera camera;
	private Vector2 screenSize;
	private TextMeshProUGUI weaponNameText;
	private TextMeshProUGUI weaponDamageableMaterialText;
	private TextMeshProUGUI weaponAmmoText;

	public void Setup(Transform firePoint, Animator animator, string weaponName, AudioSource audioSource)
	{
		bulletsInMagazine = MagazineSize;
		this.animator = animator;
		this.firePoint = firePoint;
		WeaponName = weaponName;
		weaponNameText = GameObject.Find("WeaponName").GetComponent<TextMeshProUGUI>();
		weaponDamageableMaterialText = GameObject.Find("DamageableMaterial").GetComponent<TextMeshProUGUI>();
		weaponAmmoText = GameObject.Find("Ammo").GetComponent<TextMeshProUGUI>();
		weaponSoundSorce = audioSource;
		camera = Camera.main;
		screenSize.x = Screen.width;
		screenSize.y = Screen.height;
	}

	public void UpdateWeaponText()
	{
		weaponNameText.text = WeaponName;
		weaponDamageableMaterialText.text = $"Damageable Material: {DamageableMaterial}";
		weaponAmmoText.text = $"Ammo: {bulletsInMagazine} / {MagazineSize}";
	}

	public void Aim()
	{
		ray = camera.ScreenPointToRay(new Vector3(screenSize.x / 2, screenSize.y / 2, 0));

		if (Physics.Raycast(ray, out Hit))
		{
			firePoint.LookAt(Hit.point);
		}
		else
		{
			firePoint.LookAt(firePoint.position + firePoint.forward);
		}

		if(InputManager.Instance.GetFirePressed()) Fire();
	}

	private void Fire()
	{
		if (isReloading)
		{
			weaponSoundSorce.Stop();
			isReloading = false;
		}
		
		if (timeBtwShoots <= 0 && bulletsInMagazine > 0)
		{
			animator.Play("Fire");
			weaponSoundSorce.PlayOneShot(WeaponFireSound, 0.10f);
			
			GameObject _bullet = Instantiate(BulletPrefab, firePoint.position, firePoint.rotation.normalized);
			_bullet.GetComponentInChildren<Bullet>().Damage = Damage;
			_bullet.GetComponentInChildren<Bullet>().DamageableMaterial = DamageableMaterial;
			
			Rigidbody _bulletRb = _bullet.GetComponentInChildren<Rigidbody>();
			_bulletRb.AddForce(firePoint.forward * BulletForce, ForceMode.Impulse);
			
			Destroy(_bullet, BulletLifeTime);
			
			timeBtwShoots = 1 / FireRate;
			bulletsInMagazine -= 1;
			
			UpdateWeaponText();
		}
		else if(timeBtwShoots <= 0 && bulletsInMagazine == 0 )
		{
			 weaponSoundSorce.PlayOneShot(WeaponEmptyFireSound, 0.30f);
			 timeBtwShoots = 1 / FireRate;
		}
		else
		{
			timeBtwShoots -= Time.deltaTime;
		}
	}

	public void Reload()
	{
		if (bulletsInMagazine == MagazineSize) return;

		isReloading = true;
		if(bulletsInMagazine > 0)
		{
			animator.Play("Reload");
			weaponSoundSorce.PlayOneShot(WeaponReload);
		}
		else if(bulletsInMagazine == 0)
		{
			animator.Play("ReloadNoAmmo");
			weaponSoundSorce.PlayOneShot(WeaponEmptyReload);
		}
	}

	public void SetAmmo()
	{
		bulletsInMagazine = MagazineSize;
		UpdateWeaponText();
		isReloading = false;
	}
}