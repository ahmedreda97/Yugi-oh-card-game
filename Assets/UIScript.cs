using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIScript : MonoBehaviour {
    public Button restart;
    public Button resume;
    public Button exit;
    public Button help;
    public GameObject panel;
    public GameObject HelpPanel;
    public Button pause;
    public Button OK;


    // Use this for initialization
    void Start()
    {

        panel.SetActive(false);
        HelpPanel.SetActive(false);
        restart.onClick.AddListener(RestartGame);
        resume.onClick.AddListener(Resume);
        exit.onClick.AddListener(Exit);
        help.onClick.AddListener(Help);
        pause.onClick.AddListener(Pause);
        OK.onClick.AddListener(OKClick);
    }

    public void RestartGame() {
        Application.LoadLevel(1);
        //SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
    public void Resume()
    {
        panel.SetActive(false);
    }
    public void Exit()
    {
        Application.Quit();
        
    }
    public void Help()
    {
        if (HelpPanel.activeSelf == true)
        { HelpPanel.SetActive(false); }
        else { HelpPanel.SetActive(true); }
       
    }
    public void Pause()
    {
        panel.SetActive(true);
    }
    public void OKClick()
    {
        HelpPanel.SetActive(false);
        panel.SetActive(false);
    }

}
