using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField] Camera myCamera;
	Rigidbody myRigidBody;

	[Header("Movement Settings")]
	float speed = 10.0f;
	float turnSmoothTime = 0.1f;
	float turnSmoothVelocity;

	[Header("Jump Settings")]
	float jumpForce = 15.0f;
	bool isGrounded = true;

	[Header("Dash Settings")]
	float dashForce = 25.0f;
	float dashDuration = 0.1f;
	float dashTimer = 0.0f;

	float dashCooldown = 1.0f;
	float dashCooldownTimer = 0.0f;

	void Awake()
	{
		myRigidBody = GetComponent<Rigidbody>();
	}
	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		Move();

		if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
		{
			Jump();
		}

		if (Input.GetKeyDown(KeyCode.LeftShift) && dashCooldownTimer <= 0.0f)
		{
			dashCooldownTimer = dashCooldown;
			Dash();
		}
	}
	private void Dash()
	{
		StartCoroutine(DashRoutine());
		StartCoroutine(DashCooldown());
	}
	IEnumerator DashCooldown()
	{
		while (dashCooldownTimer > 0.0f)
		{
			dashCooldownTimer -= Time.deltaTime;
			yield return null;
		}
	}
	IEnumerator DashRoutine()
	{
		dashTimer = Time.time;
		while (Time.time < dashTimer + dashDuration)
		{
			transform.Translate(Vector3.forward * dashForce * Time.deltaTime);
			yield return null;
		}
	}

	private void Jump()
	{
		myRigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
		isGrounded = false;
	}

	private void Move()
	{
		// get user input
		float horizontalInput = Input.GetAxisRaw("Horizontal");
		float verticalInput = Input.GetAxisRaw("Vertical");

		// create a direction vector based on user input and normalize it to go same speed in any direction
		Vector3 direction = new Vector3(horizontalInput, 0, verticalInput).normalized;

		// if the player input is not zero, move the player
		if (direction.magnitude >= 0.1f)
		{
			// calculate the target angle for the player to look at based on user input
			// atan2 returns the angle in radians between the x-axis and the vector
			// multiply by 180 / pi to convert to degrees which is rad2deg 
			float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + myCamera.transform.eulerAngles.y;

			// smooth damp the player's angle to the target angle
			float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

			// rotate the player to look at the target angle
			transform.rotation = Quaternion.Euler(0, angle, 0);

			// calculate the target position for the player to move to based on user input and the angle of the camera
			Vector3 moveDirection = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;

			// move the player
			transform.Translate(speed * moveDirection * Time.deltaTime, Space.World);
		}
	}

	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "Ground")
		{
			isGrounded = true;
		}
	}
}
