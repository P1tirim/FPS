using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 



public static class Global{
   
    public static string gameMode;
    public static int numbering = 1;
    public static float random;
}

public class GameManager : MonoBehaviour
{
    private int score = 0;
    public Text scoreText;
    public GameObject spawnPoint1;
    public GameObject spawnPoint2;
    public GameObject spawnPoint3;
    public GameObject spawnPoint4;
    public GameObject spawnPoint5;
    public GameObject spawnPoint6;
    public GameObject spawnPointReverseForward1;
    public GameObject spawnPointReverseForward2;
    public GameObject spawnPointReverse;
    public GameObject target;
    public GameObject targetReverse;
    public GameObject targetReverseForward;
    public GameObject easyButton;
    public Camera camera;
    public GameObject spawnPointEasy1;
    public GameObject spawnPointEasy2;
    public GameObject spawnPointEasy3;
    public GameObject spawnPointEasy4;
    public GameObject spawnPointEasy5;
    public GameObject spawnPointEasy6;

    private bool enableEasy = true;
    GameObject target1;
    GameObject target2;
    GameObject target3;
    GameObject target4;
    GameObject target5;
    GameObject target6;
    GameObject target7;
    GameObject target8;
    GameObject target9;

    Animation animButton;
    Animation animTarget;

    // Start is called before the first frame update
    void Start()
    {
        //Spawn start target
        StartSpawn();

        animButton = easyButton.GetComponent<Animation>();
        animTarget = target.GetComponent<Animation>();
        Global.gameMode = "start";
    }

    // Update is called once per frame
    void Update()
    {
        //Start easy gamemode when press button
        if(Input.GetKeyUp(KeyCode.E)){
            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit)) {
            
             if(hit.transform.gameObject.name == "Easy" && enableEasy){ 
                 animButton.Play("ButtonDown");
                 enableEasy = false;

                 //Destrot old target
                 Destroy(target1);
                 Destroy(target2);
                 Destroy(target3);
                 Destroy(target4);
                 Destroy(target5);
                 Destroy(target6);
                 Destroy(target7);
                 Destroy(target8);
                 Destroy(target9);

                //Create new Target
                target1 =  Instantiate(target, spawnPointEasy1.transform);
                target2 = Instantiate(target, spawnPointEasy2.transform);
                target3 =  Instantiate(target, spawnPointEasy3.transform);
                target4 = Instantiate(target, spawnPointEasy4.transform);
                target5 = Instantiate(target, spawnPointEasy5.transform);
                target6 = Instantiate(target, spawnPointEasy6.transform);

                //Falling target
                Global.gameMode = "easy";
                target1.GetComponent<Animation>().Play("TargetFall");
                target2.GetComponent<Animation>().Play("TargetFall");
                target3.GetComponent<Animation>().Play("TargetFall");
                target4.GetComponent<Animation>().Play("TargetFall");
                target5.GetComponent<Animation>().Play("TargetFall");
                target6.GetComponent<Animation>().Play("TargetFall");

                //Random for lift target
                Global.random = Random.Range(1, 7);
             }
        }
        }
    }

    // +score
   public void AddScore(){
        score += 10;
        scoreText.text = score + "";
    }

    //Spawn start target
    void StartSpawn(){
       target1 =  Instantiate(target, spawnPoint1.transform);
       target2 = Instantiate(target, spawnPoint2.transform);
       target3 =  Instantiate(target, spawnPoint3.transform);
       target4 = Instantiate(target, spawnPoint4.transform);
       target5 = Instantiate(target, spawnPoint5.transform);
       target6 = Instantiate(target, spawnPoint6.transform);
       target7 = Instantiate(targetReverseForward, spawnPointReverseForward1.transform);
       target8 = Instantiate(targetReverseForward, spawnPointReverseForward2.transform);
       target9 = Instantiate(targetReverse, spawnPointReverse.transform);
    }
}
