using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObjectBase : MonoBehaviour
{
	private Transform _camera;
	private Transform _statsCanvas;
	private short health;
	private Slider healthBar;
	private TextMeshProUGUI healthBarText;
	private TextMeshProUGUI materialTypeText;
	[SerializeField] private short maxHealth;
	[SerializeField] private DamagableMaterial material;
	[SerializeField] private OnDestroyFunction test;

	// Start is called before the first frame update
	void Start()
	{
		_camera = Camera.main.transform;
		_statsCanvas = GameObject.Find($"{gameObject.name}/Canvas").GetComponent<Transform>();
		health = maxHealth;
		healthBar = GetComponentInChildren<Slider>();
		healthBar.maxValue = maxHealth;
		healthBar.value = maxHealth;
		healthBarText = GameObject.Find($"{gameObject.name}/Canvas/Panel/HPBar/HPText").GetComponent<TextMeshProUGUI>();
		healthBarText.text = $"{health} / {maxHealth}";
		materialTypeText = GetComponentInChildren<TextMeshProUGUI>();
		materialTypeText.text = $"Material Type: <br>{material}";
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.G))
		{
			TakeDamage(5);
		}

		_statsCanvas.transform.LookAt(_statsCanvas.position + _camera.rotation * Vector3.forward, _camera.rotation * Vector3.up );
	}

	public void ObjectDestroy()
	{
		test.Destroy();
	}

	public void TakeDamage(byte damage)
	{
		if (health <= 0) return;

		health -= damage;
		healthBar.value = health;
		healthBarText.text = $"{health} / {maxHealth}";

		if (health <= 0)
		{
			ObjectDestroy();
		}
	}
}