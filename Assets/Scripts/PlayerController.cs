using UnityEngine;

[RequireComponent(typeof(MoveController))]
[RequireComponent(typeof(CameraController))]
public class PlayerController : MonoBehaviour
{
    private PlayerInput _playerInput;
    private MoveController _moveController;
    private CameraController _cameraController;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _moveController = GetComponent<MoveController>();
        _cameraController = GetComponent<CameraController>();
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    private void FixedUpdate()
    {
        _moveController.Move(_playerInput.Player.Move.ReadValue<Vector2>());
    }

    private void Update()
    {
        _cameraController.RotateGaze(_playerInput.Player.Look.ReadValue<Vector2>());

        if (Input.GetKeyDown(KeyCode.Space))
            _moveController.TryJump();
    }
}