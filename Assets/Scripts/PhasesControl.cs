using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhasesControl : MonoBehaviour {
	Gamecontroller control;
    BaseFunctions Base_Function;
    public Button SPFalse, DPFalse, MP1False, MP2False, BPFalse, EPFalse;
    public SpriteRenderer SPTrue, DPTrue, Mp1True, MP2True, BPTrue, EPTrue;
    public GameObject PlayerDeck, EnemyDeck;
    DeckBehaviour PlayerDeckBeh, EnemyDeckBeh;
    [HideInInspector]
    public bool FirstCase,FirstTurn;
    public static bool summonCheck = true;
    public Sprite[] PlayerTwoImg;
    public Sprite[] PlayerOneImg;
	public bool battle;
    public static Rotate RotateCamera;
    //public Sprite DefultImage;
    public   GameObject PlayerHand, EnemyHand;
    private void Awake()
    {
        Base_Function = this.gameObject.GetComponent<BaseFunctions>();
        FirstCase = true;
        FirstTurn = true;
        if (FirstCase == true)
        {
            CardsDB.PlayerOne.IsTurn = true;
            CardsDB.PlayerTwo.IsTurn = false;
        }
    }
    void Start () {
		control = GameObject.FindGameObjectWithTag ("GameController").GetComponent<Gamecontroller>();
        RotateCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Rotate>();
        DPTrue.gameObject.SetActive(true);
        SPTrue.gameObject.SetActive(true);
        Mp1True.gameObject.SetActive(false);
        MP2True.gameObject.SetActive(false);
        BPTrue.gameObject.SetActive(false);
        EPTrue.gameObject.SetActive(false);

        PlayerDeckBeh = PlayerDeck.GetComponent<DeckBehaviour>();
        EnemyDeckBeh = EnemyDeck.GetComponent<DeckBehaviour>();

        MP1False.onClick.AddListener(() =>
        {
            if(SPTrue.gameObject.activeSelf==true)
            {
                Mp1True.gameObject.SetActive(true);
            }
        });
        MP2False.onClick.AddListener(() =>
        {
            if (BPTrue.gameObject.activeSelf == true)
            {
                control.PreviousCard = null;
                MP2True.gameObject.SetActive(true);
                battle = false;

            }


        });
        BPFalse.onClick.AddListener(() =>
        {
            if (Mp1True.gameObject.activeSelf == true)
            {
                BPTrue.gameObject.SetActive(true);
					battle=true;
            }
			
        });
        EPFalse.onClick.AddListener(() =>
        {
            if (MP2True.gameObject.activeSelf == true)
            {

                EPTrue.gameObject.SetActive(true);
                Invoke("PicChange", 1);
                
               
                CardsDB.PlayerOne.IsTurn = !CardsDB.PlayerOne.IsTurn;
                CardsDB.PlayerTwo.IsTurn = !CardsDB.PlayerTwo.IsTurn;
                
                RotateCamera.rotate();
                FirstTurn = false;
                //print(CardsDB.PlayerOne.IsTurn + " " + CardsDB.PlayerTwo.IsTurn);
            }
            
        });
    }
	
	void Update () {
       if(DPTrue.gameObject.activeSelf==true && SPTrue.gameObject.activeSelf==false)
        {
            if (!CardsDB.PlayerOne.IsTurn)
            {
                for (int i = 0; i < PlayerHand.transform.childCount; i++)
                {
                    Base_Function.CardSpriteChange(PlayerHand.transform.GetChild(i).GetChild(0).GetComponent<Image>(), BaseFunctions.defultSprite);
                }
                for (int i = 0; i < EnemyHand.transform.childCount; i++)
                {
                    Base_Function.CardSpriteChange(EnemyHand.transform.GetChild(i).GetChild(0).GetComponent<Image>(), CardsDB.PlayerTwo.MyDeck.PlayerDeck[EnemyHand.transform.GetChild(i).name].CardImage);
                }
            }
            else if (CardsDB.PlayerOne.IsTurn)
            {
                for (int i = 0; i < EnemyHand.transform.childCount; i++)
                {
                    Base_Function.CardSpriteChange(EnemyHand.transform.GetChild(i).GetChild(0).GetComponent<Image>(), BaseFunctions.defultSprite);
                }
                for (int i = 0; i < PlayerHand.transform.childCount; i++)
                {
                    Base_Function.CardSpriteChange(PlayerHand.transform.GetChild(i).GetChild(0).GetComponent<Image>(), CardsDB.PlayerOne.MyDeck.PlayerDeck[PlayerHand.transform.GetChild(i).name].CardImage);
                }
            }
        }
       if(DeckBehaviour.standendPlayerOne == true)
        {
           
            SPTrue.gameObject.SetActive(true);
           
        }
        
    }
    public void PicChange()
    {
        if(DPFalse.GetComponent<SpriteRenderer>().sprite==PlayerOneImg[0])
        {
            DPFalse.GetComponent<SpriteRenderer>().sprite = PlayerTwoImg[0];
            SPFalse.GetComponent<SpriteRenderer>().sprite = PlayerTwoImg[1];
            MP1False.GetComponent<SpriteRenderer>().sprite = PlayerTwoImg[2];
            BPFalse.GetComponent<SpriteRenderer>().sprite = PlayerTwoImg[3];
            MP2False.GetComponent<SpriteRenderer>().sprite = PlayerTwoImg[4];
            EPFalse.GetComponent<SpriteRenderer>().sprite = PlayerTwoImg[5];
            
        }
        else
        {
            DPFalse.GetComponent<SpriteRenderer>().sprite = PlayerOneImg[0];
            SPFalse.GetComponent<SpriteRenderer>().sprite = PlayerOneImg[1];
            MP1False.GetComponent<SpriteRenderer>().sprite = PlayerOneImg[2];
            BPFalse.GetComponent<SpriteRenderer>().sprite = PlayerOneImg[3];
            MP2False.GetComponent<SpriteRenderer>().sprite = PlayerOneImg[4];
            EPFalse.GetComponent<SpriteRenderer>().sprite = PlayerOneImg[5];
        }
        DPTrue.gameObject.SetActive(true);
        SPTrue.gameObject.SetActive(false);
        Mp1True.gameObject.SetActive(false);
        MP2True.gameObject.SetActive(false);
        BPTrue.gameObject.SetActive(false);
        EPTrue.gameObject.SetActive(false);
        DeckBehaviour.standendPlayerOne = true;
        summonCheck = true;
    }
}
