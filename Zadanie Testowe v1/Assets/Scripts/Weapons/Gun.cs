using UnityEngine;

public class Gun : MonoBehaviour
{
	[SerializeField] private WeaponBase weaponBase;
	[SerializeField] private Transform firePoint;

	private void Awake()
	{
		firePoint = GameObject.Find($"{transform.name}/GunHands/GunBase/FirePoint").GetComponent<Transform>();
		weaponBase.firePoint = firePoint;
	}

	// Update is called once per frame
	void Update()
	{
		weaponBase.Shoot();
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		//Vector3 direction = firePoint.transform.TransformDirection(Vector3.forward) * 5;
		Gizmos.DrawLine(firePoint.transform.position, weaponBase.hit.point);
	}
}