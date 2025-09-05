using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement _Instance;

    [SerializeField] private AudioSource _footstepAudio;
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _mouseSensitivity = 100f;
    [SerializeField] private float _smoothing = 5f;  // nouveau : interpolation (lerp)

    private CharacterController _controller;
    private Transform _playerCamera;
    private Vector3 _initialPosition;
    private Quaternion _initialRotation;
    private float _xRotation = 0f;
    private float _currentYaw = 0f;  // nouveau : pour lerp horizontal
    private float _targetYaw = 0f;

    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _playerCamera = Camera.main.transform;

        Cursor.lockState = CursorLockMode.Locked;
        _xRotation = 0f;
        _playerCamera.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);

        _initialPosition = transform.position;
        _initialRotation = transform.rotation;
    }

    void Update()
    {
        Vector3 velocity = _controller.velocity;
        bool isMoving = velocity.magnitude > 0.1f;

        if (isMoving && !_footstepAudio.isPlaying)
            _footstepAudio.Play();
        else if (!isMoving && _footstepAudio.isPlaying)
            _footstepAudio.Stop();

        float x = Input.GetAxis("Horizontal") * _moveSpeed * Time.deltaTime;
        float z = Input.GetAxis("Vertical") * _moveSpeed * Time.deltaTime;
        Vector3 move = transform.right * x + transform.forward * z;
        _controller.Move(move);

        // Bloque rotation si Alt gauche enfoncé
        if (Input.GetKey(KeyCode.LeftAlt))
            return;

        float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity * Time.deltaTime;

        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

        _playerCamera.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);

        _targetYaw += mouseX;
        _currentYaw = Mathf.Lerp(_currentYaw, _targetYaw, Time.deltaTime * _smoothing);

        transform.rotation = Quaternion.Euler(0f, _currentYaw, 0f);
    }

    public void ResetPlayer()
    {
        transform.position = _initialPosition;
        transform.rotation = _initialRotation;
        HeroHealth._Instance.ResetHealth();
    }
}
