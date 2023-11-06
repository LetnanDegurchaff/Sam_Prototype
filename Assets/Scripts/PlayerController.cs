using UnityEngine;

[RequireComponent(typeof(MoveController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _rotateSpeed = 10;
    [SerializeField] private float _jumpSpeed = 650;

    private PlayerInput _playerInput;
    private MoveController _moveController;
    private Camera _camera;

    private float _cameraMaxRotation = 90f;
    private float _cameraMinRotation = -90f;
    private float _verticalRotation = 0;
    private Vector2 _lookInputVector = Vector2.zero;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _moveController = GetComponent<MoveController>();
        _camera = Camera.main;
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
        RotateLook();

        if (Input.GetKeyDown(KeyCode.Space))
            _moveController.TryJump(_jumpSpeed);
    }

    private void RotateLook()
    {
        _lookInputVector = _playerInput.Player.Look.ReadValue<Vector2>() * _rotateSpeed * Time.deltaTime;

        transform.Rotate(Vector3.up * _lookInputVector.x);

        _verticalRotation -= _lookInputVector.y;
        _verticalRotation = Mathf.Clamp(_verticalRotation, _cameraMinRotation, _cameraMaxRotation);

        _camera.transform.localRotation = Quaternion.Euler(_verticalRotation, 0, 0);
    }
}