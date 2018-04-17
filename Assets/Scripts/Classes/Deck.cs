using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Deck  {

    private Dictionary<string, Cards> playerDeck;  //int=id ,, Cards=obje
    public int size;

    public Dictionary<string, Cards> PlayerDeck
    {
        get
        {
            return playerDeck;
        }

        set
        {
            playerDeck = value;
        }
    }

    public Deck()
    {
        PlayerDeck = new Dictionary<string, Cards>();
        size = 0;
    }
    
}
