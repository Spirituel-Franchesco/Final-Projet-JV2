using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float _moveSpeed = 5f; // Vitesse de d�placement
    public float _mouseSensitivity = 100f; // Sensibilit� de la souris

    private CharacterController _controller;
    private Transform _playerCamera;
    private float _xRotation = 0f;

    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _playerCamera = Camera.main.transform;

        // Cache et verrouille le curseur au centre de l'�cran
        Cursor.lockState = CursorLockMode.Locked;

        // R�initialise la rotation de la cam�ra pour regarder vers l'avant
        _xRotation = 0f;
        _playerCamera.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
        transform.rotation = Quaternion.Euler(0f, 0f, 0f); // R�initialise la rotation du joueur
    }

    void Update()
    {
        // Mouvement du joueur
        float x = Input.GetAxis("Horizontal") * _moveSpeed * Time.deltaTime;
        float z = Input.GetAxis("Vertical") * _moveSpeed * Time.deltaTime;
        Vector3 move = transform.right * x + transform.forward * z;
        _controller.Move(move);

        // Rotation de la cam�ra avec la souris
        float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity * Time.deltaTime;

        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f); // Limite la rotation verticale

        _playerCamera.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
}