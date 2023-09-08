using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObjectBase : MonoBehaviour
{
	private Transform _camera;
	private Transform statsCanvas;
	private short health;
	private Slider healthBar;
	private TextMeshProUGUI healthBarText;
	private TextMeshProUGUI materialTypeText;

	[Header("Stats")]
	[Tooltip("Object max health.")]
	[SerializeField] private short maxHealth;
	[Tooltip("Material that object is made from.")]
	[SerializeField] private PhysicMaterial physicMaterial;
	public PhysicMaterial PhysicMaterial { get { return physicMaterial; } }
	[Tooltip("Function that will happen when object is destroyed.")]
	[SerializeField] private OnDestroyFunction onDestroyFunction;

	// Start is called before the first frame update
	public void Start()
	{
		_camera = Camera.main.transform;
		statsCanvas = GameObject.Find($"{gameObject.name}/Canvas").GetComponent<Transform>();
		health = maxHealth;
		healthBar = GetComponentInChildren<Slider>();
		healthBar.maxValue = maxHealth;
		healthBar.value = maxHealth;
		healthBarText = GameObject.Find($"{gameObject.name}/Canvas/Panel/HPBar/HPText").GetComponent<TextMeshProUGUI>();
		healthBarText.text = $"{health} / {maxHealth}";
		materialTypeText = GetComponentInChildren<TextMeshProUGUI>();
		materialTypeText.text = $"Material Type: <br>{physicMaterial}";
	}

	// Update is called once per frame
	public void Update()
	{
		statsCanvas.transform.LookAt(statsCanvas.position + _camera.rotation * Vector3.forward, _camera.rotation * Vector3.up );
	}
    
	public void TakeDamage(byte damage)
	{
		if (health <= 0) return;

		health -= damage;
		healthBar.value = health;
		healthBarText.text = $"{health} / {maxHealth}";

		if (health <= 0)
		{
			OnObjectDestroy();
		}
	}
	
	private void OnObjectDestroy()
	{
		onDestroyFunction.Destroy();
	}
}