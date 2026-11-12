using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
	public static PlayerMovement instance;
	[SerializeField] private float acceleration, terminalVelocity, friction;
	private float moveSpeed = 0;
	private Rigidbody2D rigidBody;
	private PlayerInput input;
	private InputAction moveAction;
	private Vector2 moveDirection;

	void Awake()
	{
		rigidBody = GetComponent<Rigidbody2D>();
		input = new PlayerInput();
		moveAction = input.Player.Move;
		if (PlayerMovement.instance == null) PlayerMovement.instance = this;
	}

	void OnEnable() { input.Player.Enable(); }

	void OnDisable() { input.Player.Disable(); }

	protected void Move()
	{
		if (moveDirection.magnitude > 0)
		{
			if (moveSpeed < terminalVelocity) moveSpeed += acceleration;
			rigidBody.linearVelocity = moveDirection * moveSpeed;
		} else {
			if (moveSpeed >= 0) moveSpeed -= friction;
			rigidBody.linearVelocity = rigidBody.linearVelocity.normalized * moveSpeed;
		}

	}
	void Update()
	{
		moveDirection = moveAction.ReadValue<Vector2>().normalized;
	}

	void FixedUpdate()
	{
		Move();
	}
}
