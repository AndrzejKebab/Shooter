using UnityEngine;

public class FuctionTest : OnDestroyFunction
{
	[SerializeField] private GameObject nextTarget;

	public override void Destroy()
	{
		nextTarget.SetActive(true);
		Destroy(this.gameObject);
	}
}