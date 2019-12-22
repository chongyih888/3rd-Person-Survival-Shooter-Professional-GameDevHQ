using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //get handle to character controller
    private CharacterController _controller;

    [Header("Controller Settings")]
    [SerializeField]
    private float _speed = 6.0f;

    [SerializeField]
    private float _jumpHeight = 8.0f;

    [SerializeField]
    private float _gravity = 20.0f;

    private Vector3 _velocity;

    private Vector3 direction;

    private Camera _mainCamera;

    [Header("Camera Settings")]
    [SerializeField]
    private float _cameraSensitivity = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();

        if(_controller == null)
        {
            Debug.Log("Character controller is NULL");
        }

        _mainCamera = Camera.main;

        if (_mainCamera == null)
        {
            Debug.LogError("Main Camera is NULL");
        }

        //lock cursor and hide it
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        CameraController();

        //if escapekey
        //unlock cursor
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }

         }

    void CalculateMovement()
    {
        //wsad keys for movement
        //input system(horizontal,vertical)
        //direction = vector to move 
        //velocity = direction * speed
        //if jump
        //velocity = new velocity with added y
        if (_controller.isGrounded == true)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            direction = new Vector3(horizontalInput, verticalInput, 0);
            _velocity = direction * _speed;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _velocity.y = _jumpHeight;
            }
        }
       

            _velocity.y -= _gravity * Time.deltaTime;
        
           _velocity = transform.TransformDirection(_velocity);

        //controller.move(wasd * Time.delta);
        _controller.Move(_velocity * Time.deltaTime);

    }

    void CameraController()
    {
        //x mouse
        //y mouse
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        //apply mouseX to the player rotation y to allow us to move left and right
        //transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y + mouseX, transform.localEulerAngles.z);

        Vector3 currentRotation = transform.localEulerAngles;
        currentRotation.y += mouseX * _cameraSensitivity;
        transform.localRotation = Quaternion.AngleAxis(currentRotation.y, Vector3.up);

        //apply mouseY to the camera X value and clamp between 0 and 15
        Vector3 currentCameraRotation = _mainCamera.gameObject.transform.localEulerAngles;
        currentCameraRotation.x -= mouseY * _cameraSensitivity;
        currentCameraRotation.x = Mathf.Clamp(currentCameraRotation.x, 0, 26);

        // _mainCamera.gameObject.transform.localEulerAngles = currentCameraRotation;
        _mainCamera.gameObject.transform.localRotation = Quaternion.AngleAxis(currentCameraRotation.x, Vector3.right);

    }
}
