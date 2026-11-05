using UnityEngine;
using UnityEngine.InputSystem;

public class AffixTo : MonoBehaviour
{
    [SerializeField] private GameObject other;
    [SerializeField] private Vector2 offset;
    [SerializeField] private bool inheritPlayerRotation = false;
    private Vector2 currentRotation;
    private PlayerInput input;
    private InputAction moveAction;

    void Awake()
	{
		input = new PlayerInput();
		moveAction = input.Player.Move;
	}

	void OnEnable() { input.Player.Enable(); }

	void OnDisable() { input.Player.Disable(); }

    void Start()
    {
        currentRotation = Vector2.one;
    }

    void Update()
    {
        if (inheritPlayerRotation) currentRotation = calculateOffsetFromPlayerRotation();
        transform.position = other.transform.position + (Vector3) rotateVector(offset, currentRotation);
    }

    private Vector2 calculateOffsetFromPlayerRotation()
    {
        Vector2 moveDirection = moveAction.ReadValue<Vector2>().normalized;
        if (moveDirection == Vector2.zero) return currentRotation;
        return moveDirection;
    }

	private Vector2 rotateVector(Vector2 vector, Vector2 direction)
	{
		return Quaternion.FromToRotation(vector.normalized, direction.normalized) * vector;
	}
}
