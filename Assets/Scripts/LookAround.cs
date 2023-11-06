using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAround : MonoBehaviour
{

    public float mouseSensitivity = 100f;

    private float xRot = 0f;
    private float yRot = 0f;

    public Transform playerBody;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime * 4;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime * 4;

        xRot -= mouseY;
        xRot = Mathf.Clamp(xRot, -75f, 75f);

        yRot += mouseX;
        yRot = Mathf.Clamp(yRot, -20f, 20f);

        transform.localRotation = Quaternion.Euler(xRot, yRot, 0);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
