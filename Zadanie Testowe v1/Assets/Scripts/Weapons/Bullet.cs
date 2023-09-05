using UnityEngine;

public class Bullet : MonoBehaviour
{
	private byte damage;
	public byte Damage { get { return damage; } set { damage = value; } }
	private PhysicMaterial damageableMaterial;
	public PhysicMaterial DamageableMaterial { get { return damageableMaterial; } set { damageableMaterial = value; } }

	private void OnCollisionEnter(Collision other)
	{
		Debug.Log($"Bullet DMG Type: {DamageableMaterial}");
		//Debug.Log($"Target Type: {other.gameObject.GetComponent<ObjectBase>().Material}");
		if (other.collider.tag == "Target" && other.gameObject.GetComponent<ObjectBase>().PhysicMaterial == DamageableMaterial)
		{
			other.gameObject.GetComponent<ObjectBase>().TakeDamage(damage);
		}
		Destroy(this.gameObject);
	}
}