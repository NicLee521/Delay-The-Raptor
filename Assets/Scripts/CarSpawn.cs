using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class CarSpawn : MonoBehaviour
{

    public Transform carSpawnLeft;
    public Transform carSpawnRight;
    public GameObject[] cars;

    private GameObject player;
    private Transform[] carSpawnsLeft;
    private Transform[] carSpawnsRight;
    public bool notFinished = true;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        player.AddComponent<CarMinigame>();
        carSpawnsLeft = carSpawnLeft.GetComponentsInChildren<Transform>();
        carSpawnsRight = carSpawnRight.GetComponentsInChildren<Transform>();
        StartCoroutine(SpawnOnRandomIntervals(3.0f, carSpawnsLeft, Vector3.right ));
        StartCoroutine(SpawnOnRandomIntervals(3.0f, carSpawnsRight, Vector3.left));

    }

    IEnumerator SpawnOnRandomIntervals(float maxTimeInbetween, Transform[] spawns, Vector3 moveDirection){
        while(notFinished){
            float timeInbetween = Random.Range(0.0f,maxTimeInbetween);
            int numOfSpawns = spawns.Length;
            Transform spawn = spawns[Random.Range(0,numOfSpawns)];
            yield return new WaitForSeconds(timeInbetween);
            GameObject car = cars[Random.Range(0,2)];
            car.GetComponent<Car>().moveDirection = moveDirection;
            Instantiate(car, spawn.position, spawn.rotation);
        }
    }

}
