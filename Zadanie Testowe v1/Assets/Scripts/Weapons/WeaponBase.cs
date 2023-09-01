using UnityEngine;

public enum DamagableMaterial
{
	Wood = 0,
	Stone = 1,
	Steel = 2
}

public abstract class WeaponBase : MonoBehaviour
{
	public byte Damage;
	public byte FireSpeed;
	public DamagableMaterial Material;

	public virtual void Fire()
	{

	}
}