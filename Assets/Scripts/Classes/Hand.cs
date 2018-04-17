using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand  {

    private List<Cards> playerHand;
    public Hand()
    {
        PlayerHand = new List<Cards>();
    }

    public List<Cards> PlayerHand
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

    public void NotifyChange(Transform HandGameObject,string PlayerType)
    {
        if(PlayerHand.Count!=HandGameObject.childCount)
        {
            PlayerHand = new List<Cards>();
            if(CardsDB.PlayerOne.MyDeck.PlayerDeck.ContainsKey(HandGameObject.GetChild(0).gameObject.name)==true)
            {
                if (CardsDB.PlayerOne.MyDeck.PlayerDeck[HandGameObject.GetChild(0).gameObject.name].CardOwner == 1)
                {
                    for (int i = 0; i < HandGameObject.childCount; i++)
                    {
                        PlayerHand.Add(CardsDB.PlayerOne.MyDeck.PlayerDeck[HandGameObject.GetChild(i).gameObject.name]);
                    }
                }
            }
            else if(CardsDB.PlayerTwo.MyDeck.PlayerDeck.ContainsKey(HandGameObject.GetChild(0).gameObject.name) == true)
            {
                if (CardsDB.PlayerOne.MyDeck.PlayerDeck[HandGameObject.GetChild(0).gameObject.name].CardOwner == 2)
                {
                    for (int i = 0; i < HandGameObject.childCount; i++)
                    {
                        PlayerHand.Add(CardsDB.PlayerTwo.MyDeck.PlayerDeck[HandGameObject.GetChild(i).gameObject.name]);
                    }

                }
            }
            
            
        }
    }
}
