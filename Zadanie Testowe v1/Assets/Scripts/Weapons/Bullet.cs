using UnityEngine;

public class Bullet : MonoBehaviour
{
	private byte damage;
	public byte Damage {  get { return damage; } set { damage = value; } }
	public DamagableMaterial DamagableMaterial;

	private void OnCollisionEnter(Collision other)
	{
		Debug.Log($"Bullet DMG Type: {DamagableMaterial}");
		Debug.Log($"Target Type: {other.gameObject.GetComponent<ObjectBase>().Material}");
		if (other.collider.tag == "Target" && other.gameObject.GetComponent<ObjectBase>().Material == DamagableMaterial)
		{
			other.gameObject.GetComponent<ObjectBase>().TakeDamage(damage);
		}
		Destroy(this.gameObject);
	}
}