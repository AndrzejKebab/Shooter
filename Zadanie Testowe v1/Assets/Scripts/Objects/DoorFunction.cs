using UnityEngine;

public class DoorFunction : OnDestroyFunction
{
	public GameObject Door;

	public override void Destroy()
	{
		Door.SetActive(false);
	}
}
