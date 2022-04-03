using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;


public class GraffitiController : MonoBehaviour
{
    public GameObject[] graffitiToClean;
    public float maxTimesBetweenSpawn;
    public bool gameStillGoing = true;
    public float maxTimeAlive;
    public float minTimeAlive;
    private GameObject wall;
    private Material wallRender;
    private BoxCollider wallCol;
    private Bounds wallBounds;
    private int points = 0;
    private float colorValues = 0;
    private GameObject start;
    private GameObject win;
    private GameObject lose;

    private int karma;   
    private float money;
    private float timeLeft;

    
    
    // Start is called before the first frame update
    void Start()
    {
        win = GameObject.Find("Win");
        lose = GameObject.Find("Lose");
        start = GameObject.Find("Start");
        win.SetActive(false);
        lose.SetActive(false);
        Time.timeScale = 0.0f;
        start.SetActive(true);
        wall = GameObject.Find("Wall");
        wallRender = wall.GetComponent<Renderer>().material;
        wallCol = wall.GetComponent<BoxCollider>();
        wallBounds = wallCol.bounds;
        GetInstanceValues();
        StartCoroutine(ConstantInstantiation());

    }

    Vector3 GetRandomPositionWithinBounds(){
        float x = Random.Range(wallBounds.min.x, wallBounds.max.x);
        float y = Random.Range(wallBounds.min.y, wallBounds.max.y);
        float z = wallBounds.min.z - .10f;
        return new Vector3(x,y,z);
    }

    void GetInstanceValues(){
        karma = GloabalStats.Instance.playerKarma;
        money = GloabalStats.Instance.playerMoney;
        timeLeft = GloabalStats.Instance.playerTimeLeft;
    }

    void SetInstanceValues(){
        GloabalStats.Instance.playerKarma = karma;
        GloabalStats.Instance.playerMoney = money;
        GloabalStats.Instance.playerTimeLeft = timeLeft;
    }

    IEnumerator ConstantInstantiation(){
        while(gameStillGoing){
            GameObject objectToSpawn = graffitiToClean[Random.Range(0,graffitiToClean.Length)];
            objectToSpawn = Instantiate(objectToSpawn, GetRandomPositionWithinBounds(), Quaternion.identity);
            objectToSpawn.AddComponent<ObjectToClean>();
            objectToSpawn.SendMessage("Spawned", this.gameObject);
            yield return new WaitForSeconds(Random.Range(0.0f, maxTimesBetweenSpawn));
        }
    }

    public void OnMouseClick(){
        Vector3 mousePos = Mouse.current.position.ReadValue();   
        Ray ray = Camera.main.ScreenPointToRay (mousePos);
        RaycastHit hit;
        if (Physics.Raycast (ray, out hit, 100)) {
            hit.transform.gameObject.SendMessage("Clicked");
        }
    }

    public void SubtractPoints(){
        if(gameStillGoing){
            points -= 10;
            if(colorValues >= .10f){
                ChangeColor(-.10f);
            }else if(colorValues != 0){
                ChangeColor(-1*colorValues);
            }else{
                ChangeColor(0.0f);
            }
        }
    }

    public void AddPoints(){
        points += 10;
        if(colorValues <= .90f){
            ChangeColor(.10f);
        } else {
            ChangeColor(1.0f-colorValues);
        }
    }

    void ChangeColor(float colorChangeValue){
        Color currColor = wallRender.color;
        colorValues += colorChangeValue;
        Color newColor = currColor;
        newColor.a = colorValues;
        wallRender.color = Color.Lerp(currColor,newColor,Time.time);
        if(points >= 100){
            gameStillGoing = false;
            karma += 25;
            SetInstanceValues();
            win.SetActive(true);
        }else if(points <= -50){
            gameStillGoing = false;
            karma -= 25;
            SetInstanceValues();
            lose.SetActive(true);
        }
    }

    public void ChangeScene(){
        SceneManager.LoadScene("Razorville", LoadSceneMode.Single);
    }

    public void OnStart(){
        start.SetActive(false);
        Time.timeScale = 1.0f;
    }
}
