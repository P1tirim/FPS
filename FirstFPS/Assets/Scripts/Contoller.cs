using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Contoller : MonoBehaviour
{
    
    public float speed = 50f;
    public float sensa = 2f;
    private float pitch = 0.0f;
    public float maxLookAngle = 50f;
    public float maxVelocityChange = 10f;
    private Rigidbody rb;
    private Vector3 targetVelocity;

    // Start is called before the first frame update
    void Start()
    {
        //lock cursor
        Cursor.lockState=CursorLockMode.Locked;
        
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {   
        //Camera rotation
        float yaw = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensa;
        pitch +=sensa * Input.GetAxis("Mouse Y") * 2;
        pitch = Mathf.Clamp(pitch, -maxLookAngle, maxLookAngle);
        transform.localEulerAngles = new Vector3(pitch, yaw, 0);
        
        //Unlock cursor
        if (Input.GetKeyUp (KeyCode.Escape)) {
        Cursor.lockState = CursorLockMode.None;
        }
    }

    //For motion
    void FixedUpdate(){
        // Motion
        targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0 ,Input.GetAxis("Vertical"));
        targetVelocity = transform.TransformDirection(targetVelocity) * (-speed);
        Vector3 velocity = rb.velocity;
        Vector3 velocityChange = (targetVelocity - velocity);
        velocityChange.y = 0;
        
        
        rb.AddForce(velocityChange, ForceMode.VelocityChange);
    }
}
