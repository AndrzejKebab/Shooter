using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class ObjectBase : MonoBehaviour
{
	private byte Health;
	[SerializeField] private byte MaxHealth;
	[SerializeField] private DamagableMaterial material;
	[SerializeField] private OnDestroyFunction test;

	// Start is called before the first frame update
	void Start()
	{
		Health = MaxHealth;
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.G))
		{
			ObjectDestroy();
		}
	}

	public void ObjectDestroy()
	{
		test.Destroy();
	}
}