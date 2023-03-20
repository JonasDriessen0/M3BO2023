using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    [Header("Sway Settings")]
    [SerializeField] private float smooth;
    [SerializeField] private float swayMultiplierX;
    [SerializeField] private float swayMultiplierY;

    private bool isSpacePressed = false; // track if space key is pressed

    private void Update()
    {
        //get mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * swayMultiplierX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * swayMultiplierY;

        // calculate target rotation 
        Quaternion rotationX = Quaternion.AngleAxis(-mouseY, Vector3.right);
        Quaternion rotationY = Quaternion.AngleAxis(mouseX, Vector3.up);

        Quaternion targetRotation = rotationX * rotationY;

        if (isSpacePressed) // if space key is pressed, tilt the gun downwards
        {
            Quaternion rotationZ = Quaternion.AngleAxis(10f, Vector3.forward); // 10 degrees downwards
            targetRotation = targetRotation * rotationZ;
        }

        //rotation
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smooth * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space)) // if space key is pressed down
        {
            isSpacePressed = true;
        }
        else if (Input.GetKeyUp(KeyCode.Space)) // if space key is released
        {
            isSpacePressed = false;
        }
    }
}