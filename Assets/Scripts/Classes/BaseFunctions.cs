using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseFunctions : MonoBehaviour {

    public Sprite DefultSprite;
    public DeckBehaviour DB;
    public PhasesControl phaseController;
    public GameObject CardGameObject;
    float lerp = 0;
    bool LerpingCheck;
    public GameObject playerOneHand, playerTwoHand, playerOneGraveYard, playerTwoGraveYard, playerOneMonsterField, playerOneSpellField, playerTwoMonsterField, playerTwoSpellField, playerOneDeck, playerTwoDeck;
    public Slider playerOneHealth, playerTwoHealth;

    public bool Negate;
    public static Sprite defultSprite;
    public string PlayerOneType = "Player1", PlayerTwoType = "Player2";
	public void LerpFunction(ref bool check,Vector3 StartPostion,Vector3 EndPostion,ref GameObject Object,ref float LerpingFactor,GameObject Field)
    {
        if(check==true)
        {
            LerpingFactor += Time.deltaTime / 1;
            Object.transform.position = Vector3.Lerp(StartPostion, EndPostion, LerpingFactor);
            if (Object.transform.position == EndPostion)
            {
                check = false;
                LerpingFactor = 0;
                Object.transform.SetParent(Field.transform);

            }
        }


    }
    private void FixedUpdate()
    {
        if(CardGameObject!=null)
        {
            LerpFunction(ref LerpingCheck, CardGameObject.transform.position, playerOneGraveYard.transform.position, ref CardGameObject, ref lerp, playerOneGraveYard);

        }
        
    }
    public void InvokingLerpToPlayerOneGraveYard()
    {
        LerpingCheck = true;
        LerpFunction(ref LerpingCheck, CardGameObject.transform.position, playerOneGraveYard.transform.position,ref CardGameObject,ref lerp, playerOneGraveYard);
       
    }
    public void InvokingLerpToPlayerTwoGraveYard()
    {
        LerpingCheck = true;
        LerpFunction(ref LerpingCheck, CardGameObject.transform.position, playerTwoGraveYard.transform.position, ref CardGameObject, ref lerp, playerTwoGraveYard);

    }

    public void PlayerHandCheck(Transform Hand, string PlayerType)
    {
        if (PlayerType == PlayerOneType)
        {
            CardsDB.PlayerOne.PlayerHand.NotifyChange(Hand.transform, PlayerOneType);
        }
        else
        {
            CardsDB.PlayerOne.PlayerHand.NotifyChange(Hand.transform, PlayerTwoType);
        }

        //print(CardsDB.PlayerOne.PlayerHand.PlayerHand.Count);
        //print(CardsDB.PlayerTwo.PlayerHand.PlayerHand.Count);
    }
    public void CardSpriteChange(Image cardImage,Sprite DefultSprit)
    {
        cardImage.sprite = DefultSprit;
    }
    public void LPDecInc(int amount, string AffectedPlayer)
    {
        if (AffectedPlayer == "Player1") { CardsDB.PlayerOne.lifePoints += amount; }
        if (AffectedPlayer == "Player2") { CardsDB.PlayerTwo.lifePoints += amount; }
    }

    public void Draw(Transform Deck, Transform Hand, string PlayerType)
    {

        if (PlayerType == "Player1")
        {

            DB.CreateCard(Deck.transform, Hand, PlayerType);

        }

        if (PlayerType == "Player2")
        {

            DB.CreateCard(Deck.transform, Hand, PlayerType);

        }


    }

    public void Destroy(ref GameObject Card, GameObject Grave, float LerpingFactor)
    {
        bool T = true;
        LerpFunction(ref T, Card.transform.position, Grave.transform.position,ref Card, ref LerpingFactor, Grave);

    }
    public void change_atk_def(Monsters mon, int atk, int def)
    {
        mon.AttackPoints += atk;
        mon.DefencePoints += def;
    }

    public void control(float lerpingfactor,ref GameObject card, GameObject field, ref bool check)
    {
        LerpFunction(ref check, card.transform.position, field.transform.position,ref card, ref lerpingfactor, field);
    }
    public void Get_NegateEffect(Spells spell, bool negate)
    {
        //   spell.negate=true must be done in datebase
        Negate = negate;

    }
    public bool Set_NegateEffect() //edited in Attack script
    {
        //   spell.negate=true must be done in datebase 

        return Negate;
    }
    public void Change_State(ref Monsters monster, GameObject card)
    {
        if (monster.AttacDefencState == true)
        {
            monster.AttacDefencState = false;
            card.GetComponent<RectTransform>().localScale = new Vector3(43f, 65.4f, 65.28835f);
        }
        else
        {
            card.GetComponent<RectTransform>().localScale = new Vector3(45f, 35f, 65.28835f);
            card.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, 90);
            monster.AttacDefencState = true;
        }

    }
    public void ReturnToDeck(Transform deck, Transform field, ref bool check, GameObject Object, ref float LerpingFactor, GameObject Field, Cards card, Player owner)
    {
        LerpFunction(ref check, field.position, deck.position, ref Object, ref LerpingFactor, Field);
        //Transform cardd = field.FindChild(card.CardName);
        //cardd.parent = deck;// set the new parent of the cardd to deck
        //owner.RandomOrderPlayingCard.Add(card); //add to deck
        //owner.Field.Remove(card);           //remove from field

    }
    public void ReturnToHand(Transform hand, Transform field, ref bool check, GameObject Object, ref float LerpingFactor, GameObject Field, Cards card, Player owner)
    {
        LerpFunction(ref check, field.position, hand.position, ref Object, ref LerpingFactor, Field);
        //Transform cardd = field.FindChild(card.CardName);
        //cardd.parent = hand; //set the new parent of the cardd to hand
        //owner.Hand.Add(card);  //add to hand
        //owner.Field.Remove(card); //remove from field
    }
    public void Banish(GameObject Object, Player owner, Cards card, Transform field)
    {
        MonoBehaviour.Destroy(Object);
        //owner.Field.Remove(card);
        //Transform cardd = field.FindChild(card.CardName);
        //cardd.parent = null; //set the parent to null (vanishes completly)


    }


}
