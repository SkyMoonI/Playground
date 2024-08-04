using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestNewInputSystem : MonoBehaviour
{
	Rigidbody rb;
	Vector2 moveVector;
	void Awake()
	{
		rb = GetComponent<Rigidbody>();
	}
	private void Update()
	{
		transform.Translate(new Vector3(moveVector.x, 0, moveVector.y) * 5f * Time.deltaTime);
	}
	public void Jump(InputAction.CallbackContext context)
	{
		Debug.Log(context);
		if (context.performed)
		{
			Debug.Log("Jump");
			rb.AddForce(Vector3.up * 10f, ForceMode.Impulse);
		}

	}

	public void MoveInput(InputAction.CallbackContext context)
	{
		moveVector = context.ReadValue<Vector2>();
	}
}
