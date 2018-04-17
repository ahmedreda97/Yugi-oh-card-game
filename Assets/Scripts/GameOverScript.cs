using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour {

    float RestartDelay = 7f;
    Animator Anime;
    float RestartTimer=0;
    public GameObject Panel;
    public GameObject Text;
    public GameObject Screen;
    public Text TT;
    bool done ;
    bool rotated;
    void Awake()
    {
        Anime = GetComponent<Animator>();
        Panel = GameObject.FindWithTag("GOPanel");
        Text = GameObject.FindWithTag("GOText");
        TT = Text.GetComponent<Text>();
        Panel.SetActive(false);
        Text.SetActive(false);
        done = false;
        rotated = false;
    }
	void Update () 
    {
		if ((CardsDB.PlayerOne.lifePoints<=0) && done==false)
        {
            if (rotated == false)
            {
                PhasesControl.RotateCamera.rotate();
                rotated = true;
            }
            TT.text = "Player 1 Lost";
            Panel.SetActive(true);
            Text.SetActive(true);
            Anime.SetTrigger("GameOver");
            RestartTimer += Time.deltaTime;
            if (RestartTimer>=RestartDelay)
            {
                Application.LoadLevel(1);
            }
        }
        else if ((CardsDB.PlayerTwo.lifePoints<=0)&& done==false)
        {
            Panel.SetActive(true);
            TT.text = "Player 2 Lost";
            Text.SetActive(true);
            Anime.SetTrigger("GameOver");
            RestartTimer += Time.deltaTime;
            if (RestartTimer >= RestartDelay)
            {
                Application.LoadLevel(1);
            }
        }
	}
}
