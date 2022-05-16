using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMove : MonoBehaviour
{
    public float speed = 10;
    private float elapsedTargetFall = 0.0f;
    public float timeForTargetUP = 5;
    private bool targetFall = false;
    private bool enable = true;
    private string lastAnim = "";
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
        
        //Blocking target, if animation of the fall is playing
        if(anim.IsPlaying("TargetFall") || anim.IsPlaying("TargetFallReverse")){
            elapsedTargetFall = 0.0f;
            targetFall = true;
            lastAnim = "TargetFall";
            enable = false;
        }

        //Animation of lifting target elapsed time
        if(targetFall && elapsedTargetFall >= timeForTargetUP){

            if(this.name == "Military target Reverse"){
                anim.Play("TargetUpReverse");
                lastAnim = "TargetUpReverse";
            } else{
                anim.Play("TargetUP");
                lastAnim = "TargetUP";
            } 
            
            targetFall = false;
            
            enable = false;
        }

        //Unblocking target for +score and play animation
        if(!anim.IsPlaying("TargetFall") && !anim.IsPlaying("TargetUP") && lastAnim == "TargetUP" || 
                    !anim.IsPlaying("TargetFallReverse") && !anim.IsPlaying("TargetUpReverse") && lastAnim == "TargetUpReverse"){
            enable = true;
        }

        elapsedTargetFall += Time.deltaTime;
    }

    //When the bullet hole hits the trigger, play animation of the fall, +score and disabled collider of hole
    void OnTriggerEnter (Collider other)
    {
        if(enable){

            if(this.name == "Military target Reverse"){
                anim.Play("TargetFallReverse");
            } else anim.Play("TargetFall");
            
            gameManager.AddScore();
            holeCollider = other.GetComponent<Collider>();
            holeCollider.enabled = false;
        } 
    }


}
