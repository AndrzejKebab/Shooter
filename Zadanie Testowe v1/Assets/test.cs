using System.Collections;
using UnityEngine;

public class test : MonoBehaviour
{
	private Material material;
	private float time;

	// Start is called before the first frame update
	void Start()
	{
		material = GetComponent<MeshRenderer>().sharedMaterial;
		material.SetFloat("_Fade", -0.1f);
	}

	// Update is called once per frame
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.G))
		{
			StartCoroutine(Fade());
		}
	}

	private IEnumerator Fade()
	{
		while(time <= 1)
		{
			time += 0.01f;
			material.SetFloat("_Fade", time);
			yield return null;
		}
	}

	private void OnDestroy()
	{
		material.SetFloat("_Fade", -0.1f);
	}
}
