using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

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

    // Start is called before the first frame update
    void Start()
    {
        StartSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // +score
   public void AddScore(){
        score += 10;
        scoreText.text = score + "";
    }

    void StartSpawn(){
        Instantiate(target, spawnPoint1.transform);
        Instantiate(target, spawnPoint2.transform);
        Instantiate(target, spawnPoint3.transform);
        Instantiate(target, spawnPoint4.transform);
        Instantiate(target, spawnPoint5.transform);
        Instantiate(target, spawnPoint6.transform);
        Instantiate(targetReverseForward, spawnPointReverseForward1.transform);
        Instantiate(targetReverseForward, spawnPointReverseForward2.transform);
        Instantiate(targetReverse, spawnPointReverse.transform);
    }
}
