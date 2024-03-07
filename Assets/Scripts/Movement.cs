using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
[RequireComponent(typeof(CharacterController))]

public class Movement : MonoBehaviour
{
    public static int key;
    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 40.0f;
    public float alpha;
    public float lookXLimit = 45.0f;
    public float sense;
    public bool Jump;
    float mouseX, mouseY;
    public static float mouseSens;
    float xRotation, yRotation;
    public Camera playerCamera;
    public TextMeshProUGUI k, k1;
    [Range(-1f, 1f)]
    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;

    [HideInInspector]
    public bool canMove = true;

    void Start()
    {
        key = 0;
        characterController = GetComponent<CharacterController>();

        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Update()
    {
        k.text = key + " / 10";
        k1.text = key + " / 10";
        mouseSens = sense;
        // We are grounded, so recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        // Press Left Shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded && Jump)
        {
            moveDirection.y = jumpSpeed;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }
        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

        // Player and Camera rotation
        if (canMove)
        {
            mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
            mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            yRotation -= mouseX;
            float targetAxis = 0;
            if (mouseX > 1) { targetAxis = 1; }
            else if (mouseX < -1) { targetAxis = -1; }
            else { targetAxis = mouseX; }
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(/*xRotation*/0, -yRotation, targetAxis * -5f), alpha);
            playerCamera.transform.localRotation = Quaternion.Lerp(playerCamera.transform.localRotation, Quaternion.Euler(xRotation, playerCamera.transform.localRotation.y, playerCamera.transform.localRotation.z), alpha);
        }
    }
}