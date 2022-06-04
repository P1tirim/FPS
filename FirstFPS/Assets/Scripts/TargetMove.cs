using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TargetMove : MonoBehaviour
{
    public float speed = 10;
    private float elapsedTargetFall = 0.0f;
    private float elapsedTargetUP = 0.0f;
    public float timeForTargetUP = 5;
    private bool targetFall = false;
    private bool enable = true;
    private string lastAnim = "";
    private int number = 0;
    GameManager gameManager;
    public GameObject hole;

    private Animation anim;
    private Collider holeCollider;

    

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animation>();
        gameManager = GameObject.FindObjectOfType<GameManager>();
        holeCollider = hole.GetComponent<Collider>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //Motion target if gameMode == start
        if(Global.gameMode == "start"){
            TargetAnimationStart();
            TargetMotion();
        } 

        //Numbering to target and increase speed
        if(Global.gameMode != "start" && Global.gameMode != "nothing" && number == 0){

            number = Global.numbering;
            Global.numbering ++;
            anim["TargetFall"].speed = 2.5f;
            anim["TargetUP"].speed = 2.5f;
        }
        
        //Blocking target, if animation of the fall is playing
        if(anim.IsPlaying("TargetFall")){

            elapsedTargetFall = 0.0f;
            targetFall = true;
            lastAnim = "TargetFall";
            enable = false;
        }

        //Animation of lifting random target
        if(targetFall && Global.gameMode != "start" && number == Global.random && !anim.IsPlaying("TargetFall") && Global.gameMode != "nothing"){
            anim.Play("TargetUP");
            lastAnim = "TargetUP";
            targetFall = false;
            enable = false;
            
        }

        //Unblocking target for +score and play animation
        if(!anim.IsPlaying("TargetFall") && !anim.IsPlaying("TargetUP") && lastAnim == "TargetUP" && number == Global.random){
            enable = true;
            elapsedTargetUP += Time.deltaTime;
            if(Global.gameMode == "medium" || Global.gameMode == "hard") TargetFallForMediumandHardMode();
        }
    }

    //When the bullet hole hits the trigger, play animation of the fall, +score and disabled collider of hole
    void OnTriggerEnter (Collider other)
    {
        if(enable){
            anim.Play("TargetFall");
            gameManager.AddScore();
            holeCollider = other.GetComponent<Collider>();
            holeCollider.enabled = false;
            Global.random = Random.Range(1, 7);
        } 
    }

    void TargetMotion (){
        //Change direction
        if(transform.position.x < -100 || transform.position.x > 100){
            speed = speed * -1;
        }
        if(this.tag == "Forward" && transform.position.z < 20 || transform.position.z > 100 && this.tag == "Forward"){
            speed = speed * -1;
        }

        //Motion target
        if(this.tag == "Forward"){
            transform.Translate(0, 0, 1 * Time.deltaTime * speed, Space.World);
        }else transform.Translate(Vector3.right * Time.deltaTime * speed);
    }

    //For gameMode == start
    void TargetAnimationStart(){

        //Animation of lifting target elapsed time
        if(targetFall && elapsedTargetFall >= timeForTargetUP){

            anim.Play("TargetUP");
            lastAnim = "TargetUP";
        
            targetFall = false;
            
            enable = false;
        }

        //Unblocking target for +score and play animation
        if(!anim.IsPlaying("TargetFall") && !anim.IsPlaying("TargetUP") && lastAnim == "TargetUP"){
            enable = true;
        }

        elapsedTargetFall += Time.deltaTime;
    }

    //Falling targets if elapsedTargetUP > 0.8 in medium gameMode
    void TargetFallForMediumandHardMode(){

        if(enable && elapsedTargetUP >= 0.8 && Global.gameMode == "medium"){
            
            anim.Play("TargetFall");
            Global.random = Random.Range(1, 7);
            elapsedTargetUP = 0.0f;
        }
        
        if(enable && elapsedTargetUP >= 0.5 && Global.gameMode == "hard"){
            
            anim.Play("TargetFall");
            Global.random = Random.Range(1, 10);
            elapsedTargetUP = 0.0f;
        }
    }
}
