using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

public class Spells : Cards {
    private static BaseFunctions baseFunctions;

    string type;
    private Dictionary<string, Action> FunctionsList;

    public static BaseFunctions Base_Functions
    {
        get
        {
            return baseFunctions;
        }

        set
        {
            baseFunctions = value;
        }
    }

    public void FillingFunctionsMap()
    {
        FunctionsList = new Dictionary<string, Action>();
        Spells s = new Spells();
        FunctionsList.Add("Pot of Greed", () => this.PotOfGreed()); //d
        FunctionsList.Add("Messenger Of Peace", () => this.MessengerOfPeace());
        FunctionsList.Add("Change Of Heart", () => this.ChangeOfHeart());
        FunctionsList.Add("Cost Down", () => this.CostDown());
        FunctionsList.Add("Graceful Charity", () => this.GracefulCharity());
        FunctionsList.Add("Gravity Axe - Grarl", () => this.GravityAxe_Grarl());
        FunctionsList.Add("Graceful Dice", () => this.GracefulDice());
        FunctionsList.Add("Dragon Treasure", () => this.DragonTreasure()); //d

    }
    public Spells(Spells s)
    {
        this.CardName = s.CardName;
        this.CardDesc = s.CardDesc;
        this.CardImage = s.CardImage;
        this.CardOwner = s.CardOwner;
        this.ID = s.ID;
        
    }



    public Spells(string name, string Desc, Sprite image)
    {
        CardName = name;
        CardDesc = Desc;
        CardImage = image;
    }
    public Spells()
    {

    }
   
    public override void destroyCard()
    {
        
    }
    public void Draw(DeckBehaviour deckB,Transform Deck,Transform Hand)
    {
       // deckB.CreateCard(Deck, Hand);
    }
    public void InvokeFunction(string FunctionName)
    {
        FunctionsList[FunctionName].DynamicInvoke();
    }
    
