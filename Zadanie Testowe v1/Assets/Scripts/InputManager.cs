using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class InputManager : MonoBehaviour
{
	private float fire;
	private Vector2 cursorPos;
	private Vector2 movement;
	private bool jumpPressed = false;

	public static InputManager instance { get; private set; }

	private void Awake()
	{
		if (instance != null)
		{
			Debug.Log("More than one Input Manager in scene.");
			return;
		}
		instance = this;
	}

	public void Move(InputAction.CallbackContext ctx)
	{
		movement = ctx.ReadValue<Vector2>();
	}

	public void Look(InputAction.CallbackContext ctx)
	{
		cursorPos = ctx.ReadValue<Vector2>();
	}

	public void Fire(InputAction.CallbackContext ctx)
	{
		fire = ctx.ReadValue<float>();
	}

	public void Jump(InputAction.CallbackContext context)
	{
		if (context.performed)
		{
			jumpPressed = true;
		}
		else if (context.canceled)
		{
			jumpPressed = false;
		}
	}

	public Vector2 GetMovement()
	{
		return movement;
	}

	public Vector2 GetMousePos()
	{
		return cursorPos;
	}

	public bool GetJumpPressed()
	{
		return jumpPressed;
	}

	public bool GetFirePressed()
	{
		if(fire == 1)
		{
			return true;
		}
		else 
		{ 
			return false;
		}
	}
}