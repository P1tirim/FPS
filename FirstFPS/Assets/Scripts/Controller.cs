using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class Controller : MonoBehaviour
{
    
    public float speed = 50f;
    public float sensa = 2f;
    private float pitch = 0.0f;
    public float maxLookAngle = 50f;
    public float maxVelocityChange = 10f;
    public float rateOfFire = 0.6f;
    public float elapsedFire = 0.0f;
    private float yaw = 0.0f;
    public int cartridges = 7;
    private int countCartridges;
    private int score = 0;
    

    public GameObject weapon;
    public GameObject hole;
    public GameObject target;
    public Camera camera;
    public Text currentCartridgesText;
    public Text scoreText;

    private Rigidbody rb;
    private Vector3 targetVelocity;
    private Animation anim;
    private AudioSource audio;
    private Animation animTarget;
    // Start is called before the first frame update
    void Start()
    {
        //lock cursor
        Cursor.lockState=CursorLockMode.Locked;

        rb = GetComponent<Rigidbody>();
        anim = weapon.GetComponent<Animation>();
        audio = weapon.GetComponent<AudioSource>();
        animTarget = target.GetComponent<Animation>();

        countCartridges = cartridges;
    }

    // Update is called once per frame
    void Update()
    {   
        
        //Camera rotation
        yaw = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensa;
        pitch -=sensa * Input.GetAxis("Mouse Y") * 2;
        pitch = Mathf.Clamp(pitch, -maxLookAngle, maxLookAngle);
        transform.localEulerAngles = new Vector3(0, yaw, 0);
        camera.transform.localEulerAngles = new Vector3(pitch, 0, 0);
        
        //Unlock cursor
        if (Input.GetKeyUp (KeyCode.Escape)) {
            Cursor.lockState = CursorLockMode.None;
        }

        

        //Shoot with delay
        if(Input.GetMouseButton(0) && elapsedFire >= rateOfFire && countCartridges != 0 && !anim.IsPlaying("PistolReloaded")){
                
            //Create Raycast
            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit)) {
                //Rotation hole of bullet, then create
                Quaternion quaternion = new Quaternion();
                if(hit.collider.tag == "Forward") quaternion.Set(1, 0, 0, 1);
                if(hit.collider.tag == "Left") quaternion.Set(0, 0, 1, 1);
                if(hit.collider.tag == "Right") quaternion.Set(0, 0, -1, 1);
                if(hit.collider.tag == "Up") quaternion.Set(90, 0, 0, 1);
                if(hit.collider.tag == "Target") quaternion.Set(1, 0, 0, 1);
                
                
                GameObject newHole = Instantiate(hole, hit.point, quaternion);
                
                //Marke the hole child
                newHole.transform.parent = hit.collider.transform;
                

                if(hit.collider.tag == "Target"){
                    score += 10;
                    scoreText.text = score + "";
            }
            }
            //Play shoot animation
            anim.Play("PistolShoot");
            audio.Play();
            elapsedFire = 0.0f;
            countCartridges -= 1;
            currentCartridgesText.text = countCartridges + "/" + cartridges;   

                  
            }else if(Input.GetMouseButton(0) && countCartridges == 0 && elapsedFire >= rateOfFire){
                countCartridges = cartridges;
                currentCartridgesText.text = countCartridges + "/" + cartridges;
                anim.Play("PistolReloaded");
                elapsedFire = 0.2f;
            }

            if(Input.GetKeyDown(KeyCode.R)){
                countCartridges = cartridges;
                currentCartridgesText.text = countCartridges + "/" + cartridges;
                anim.Play("PistolReloaded");
            }

            
            
            elapsedFire += Time.deltaTime;
           
    }

    //For motion
    void FixedUpdate(){
        // Motion
        targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0 ,Input.GetAxis("Vertical"));
        targetVelocity = transform.TransformDirection(targetVelocity) * (speed);
        Vector3 velocity = rb.velocity;
        Vector3 velocityChange = (targetVelocity - velocity);
        velocityChange.y = 0;
        
        
        rb.AddForce(velocityChange, ForceMode.VelocityChange);
    }


}