    public void PotOfGreed()
    {
        //Debug.Log(CardOwner);
        if (CardOwner == 1)
        {

            for (int i = 0; i < 2; i++)
            {
                baseFunctions.DB.Invoke("DrawCardPlayer1", i * 1.5f);
            }
        }
        else if (CardOwner == 2)
        {
            for (int i = 0; i < 2; i++)
            {
                baseFunctions.DB.Invoke("DrawCardPlayer2", i * 1.5f);
            }
        }
        Debug.Log("PotOfGreed");

    }
    public void GracefulDice()
    {
        System.Random newrandom = new System.Random();
        int rndm = newrandom.Next(1, 7);
        Debug.Log(rndm);
        if (this.CardOwner == 1)
        {
            Debug.Log(baseFunctions.playerOneMonsterField.gameObject.name);
            for (int i = 0; i < baseFunctions.playerOneMonsterField.transform.childCount; i++)
            {
                GameObject e = baseFunctions.playerOneMonsterField.transform.GetChild(i).gameObject;
                Debug.Log(e.name);
                Debug.Log(CardsDB.PlayerOne.MyDeck.PlayerDeck[e.name].ID);
                ((Monsters)CardsDB.PlayerOne.MyDeck.PlayerDeck[e.name]).TempattackPoints += (rndm * 100);
                ((Monsters)CardsDB.PlayerOne.MyDeck.PlayerDeck[e.name]).TempdefencePoints += (rndm * 100);
            }

        }
        else
        {
            for (int i = 0; i < baseFunctions.playerTwoMonsterField.transform.childCount; i++)
            {
                GameObject e = baseFunctions.playerTwoMonsterField.transform.GetChild(i).gameObject;
                Debug.Log(e.name);
                ((Monsters)CardsDB.PlayerTwo.MyDeck.PlayerDeck[e.name]).TempattackPoints += (rndm * 100);
                ((Monsters)CardsDB.PlayerTwo.MyDeck.PlayerDeck[e.name]).TempdefencePoints += (rndm * 100);
            }
        }
        Debug.Log("GracefulDice");
    }
    public void GracefulCharity()
    {
        if (CardOwner == 1)
        {
            int counter = 0;
            for (int i = 0; i < 3; i++, counter++)
            {

                baseFunctions.DB.Invoke("DrawCardPlayer1", (counter * 1.5f));
                //deckBehaviour.CreateCard(playerOneDeck.transform, playerOneHand.transform, "Player1"); 

            }

            //baseFunctions.playerOneHand.transform.GetChild(i).parent = baseFunctions.playerOneHand.transform.GetChild(i).parent;
            if (baseFunctions.playerOneHand.transform.childCount != 0)
            {
                baseFunctions.CardGameObject = baseFunctions.playerOneHand.transform.GetChild(0).gameObject;
                baseFunctions.Invoke("InvokingLerpToPlayerOneGraveYard", counter * 1.5f);
                
            }
        }
        else if (CardOwner == 2)
        {
            int counter = 0;
            for (int i = 0; i < 3; i++, counter++)
            {

                baseFunctions.DB.Invoke("DrawCardPlayer1", (counter * 1.5f));
            }

            if (baseFunctions.playerTwoHand.transform.childCount != 0)
            {
                baseFunctions.CardGameObject = baseFunctions.playerTwoHand.transform.GetChild(0).gameObject;
                baseFunctions.Invoke("InvokingLerpToPlayerTwoGraveYard", counter * 1.5f);

            }
        }
    }
    public void DragonTreasure()
    {
        Debug.Log("DragonTreasure");
        if (this.CardOwner == 1)
        {
            Debug.Log(baseFunctions.playerOneMonsterField.gameObject.name);
            for (int i = 0; i < baseFunctions.playerOneMonsterField.transform.childCount; i++)
            {
                GameObject e = baseFunctions.playerOneMonsterField.transform.GetChild(i).gameObject;
                Debug.Log(e.name);
                if (((Monsters)CardsDB.PlayerOne.MyDeck.PlayerDeck[e.name]).Race == "Dragon")
                {
                    Debug.Log(CardsDB.PlayerOne.MyDeck.PlayerDeck[e.name].ID);
                    ((Monsters)CardsDB.PlayerOne.MyDeck.PlayerDeck[e.name]).TempattackPoints += 300;
                    ((Monsters)CardsDB.PlayerOne.MyDeck.PlayerDeck[e.name]).TempdefencePoints += 300;

                }
            }

        }
        else
        {
            for (int i = 0; i < baseFunctions.playerTwoMonsterField.transform.childCount; i++)
            {
                GameObject e = baseFunctions.playerTwoMonsterField.transform.GetChild(i).gameObject;
                Debug.Log(e.name);
                if (((Monsters)CardsDB.PlayerTwo.MyDeck.PlayerDeck[e.name]).Race == "Dragon")
                {
                    ((Monsters)CardsDB.PlayerTwo.MyDeck.PlayerDeck[e.name]).TempattackPoints += 300;
                    ((Monsters)CardsDB.PlayerTwo.MyDeck.PlayerDeck[e.name]).TempdefencePoints += 300;

                }
            }
        }
    }
    public void GravityAxe_Grarl()
    {
        if (CardOwner == 1)
        {
            for (int i = 0; i < baseFunctions.playerOneHand.transform.childCount; i++)
            {
                GameObject Object = baseFunctions.playerOneHand.transform.GetChild(i).gameObject;
                if (CardsDB.PlayerOne.MyDeck.PlayerDeck[Object.name].GetType() == typeof(Monsters))
                {
                    ((Monsters)CardsDB.PlayerOne.MyDeck.PlayerDeck[baseFunctions.playerOneHand.transform.GetChild(i).gameObject.name]).TempattackPoints += 500;
                    break;
                }
            }
        }
        else if (CardOwner == 2)
        {
            for (int i = 0; i < baseFunctions.playerTwoHand.transform.childCount; i++)
            {
                if (CardsDB.PlayerTwo.MyDeck.PlayerDeck[baseFunctions.playerTwoHand.transform.GetChild(i).gameObject.name].GetType() == typeof(Monsters))
                {
                    ((Monsters)CardsDB.PlayerTwo.MyDeck.PlayerDeck[baseFunctions.playerTwoHand.transform.GetChild(i).gameObject.name]).TempattackPoints += 500;
                    break;
                }
            }

            Debug.Log("GravityAxe_Grarl");
        }
    }
    public void MessengerOfPeace()
    {
        Debug.Log("MessengerOfPeace");
    }
    public void CostDown()
    {
        Debug.Log("CostDown");
    }
    public void ChangeOfHeart()
    {
        if(CardOwner==1)
        {
            if(baseFunctions.playerTwoMonsterField.transform.childCount!=0)
            {

            }
        }
        else
        {
            if (baseFunctions.playerOneMonsterField.transform.childCount != 0)
            {

            }

        }
        Debug.Log("ChangeOfHeart");
    }
    
}
