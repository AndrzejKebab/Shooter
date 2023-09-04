using UnityEngine;

public class FuctionTest : OnDestroyFunction
{
	public GameObject spawn;

	public override void Destroy()
	{
		Instantiate(spawn,Vector3.up, Quaternion.identity);
	}
}