using UnityEngine;

public class Gun : MonoBehaviour
{
	[SerializeField] private WeaponBase weaponBase;
	[SerializeField] private Transform firePoint;
	[SerializeField] private Animator animator;

	private void Awake()
	{
		animator = GetComponent<Animator>();
		weaponBase.Setup(firePoint, animator);
	}

	// Update is called once per frame
	void Update()
	{
		weaponBase.Aim();

		if(Input.GetKeyDown(KeyCode.R))
		{
			weaponBase.Reload();
		}
	}

	public void OnWeaponSelect()
	{
		weaponBase.OnWeaponSelected();
	}

	public void ReloadEnd()
	{
		weaponBase.SetAmmo();
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawLine(firePoint.transform.position, weaponBase.hit.point);
	}
}