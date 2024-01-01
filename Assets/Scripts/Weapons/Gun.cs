using UnityEngine;

public class Gun : MonoBehaviour
{
	[SerializeField] private WeaponBase weaponBase;
	[SerializeField] private Transform firePoint;
	[SerializeField] private Animator animator;

	private void Awake()
	{
		animator = GetComponent<Animator>();
		weaponBase.Setup(firePoint, animator, gameObject.name, GetComponent<AudioSource>());
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

	//Function exposed for weapon manager.
	public void OnWeaponSelected()
	{
		weaponBase.UpdateWeaponText();
	}

	//Function exposed for animator event.
	public void ReloadEnd()
	{
		weaponBase.SetAmmo();
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawLine(firePoint.transform.position, weaponBase.Hit.point);
	}
}