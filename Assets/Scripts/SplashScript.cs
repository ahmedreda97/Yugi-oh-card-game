using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SplashScript : MonoBehaviour {


    public Image part1, part2;
    public Image origin1, origin2,origin3;
    public GameObject panel, soundpanel;
    public Button play, sound, exit,Back;
    public Slider audioslider;
    Button btn, btn2, btn3;
    bool activefalse = false;
    Vector3 startposition, endpositon, startposition2, endpositon2, startposition3, endpositon3, startposition4, endpositon4;
    float Lerp = 0, timer = .75f, Lerp2 = 0;
    public Toggle mute;
    bool Mute=true;
    // Use this for initialization
    void Start()
    {
        soundpanel.SetActive(false);
        startposition = part1.transform.position;
        endpositon = origin1.transform.position;
        startposition2 = part2.transform.position;
        endpositon2 = origin2.transform.position;


        origin3.gameObject.SetActive(false);
        origin1.gameObject.SetActive(false);
        origin2.gameObject.SetActive(false);
        play.onClick.AddListener(PlayClick);
        sound.onClick.AddListener(soundClick);
        Back.onClick.AddListener(BackClick);
        exit.onClick.AddListener(exitClick);

        mute.onValueChanged.AddListener((Mute) => {
            if (!mute.isOn)
            {
                muteClick(Mute);
            }
        });

        //  panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        AudioListener.volume = audioslider.value / 10.0f;
        timer -= Time.deltaTime;
        if (timer <= 0 && startposition != endpositon)
        {
            lerping();
        }
        if (timer <= 0 && startposition2 != endpositon2)
        {
            lerping2();
        }

        if (timer <= -1.0f && activefalse == false)
        {
            Lerp = 0;
            activefalse = true;
            startposition = panel.transform.position;
            endpositon = origin3.transform.position;

        }
        if (activefalse == true)
        {
            lerping();
        }
      

    }
    void lerping()
    {
        Lerp +=  Time.deltaTime / 1;

        if (activefalse == false)
        {
            part1.transform.position = Vector3.Lerp(startposition, endpositon, Lerp);
        }
        else
        {
            panel.transform.position = Vector3.Lerp(startposition, endpositon, Lerp);

        }

    }
    void lerping2()
    {
        Lerp2 += Time.deltaTime / 1;
        part2.transform.position = Vector3.Lerp(startposition2, endpositon2, Lerp2);
    }

    void PlayClick()
    {
        Application.LoadLevel(1);
    }
    void exitClick()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
    void soundClick()
    {
        panel.SetActive(false);
        soundpanel.SetActive(true);


    }
    void BackClick()
    {
        panel.SetActive(true);

        soundpanel.SetActive(false);


    }
    public void muteClick(bool mute)
    {
        if (mute)
        {
            AudioListener.volume = 0.0f;
        }
        else {
            AudioListener.volume = 100.0f;
        }
    }

}
