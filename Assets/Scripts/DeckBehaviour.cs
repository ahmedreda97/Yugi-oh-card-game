using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class DeckBehaviour : MonoBehaviour
{

    public GameObject Phase;
    public GameObject Card;
    public BaseFunctions baseFunctions;
    [HideInInspector]
    public bool clickedPlayerOne = false;
    GameObject inst1;

    //public GameObject Panel;

    float ScaleX = 43f;
    float ScaleY = 65.4f;
    float ScaleZ = 65.28835f;

    public static bool standendPlayerOne = false;

    float PlayerOneLerp = 0, PlayerOneDuration = 1;
    //public Sprite Defult
    public GameObject Hand;

    PhasesControl phaseControl;
    Vector3 PlayerOneStart, PlayerOneEnd;
    int handChilds;
    public string PlayerType;
    private void Awake()
    {
        PlayerOneStart = this.transform.position;
        PlayerOneEnd = Hand.transform.position;
        
    }
    void Start()
    {
        phaseControl = Phase.GetComponent<PhasesControl>();
    }

    public void Update()
    {
        if (standendPlayerOne == true )
        {
            if (CardsDB.PlayerOne.IsTurn==true && PlayerType=="Player1")
            {
                CreateCard(this.transform,Hand.transform,PlayerType);
                //phaseControl.SPTrue.gameObject.SetActive(true);
                standendPlayerOne = false;
            }
            else if(CardsDB.PlayerTwo.IsTurn==true && PlayerType == "Player2")
            {
                CreateCard(this.transform,Hand.transform,PlayerType);
                phaseControl.SPTrue.gameObject.SetActive(true);
                standendPlayerOne = false;
            }
        }
        if (phaseControl.FirstCase == true)
        {
            if (clickedPlayerOne == false && Hand.transform.childCount < 4 && standendPlayerOne == false)
            {
                CreateCard(this.transform,Hand.transform,PlayerType);
            }
            if (Hand.transform.childCount>=4 && PlayerType=="Player1")
            {
                phaseControl.FirstCase = false;
                if(PlayerType=="Player1")
                {
                    standendPlayerOne = true;
                }
            }
        }
        
    }
    private void FixedUpdate()
    {
        if (clickedPlayerOne == true)
        {
            baseFunctions.LerpFunction(ref clickedPlayerOne, PlayerOneStart, PlayerOneEnd,ref inst1, ref PlayerOneLerp,Hand);
            if (phaseControl.FirstCase == false)
            {
                phaseControl.Mp1True.gameObject.SetActive(true);
            }
        }
    }
   
    
    public void CreateCard(Transform Parent,Transform Field,string PlayerType)
	{
        //Creating a Card Object in unity and setting its scale and panel and its parent

        inst1=Instantiate(Card, Parent.transform.position, Parent.transform.rotation) as GameObject;
        inst1.transform.SetParent(Field.transform.parent);
        clickedPlayerOne = true;
        
        float rate = 68 / ScaleX;
        inst1.GetComponent<RectTransform>().localScale = new Vector3(68, ScaleY * rate, ScaleZ * rate);
        //Panel.transform.SetParent(Inst.transform);

        //geting cards from dataBase
        Cards c = null; ;
        if(PlayerType== "Player1")
        {
            c = CardsDB.PlayerOne.RandomOrderPlayingCard[CardsDB.PlayerOne.RandomOrderPlayingCard.Count - 1];
            CardsDB.PlayerOne.RandomOrderPlayingCard.RemoveAt(CardsDB.PlayerOne.RandomOrderPlayingCard.Count - 1);
        }
        else
        {
            c = CardsDB.PlayerTwo.RandomOrderPlayingCard[CardsDB.PlayerTwo.RandomOrderPlayingCard.Count - 1];
            CardsDB.PlayerTwo.RandomOrderPlayingCard.RemoveAt(CardsDB.PlayerTwo.RandomOrderPlayingCard.Count - 1);
        }
        //print(c.ID);
       

        //Giving the card a birth mark
        if (c.GetType() == typeof(Monsters) || c.GetType() == typeof(EffectedMonsters))
        {
            //Monsters m = new Monsters();
            Monsters newMonster = new Monsters((Monsters)c);
            inst1.transform.GetChild(0).GetComponent<Image>().sprite = newMonster.CardImage;
            inst1.name = newMonster.ID;
            inst1.transform.GetChild(1).GetComponent<Text>().text = "ATK/" + newMonster.AttackPoints + "   " + "DEF/" + newMonster.DefencePoints;
        }
        else if (c.GetType() == typeof(Spells))
        {
            Spells newMonster = new Spells((Spells)c);
            inst1.name = newMonster.ID;
            inst1.transform.GetChild(0).GetComponent<Image>().sprite = newMonster.CardImage;

            inst1.transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<Text>().text = "Activate";

        }
        else if (c.GetType() == typeof(Traps))
        {
            Traps newMonster = new Traps((Traps)c);
            inst1.name = newMonster.ID;
            inst1.transform.GetChild(0).GetComponent<Image>().sprite = newMonster.CardImage;
            inst1.transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<Text>().text = "Activate";
        }
        if (CardsDB.PlayerOne.IsTurn && PlayerType == "Player2")
        {
            inst1.transform.GetChild(0).GetComponent<Image>().sprite = BaseFunctions.defultSprite;
        }
        else if (CardsDB.PlayerTwo.IsTurn && PlayerType == "Player1")
        {
            inst1.transform.GetChild(0).GetComponent<Image>().sprite = BaseFunctions.defultSprite;
        }
    }
    public void DrawCardPlayer1()
    {
        CreateCard(this.transform, Hand.transform, "Player1");
    }
    public void DrawCardPlayer2()
    {
        CreateCard(this.transform, Hand.transform, "Player2");
    }
}








