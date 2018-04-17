using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public abstract class Cards {

    //public static Dictionary<int, Cards> AllCards;
    private string iD;
    private string cardName;
    private Sprite cardImage;
    private string cardDesc;
    private bool faceState;
    private int cardOwner;

    public bool FaceState
    {
        get
        {
            return faceState;
        }

        set
        {
            faceState = value;
        }
    }

    public string CardName
    {
        get
        {
            return cardName;
        }

        set
        {
            cardName = value;
        }
    }

    public Sprite CardImage
    {
        get
        {
            return cardImage;
        }

        set
        {
            cardImage = value;
        }
    }

    public string CardDesc
    {
        get
        {
            return cardDesc;
        }

        set
        {
            cardDesc = value;
        }
    }

    public string ID
    {
        get
        {
            return iD;
        }

        set
        {
            iD = value;
        }
    }

    public int CardOwner
    {
        get
        {
            return cardOwner;
        }

        set
        {
            cardOwner = value;
        }
    }

    public abstract void destroyCard();
}
