using UnityEngine;
using UnityEngine.Windows;

public class StarterAssetsInputs : MonoBehaviour
{
	public StarterAssets inputActions;

	private void Awake() {
		inputActions = new StarterAssets();
		inputActions.Player.Enable();
	}

	[Header("Character Input Values")]
	public Vector2 move;
	public Vector2 look;
	public bool jump;
	public bool interact;
	public bool interact_alt;
	public bool select;
	public bool sprint;
	public bool esc;

	[Header("Movement Settings")]
	public bool analogMovement;

	[Header("Mouse Cursor Settings")]
	public bool cursorLocked = true;
	public bool cursorInputForLook = true;
    private bool cursorFlag = true;


    private void OnApplicationFocus(bool hasFocus)
	{
		SetCursorState(cursorLocked);
	}

	private void SetCursorState(bool newState)
	{
		Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
	}
	private void Start() {
		inputActions.Player.Select.performed += Select_performed;
    }

	private void Select_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
		cursorFlag = !cursorFlag;
		cursorInputForLook = !cursorInputForLook;
		cursorLocked = !cursorLocked;
        SetCursorState(cursorFlag);
    }
	public void ActivateUISelect() {
        cursorFlag = false;
        cursorInputForLook = false;
        cursorLocked = false;
        SetCursorState(cursorFlag);
    }
    public void DeActivateUISelect() {
        cursorFlag = true;
        cursorInputForLook = true;
        cursorLocked = true;
        SetCursorState(cursorFlag);
    }

    private void Update() {
		move = inputActions.Player.Move.ReadValue<Vector2>();
		look = inputActions.Player.Look.ReadValue<Vector2>();
		jump = inputActions.Player.Jump.WasPressedThisFrame();
		interact = inputActions.Player.Interact.WasPressedThisFrame();
		interact_alt = inputActions.Player.Interact_Alt.WasPressedThisFrame();
		esc = inputActions.Player.Pause.WasPressedThisFrame();
		sprint = inputActions.Player.Sprint.IsPressed();
	}	
}
