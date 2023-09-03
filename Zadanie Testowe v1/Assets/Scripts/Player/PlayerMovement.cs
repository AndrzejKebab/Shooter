using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	private Camera _camera;
	private Rigidbody _rigidbody;

	[Header("Player Movement")]
	[Tooltip("Speed with what player will move.")]
	[SerializeField] private short movementSpeed = 5;
	[Tooltip("How fast player will look around.")]
	[SerializeField] private short mouseSensivity = 10;
	private Vector2 mousePos;
	private Vector2 movement;
	private float cameraPitch;

	[Header("Player Jump")]
	[Tooltip("The height the player can jump")]
	[SerializeField] private float jumpHeight = 1.2f;
	[Tooltip("Time required to pass before being able to jump again.")]
	[SerializeField] private float JumpTimeout = 0.1f;
	private float _jumpTimeoutDelta;

	[Header("Player Grounded")]
	[Tooltip("If the character is grounded or not.")]
	[SerializeField] private bool Grounded = true;
	[Tooltip("Useful for rough ground")]
	[SerializeField] private float GroundedOffset = -0.14f;
	[Tooltip("The radius of the grounded check.")]
	[SerializeField] private float GroundedRadius = 0.5f;
	[Tooltip("What layers the character uses as ground")]
	[SerializeField] private LayerMask GroundLayers;

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
		cameraPitch -= mousePos.y * Time.deltaTime;
		cameraPitch = ClampAngle(cameraPitch, -90, 90);
		_camera.transform.localRotation = Quaternion.Euler(cameraPitch, 0.0f, 0.0f);

		GroundedCheck();
		JumpAndGravity();
	}

	private void FixedUpdate()
	{
		movement = InputManager.instance.GetMovement();
		Vector3 velocity = new Vector3();
		velocity = ((transform.forward * movement.y) + (transform.right * movement.x)) * movementSpeed;
		transform.position += velocity * Time.deltaTime;
	}

	private void GroundedCheck()
	{
		Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z);
		Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers, QueryTriggerInteraction.Ignore);
	}

	private void JumpAndGravity()
	{
		if (Grounded)
		{
			if (InputManager.instance.GetJumpPressed() == true && _jumpTimeoutDelta <= 0.0f)
			{
				_rigidbody.AddForce(new Vector3(0 , jumpHeight, 0), ForceMode.Impulse); ;
			}

			if (_jumpTimeoutDelta >= 0.0f)
			{
				_jumpTimeoutDelta -= Time.deltaTime;
			}
		}
		else
		{
			_jumpTimeoutDelta = JumpTimeout;
		}
	}

	private float ClampAngle(float lfAngle, float lfMin, float lfMax)
	{
		if (lfAngle < -360f) lfAngle += 360f;
		if (lfAngle > 360f) lfAngle -= 360f;
		return Mathf.Clamp(lfAngle, lfMin, lfMax);
	}

	private void OnDrawGizmosSelected()
	{
		Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
		Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

		if (Grounded)
		{
			Gizmos.color = transparentGreen;
		}
		else
		{
			Gizmos.color = transparentRed;
		}

		Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z), GroundedRadius);
	}
}