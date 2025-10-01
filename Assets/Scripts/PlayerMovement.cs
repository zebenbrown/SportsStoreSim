using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private Collider capsuleCollider;
    private PlayerMovementInput input;
    private Vector2 movement;
    private Vector2 look;
    public float mouseSensitivity;
    private float mouseX;
    private float mouseY;
    public Transform cameraTransform;
    private float verticalLook;

    private float speed = 50f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        input = new PlayerMovementInput();
        rb = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<Collider>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnEnable()
    {
        input.Player.Enable();
    }

    private void OnDisable()
    {
        input.Player.Disable();
    }

    private void Update()
    {
        movement = input.Player.Move.ReadValue<Vector2>();
        look = input.Player.Look.ReadValue<Vector2>();

        mouseX = look.x * (mouseSensitivity);
        mouseY = look.y * (mouseSensitivity);
        
        transform.Rotate(Vector3.up * mouseX);
        
        verticalLook -= mouseY;
        verticalLook = Mathf.Clamp(verticalLook, -90f, 90f);
        
        cameraTransform.localRotation = Quaternion.Euler(verticalLook, 0, 0);
        
        Vector3 move = transform.right * movement.x + transform.forward * movement.y;
        rb.MovePosition(rb.position + move * speed * Time.deltaTime);
    }
}
