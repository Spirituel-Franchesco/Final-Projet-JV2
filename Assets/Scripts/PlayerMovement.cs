using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement _Instance;


    private CharacterController _controller;
    private Transform _playerCamera;

    public float _moveSpeed = 5f; // Vitesse de déplacement
    public float _mouseSensitivity = 70f; // Sensibilité de la souris

    private float _xRotation = 0f;

    private Vector3 _initialPosition;
    private Quaternion _initialRotation;

    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _playerCamera = Camera.main.transform;

        // Cache et verrouille le curseur au centre de l'écran
        Cursor.lockState = CursorLockMode.Locked;

        // Réinitialise la rotation de la caméra pour regarder vers l'avant
        _xRotation = 0f;
        _playerCamera.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
        transform.rotation = Quaternion.Euler(0f, 0f, 0f); // Réinitialise la rotation du joueur

        //ResourceManager._Instance.AddResources(reward);
        //ResourceManager._Instance.AddResources(reward);

        _initialPosition = transform.position;
        _initialRotation = transform.rotation;

    }

    void Update()
    {
        // Mouvement du joueur
        float x = Input.GetAxis("Horizontal") * _moveSpeed * Time.deltaTime;
        float z = Input.GetAxis("Vertical") * _moveSpeed * Time.deltaTime;
        Vector3 move = transform.right * x + transform.forward * z;
        _controller.Move(move);

        // Rotation de la caméra avec la souris
        float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity * Time.deltaTime;

        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f); // Limite la rotation verticale

        _playerCamera.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    public void ResetPlayer()
    {
        transform.position = _initialPosition;
        transform.rotation = _initialRotation;
        // Remettre la vie au max :
        HeroHealth._Instance.ResetHealth();
    }
}