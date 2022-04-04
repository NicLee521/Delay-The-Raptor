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
    private GameObject spaceToSkip;
    // Start is called before the first frame update
    void Start(){
        video.loopPointReached += EndReached;
        menuContainer = transform.GetChild(0).gameObject;
        spaceToSkip = transform.GetChild(1).gameObject;
        spaceToSkip.SetActive(true);
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
        spaceToSkip.SetActive(false);
    }

    public void OnSkip(InputValue value){
        video.gameObject.SetActive(false);
        menuContainer.SetActive(true);
        spaceToSkip.SetActive(false);

    }
}
