using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMove : MonoBehaviour
{
    public float speed = 5;
    private float elapsedTargetFall = 0.0f;
    public float timeForTargetUP = 5;
    private bool targetFall = false;
    private bool enable = true;
    private string lastAnim = ""; 
    GameManager gameManager;

    public Animation anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animation>();
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < -50 || transform.position.x > 50){
            speed = speed * -1;
        }
        transform.Translate(Vector3.right * Time.deltaTime * speed);

        if(anim.IsPlaying("TargetFall")){
            elapsedTargetFall = 0.0f;
            targetFall = true;
            lastAnim = "TargetFall";
            enable = false;
        }

        if(targetFall && elapsedTargetFall >= timeForTargetUP){
            anim.Play("TargetUP");
            targetFall = false;
            lastAnim = "TargetUP";
            enable = false;
        }

        if(!anim.IsPlaying("TargetFall") && !anim.IsPlaying("TargetUP") && lastAnim == "TargetUP"){
            enable = true;
        }

        elapsedTargetFall += Time.deltaTime;
    }

    void OnTriggerEnter (Collider other)
    {
        if(enable){
            anim.Play("TargetFall");
            gameManager.AddScore();
        } 
    }
    
    
}
