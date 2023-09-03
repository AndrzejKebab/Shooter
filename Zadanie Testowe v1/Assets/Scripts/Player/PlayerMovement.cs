using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class PlayerMovement : MonoBehaviour
{
	private Camera _camera;
	private Rigidbody _rigidbody;

	[Header("Player Movement")]
	[SerializeField] private short movementSpeed = 5;
	[SerializeField] private short mouseSensivity = 10;
	private Vector2 mousePos;
	private Vector2 movement;
	private float cameraPitch;

	[Header("Player Grounded")]
	[Tooltip("If the character is grounded or not.")]
	[SerializeField] public bool Grounded = true;
	[Tooltip("Useful for rough ground")]
	[SerializeField] public float GroundedOffset = -0.14f;
	[Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
	[SerializeField] public float GroundedRadius = 0.5f;
	[Tooltip("What layers the character uses as ground")]
	[SerializeField] public LayerMask GroundLayers;

	void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
		_camera = Camera.main; 
		_rigidbody = GetComponent<Rigidbody>();
	}

	void Update()
	{
		mousePos = InputManager.instance.GetMousePos() * mouseSensivity;
		transform.Rotate(Vector3.up, mousePos.x * Time.deltaTime);
		//_camera.transform.Rotate(Vector3.right, -mousePos.y * Time.deltaTime);
		cameraPitch -= mousePos.y * Time.deltaTime;
		cameraPitch = ClampAngle(cameraPitch, -90, 90);
		_camera.transform.localRotation = Quaternion.Euler(cameraPitch, 0.0f, 0.0f);
	}

	private void FixedUpdate()
	{
		movement = InputManager.instance.GetMovement();
		Vector3 velocity = new Vector3();
		velocity = ((transform.forward * movement.y) + (transform.right * movement.x)) * movementSpeed;
		_rigidbody.velocity = velocity;
	}

	private float ClampAngle(float lfAngle, float lfMin, float lfMax)
	{
		if (lfAngle < -360f) lfAngle += 360f;
		if (lfAngle > 360f) lfAngle -= 360f;
		return Mathf.Clamp(lfAngle, lfMin, lfMax);
	}
}