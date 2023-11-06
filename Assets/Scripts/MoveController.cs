using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CharacterController))]
public class MoveController : MonoBehaviour
{
    [SerializeField] private float _speed = 2;

    private CharacterController _characterController;

    private Vector3 _moveDirection = Vector3.zero;
    private float _gravityAcceleration = Physics.gravity.y;
    private float _verticalSpeed = 0;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    public void Move(Vector2 direction)
    {
        Vector3 moveForward = transform.forward * direction.y;
        Vector3 moveRight = transform.right * direction.x;

        Vector3 moveVector = moveForward + moveRight;

        if (_characterController.isGrounded)
            _moveDirection = (moveVector.normalized + Vector3.down ) * _speed;
        else
            _moveDirection = _characterController.velocity;

        _characterController.Move
                ((_moveDirection + new Vector3(0, _verticalSpeed, 0) * Time.fixedDeltaTime) * Time.fixedDeltaTime);

        _verticalSpeed = _gravityAcceleration;
    }

    public void TryJump(float jumpSpeed)
    {
        if (_characterController.isGrounded)
        {
            _verticalSpeed = jumpSpeed;
        }
    }
}