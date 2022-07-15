using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Camera mainCamera;
    private Rigidbody rb;

    [SerializeField] float forceMagnitude;
    [SerializeField] float maxVelocity;
    [SerializeField] float rotationSpeed;

    private Vector3 movementDirection;
    void Start()
    {
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        InputProcess();

        KeepPlayerOnScreen();

        RotateToFace();
    }

    private void RotateToFace()
    {
        if (rb.velocity == Vector3.zero) return;
        Quaternion targetRotation = Quaternion.LookRotation(rb.velocity, Vector3.back);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void KeepPlayerOnScreen()
    {
        Vector3 newPosition = transform.position;
        Vector3 viewPortPosition = mainCamera.WorldToViewportPoint(transform.position);

        if (viewPortPosition.x > 1) newPosition.x = -newPosition.x + 0.1f;
        else if (viewPortPosition.x < 0) newPosition.x = -newPosition.x - 0.1f;
        else if (viewPortPosition.y > 1) newPosition.y = -newPosition.y + 0.1f;
        else if (viewPortPosition.y < 0) newPosition.y = -newPosition.y - 0.1f;
        else return;

        transform.position = newPosition;
    }

    private void InputProcess()
    {
        if (Touchscreen.current.primaryTouch.press.isPressed)
        {
            Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(touchPosition);

            movementDirection = transform.position - worldPosition;
            movementDirection.z = 0f;
            movementDirection.Normalize();
        }
        else
        {
            movementDirection = Vector3.zero;
        }
    }

    private void FixedUpdate()
    {
        PhsicalMovementCalculations();
    }

    private void PhsicalMovementCalculations()
    {
        if (movementDirection == Vector3.zero) return;

        rb.AddForce(movementDirection * forceMagnitude * Time.deltaTime, ForceMode.Force);

        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVelocity);
    }
}
