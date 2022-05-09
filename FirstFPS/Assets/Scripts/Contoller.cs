using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Contoller : MonoBehaviour
{
    private float X, Y, Z;
    public float speed = 0.5f;
    public float sensa = 2f;
    private float pitch = 0.0f;
    public float maxLookAngle = 50f;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState=CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        // Motion
        float hVal = Input.GetAxis("Horizontal") * speed;
        float vVal = Input.GetAxis("Vertical") * speed;
        transform.Translate(hVal, 0, vVal);

        float yaw = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensa;
        pitch -=sensa * Input.GetAxis("Mouse Y") * 2;
        pitch = Mathf.Clamp(pitch, -maxLookAngle, maxLookAngle);
        transform.localEulerAngles = new Vector3(pitch, yaw, 0);
        
        
        if (Input.GetKeyUp (KeyCode.Escape)) {
        Cursor.lockState = CursorLockMode.None;
        }
}}
