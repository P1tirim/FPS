using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 


//Global class between scripts
public static class Global{
    public static string gameMode;
    public static int numbering = 1;
    public static float random;
}

public class GameManager : MonoBehaviour
{
    public int timeForEasy = 60;
    private int score = 0;
    private float timer = 0.0f;
    private int lostTime = 1;
    public Text scoreText;
    public Text time;
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

    List<GameObject> instanciatedTargets = new List<GameObject>();
    List<GameObject> listSpawnPointStart = new List<GameObject>();
    
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
        //Spawn point in list
        SpawnPointStartINlist();

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
            
            //if press button "easy"
            if(hit.transform.gameObject.name == "Easy" && !animButton.IsPlaying("ButtonDown")){ 
                 
                animButton.Play("ButtonDown");
                 
                //Destrot old target and clear lists
                for (int i = 0; i < listSpawnPointStart.Count; i++)
                {
                    Destroy(instanciatedTargets[i]);
                }
                instanciatedTargets.Clear();
                listSpawnPointStart.Clear();

                //Spawn point for easy mode in list
                SpawnPointEasyINlist();

                //Create new Target and play animation of falling
                for (int i = 0; i < listSpawnPointStart.Count; i++)
                {
                    instanciatedTargets.Add(Instantiate(target, listSpawnPointStart[i].transform) as GameObject);
                    instanciatedTargets[i].GetComponent<Animation>().Play("TargetFall");
                }

                //Change GameMode
                Global.gameMode = "easy";

                //Random for lift target
                Global.numbering = 1;
                Global.random = Random.Range(1, 7);

                //reset score
                score = 0;
                scoreText.text = score + "";

                //Set timer
                lostTime = 1;
                timer = 0.0f;
             }

            //if press button "start location"
            if(hit.transform.gameObject.name == "StartLocation" && !hit.transform.gameObject.GetComponent<Animation>().IsPlaying("ButtonDownStart")){

                hit.transform.gameObject.GetComponent<Animation>().Play("ButtonDownStart");

                //Destroy old targets and clear lists
                for (int i = 0; i < listSpawnPointStart.Count; i++)
                {
                    Destroy(instanciatedTargets[i]);
                }
                instanciatedTargets.Clear();
                listSpawnPointStart.Clear();

                //Spawn point for start in list
                SpawnPointStartINlist();

                //Spawn start target
                StartSpawn();

                //Change game mode
                Global.gameMode = "start";

                //reset score
                score = 0;
                scoreText.text = score + "";

                //remove timer
                time.text = "";
            }

            //if press button "medium"
            if(hit.transform.gameObject.name == "Medium" && !hit.transform.gameObject.GetComponent<Animation>().IsPlaying("ButtonDownMedium")){

                //Play animation press button
                hit.transform.gameObject.GetComponent<Animation>().Play("ButtonDownMedium");

                //Destroy old targets
                for (int i = 0; i < listSpawnPointStart.Count; i++)
                {
                    Destroy(instanciatedTargets[i]);
                }
                instanciatedTargets.Clear();
                listSpawnPointStart.Clear();

                //Spawn point in list
                SpawnPointEasyINlist();

                //Create new Target and play animation of falling
                for (int i = 0; i < listSpawnPointStart.Count; i++)
                {
                    instanciatedTargets.Add(Instantiate(target, listSpawnPointStart[i].transform) as GameObject);
                    instanciatedTargets[i].GetComponent<Animation>().Play("TargetFall");
                }

                //Change gameMode
                Global.gameMode = "medium";

                //Random for lift target
                Global.numbering = 1;
                Global.random = Random.Range(1, 7);

                //reset score
                score = 0;
                scoreText.text = score + "";

                //Set timer
                lostTime = 1;
                timer = 0.0f;
            }
        }
        }

        //timer
        if(Global.gameMode != "start" && lostTime > 0 || Global.gameMode != "nothing" && lostTime > 0){
            timer = timer + Time.deltaTime; 
            lostTime = timeForEasy - (int)(timer);
            if(lostTime % 60 <10){
                time.text = lostTime / 60 + ":0" + lostTime % 60;
            }else{
                time.text = lostTime / 60 + ":" + lostTime % 60;
            }
        //if timer is 0:00 nothing happens
        }else if(Global.gameMode == "easy" || Global.gameMode == "medium"){
                Global.gameMode = "nothing";
            }
            
    }

    // +score
   public void AddScore(){
        score += 10;
        scoreText.text = score + "";
    }

    //Spawn point for start in list
    void SpawnPointStartINlist(){
        listSpawnPointStart.Add(spawnPoint1 as GameObject);
        listSpawnPointStart.Add(spawnPoint2 as GameObject);
        listSpawnPointStart.Add(spawnPoint3 as GameObject);
        listSpawnPointStart.Add(spawnPoint4 as GameObject);
        listSpawnPointStart.Add(spawnPoint5 as GameObject);
        listSpawnPointStart.Add(spawnPoint6 as GameObject);
        listSpawnPointStart.Add(spawnPointReverseForward1 as GameObject);
        listSpawnPointStart.Add(spawnPointReverseForward2 as GameObject);
        listSpawnPointStart.Add(spawnPointReverse as GameObject);
    }

    //Spawn point for easy mode in list
    void SpawnPointEasyINlist(){
        listSpawnPointStart.Add(spawnPointEasy1 as GameObject);
        listSpawnPointStart.Add(spawnPointEasy2 as GameObject);
        listSpawnPointStart.Add(spawnPointEasy3 as GameObject);
        listSpawnPointStart.Add(spawnPointEasy4 as GameObject);
        listSpawnPointStart.Add(spawnPointEasy5 as GameObject);
        listSpawnPointStart.Add(spawnPointEasy6 as GameObject);
    }

    //Spawn start target
    void StartSpawn(){
       for (int i = 0; i < listSpawnPointStart.Count - 3; i++)
            {
                instanciatedTargets.Add(Instantiate(target, listSpawnPointStart[i].transform) as GameObject);
            }
        instanciatedTargets.Add(Instantiate(targetReverseForward, listSpawnPointStart[6].transform) as GameObject);
        instanciatedTargets.Add(Instantiate(targetReverseForward, listSpawnPointStart[7].transform) as GameObject);
        instanciatedTargets.Add(Instantiate(targetReverse, listSpawnPointStart[8].transform) as GameObject);
    }
}
