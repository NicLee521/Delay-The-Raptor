using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.InputSystem;

public class MainMenu : MonoBehaviour
{
    public VideoPlayer video;
    private GameObject menuContainer;
    // Start is called before the first frame update
    void Start(){
        video.loopPointReached += EndReached;
        menuContainer = transform.GetChild(0).gameObject;
        menuContainer.SetActive(false);
    }

    public void playButton()
    {
        SceneManager.LoadScene("Razorville");

    }

    public void quitButton()
    {
        Application.Quit();
    }

    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        video.gameObject.SetActive(false);
        menuContainer.SetActive(true);
    }

    public void OnSkip(InputValue value){
        video.gameObject.SetActive(false);
        menuContainer.SetActive(true);
    }
}
