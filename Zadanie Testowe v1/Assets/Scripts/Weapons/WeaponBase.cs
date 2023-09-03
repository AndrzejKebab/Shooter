using UnityEngine;

public enum DamagableMaterial
{
	Wood = 0,
	Stone = 1,
	Steel = 2
}

public abstract class WeaponBase : ScriptableObject
{
	[Header("Weapon")]
	public byte Damage;
	public float FireRate;
	private float timeBtwShoots = 0;
	public DamagableMaterial Material;

	[Header("Bullet")]
	public GameObject bulletPrefab;
	public Transform firePoint;
	public float bulletForce = 20;
	public float bulletLifeTime = 2f;

	Ray ray;
	public RaycastHit hit;

	public virtual void Shoot()
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

	public virtual void Fire()
	{
		if (timeBtwShoots <= 0 && InputManager.instance.GetFirePressed())
		{
			GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation.normalized);
			bullet.GetComponentInChildren<Bullet>().Damage = Damage;
			bullet.GetComponentInChildren<Bullet>().DamagableMaterial = Material;
			Rigidbody bulletRB = bullet.GetComponentInChildren<Rigidbody>();
			bulletRB.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse);
			Destroy(bullet, bulletLifeTime);
			timeBtwShoots = 1 / FireRate;			
		}
		else
		{
			timeBtwShoots -= Time.deltaTime;
		}
	}
}