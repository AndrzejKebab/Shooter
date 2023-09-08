using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	private Camera _camera;
	private Rigidbody _rigidbody;

	[Header("Player Movement")]
	[Tooltip("Speed with what player will move.")]
	[SerializeField] private short movementSpeed = 7;
	[Tooltip("How fast player will look around.")]
	[SerializeField] private short mouseSensivity = 10;
	private Vector2 mousePos;
	private Vector2 movement;
	private Vector3 velocity;
	private float cameraPitch;

	[Header("Player Jump")]
	[Tooltip("The height the player can jump")]
	[SerializeField] private float jumpHeight = 1.2f;
	[Tooltip("Time required to pass before being able to jump again.")]
	[SerializeField] private float jumpTimeout = 0.1f;
	private float jumpTimeoutDelta;

	[Header("Player Grounded")]
	[Tooltip("If the character is grounded or not.")]
	[SerializeField] private bool grounded = true;
	[Tooltip("Useful for rough ground")]
	[SerializeField] private float groundedOffset = 0.75f;
	[Tooltip("The radius of the grounded check.")]
	[SerializeField] private float groundedRadius = 0.5f;
	[Tooltip("What layers the character uses as ground")]
	[SerializeField] private LayerMask groundLayers;

	public void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
		_camera = Camera.main; 
		_rigidbody = GetComponent<Rigidbody>();
	}

	public void Update()
	{
		LookAround();
		GroundedCheck();
		Jump();
	}

	public void FixedUpdate()
	{
		Movement();
	}

	private void Movement()
	{
		movement = InputManager.Instance.GetMovement();
		velocity = ((transform.forward * movement.y) + (transform.right * movement.x)) * movementSpeed;
		transform.position += velocity * Time.deltaTime;
	}
	
	private void LookAround()
	{
		mousePos = InputManager.Instance.GetMousePos() * mouseSensivity;
		transform.Rotate(Vector3.up, mousePos.x * Time.deltaTime);
		cameraPitch -= mousePos.y * Time.deltaTime;
		cameraPitch = ClampAngle(cameraPitch, -90, 90);
		_camera.transform.localRotation = Quaternion.Euler(cameraPitch, 0.0f, 0.0f);
	}

	private void GroundedCheck()
	{
		Vector3 _spherePosition = new Vector3(transform.position.x, transform.position.y - groundedOffset, transform.position.z);
		grounded = Physics.CheckSphere(_spherePosition, groundedRadius, groundLayers, QueryTriggerInteraction.Ignore);
	}

	private void Jump()
	{
		if (grounded)
		{
			if (InputManager.Instance.GetJumpPressed() == true && jumpTimeoutDelta <= 0.0f)
			{
				_rigidbody.AddForce(new Vector3(0 , jumpHeight, 0), ForceMode.Impulse); ;
			}

			if (jumpTimeoutDelta >= 0.0f)
			{
				jumpTimeoutDelta -= Time.deltaTime;
			}
		}
		else
		{
			jumpTimeoutDelta = jumpTimeout;
		}
	}

	/// <summary>
	/// Clamp camera pitch angles between Min and Max value.
	/// </summary>
	/// <param name="lfAngle">Current angle.</param>
	/// <param name="lfMin">Minimal angle.</param>
	/// <param name="lfMax">Maximal angle.</param>
	/// <returns>Clamped float.</returns>
	private float ClampAngle(float lfAngle, float lfMin, float lfMax)
	{
		if (lfAngle < -360f) lfAngle += 360f;
		if (lfAngle > 360f) lfAngle -= 360f;
		return Mathf.Clamp(lfAngle, lfMin, lfMax);
	}

	private void OnDrawGizmosSelected()
	{
		Color _transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
		Color _transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

		if (grounded)
		{
			Gizmos.color = _transparentGreen;
		}
		else
		{
			Gizmos.color = _transparentRed;
		}

		Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y - groundedOffset, transform.position.z), groundedRadius);
	}
}