using UnityEngine;

public class Bullet : MonoBehaviour
{
	public byte Damage { get; set; }
	public PhysicMaterial DamageableMaterial { get; set; }
	
	private void OnTriggerEnter(Collider other)
	{
		Debug.Log($"Bullet DMG Type: {DamageableMaterial}");
		//Debug.Log($"Target Type: {other.gameObject.GetComponent<ObjectBase>().Material}");
		if (other.GetComponent<Collider>().CompareTag("Target") && other.gameObject.GetComponent<ObjectBase>().PhysicMaterial == DamageableMaterial)
		{
			other.gameObject.GetComponent<ObjectBase>().TakeDamage(Damage);
		}
		Destroy(this.gameObject);
	}
}