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
	//public Transform FirePoint { get { return firePoint; } set { firePoint = value; } }
	public float bulletForce = 20;
	public float bulletLifeTime = 2f;

	Ray ray;
	public RaycastHit hit;

	public void Shoot()
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
		Debug.Log("Fire");
		if (timeBtwShoots <= 0 && InputManager.instance.GetFirePressed())
		{
			Debug.Log("theBullet");
			GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation.normalized) ;
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