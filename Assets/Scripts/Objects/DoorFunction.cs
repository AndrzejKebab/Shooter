using System.Collections;
using UnityEngine;

public class DoorFunction : OnDestroyFunction
{
	[SerializeField] private GameObject Door;
	private Material material;
	private float time;

	private void Start()
	{
		material = Door.GetComponent<MeshRenderer>().sharedMaterial;
		material.SetFloat("_Fade", -0.1f);
	}

	public override void Destroy()
	{
		StartCoroutine(Fade());
	}

	private IEnumerator Fade()
	{
		while (time <= 1)
		{
			time += 0.01f;
			material.SetFloat("_Fade", time);
			yield return null;
		}
		Door.SetActive(false);
		Destroy(this.gameObject);
	}

	private void OnDestroy()
	{
		material.SetFloat("_Fade", -0.1f);
	}
}