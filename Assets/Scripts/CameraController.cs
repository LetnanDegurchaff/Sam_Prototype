using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _rotateSpeed = 50;

    private Camera _camera;

    private float _cameraMaxRotation = 90f;
    private float _cameraMinRotation = -90f;
    private float _verticalRotation = 0;
    private Vector2 lookVector = Vector2.zero;

    private void Awake()
    {
        _camera = Camera.main;
    }

    public void RotateGaze(Vector2 lookInputVector)
    {
        lookVector = lookInputVector * _rotateSpeed * Time.deltaTime;

        transform.Rotate(Vector3.up * lookVector.x);

        _verticalRotation -= lookVector.y;
        _verticalRotation = Mathf.Clamp(_verticalRotation, _cameraMinRotation, _cameraMaxRotation);

        _camera.transform.localRotation = Quaternion.Euler(_verticalRotation, 0, 0);
    }
}