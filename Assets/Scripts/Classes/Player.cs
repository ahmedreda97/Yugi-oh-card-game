using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player {

    Deck myDeck;
    List<Cards> randomOrderPlayingCard;
    Hand playerHand;
    //Dictionary<string, Cards> playerCardMap;
    public int lifePoints;
    string playerName;
    List<Cards> graveYard;
    bool isTurn;
    public Player()
    {
        myDeck = new Deck();
        randomOrderPlayingCard = new List<Cards>();
        PlayerHand = new Hand();
        graveYard = new List<Cards>();
        lifePoints = 8000;
        
    }
    public bool IsTurn
    {
        get
        {
            return isTurn;
        }
        set
        {
            isTurn = value;
        }
    }
    public Deck MyDeck
    {
        get
        {
            return myDeck;
        }

        set
        {
            myDeck = value;
        }
    }

    public List<Cards> RandomOrderPlayingCard
    {
        get
        {
            return randomOrderPlayingCard;
        }

        set
        {
            randomOrderPlayingCard = value;
        }
    }
	public int get_LifePoints()
	{
		return lifePoints;
	}
	public void set_LifePoints(int points)
    {
		lifePoints = points;
       
    }

    public string PlayerName
    {
        get
        {
            return playerName;
        }

        set
        {
            playerName = value;
        }
    }

    public List<Cards> GraveYard
    {
        get
        {
            return graveYard;
        }

        set
        {
            graveYard = value;
        }
    }

    public Hand PlayerHand
    {
        get
        {
            return playerHand;
        }

        set
        {
            playerHand = value;
        }
    }

 

    public void DrawCard()
    {

    }
    public void DeckShuffle()
    {

    }

}
