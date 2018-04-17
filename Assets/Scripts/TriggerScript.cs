using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class TriggerScript : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler {

    Image image;
    Text txt;
    GameObject Img, textField;
   
    private void Start()
    {
        Img =GameObject.FindGameObjectWithTag("ImageViewer");
        textField = GameObject.FindGameObjectWithTag("TextViewer");
        image = Img.GetComponent<Image>();
        txt = textField.GetComponent<Text>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        Cards card =  null;
 
        if (CardsDB.PlayerOne.IsTurn == true && CardsDB.PlayerOne.MyDeck.PlayerDeck.ContainsKey(this.transform.parent.gameObject.name))
        {
            //print("Player 1");
            if (CardsDB.PlayerOne.MyDeck.PlayerDeck[this.transform.parent.gameObject.name].CardOwner == 1)
            {
                card = CardsDB.PlayerOne.MyDeck.PlayerDeck[this.transform.parent.gameObject.name];
                
            }

        }
        if (CardsDB.PlayerTwo.IsTurn == true && CardsDB.PlayerTwo.MyDeck.PlayerDeck.ContainsKey(this.transform.parent.gameObject.name))
        {
            //print("Player 2");
            if (CardsDB.PlayerTwo.MyDeck.PlayerDeck[this.transform.parent.gameObject.name].CardOwner == 2)
            {
                card = CardsDB.PlayerTwo.MyDeck.PlayerDeck[this.transform.parent.gameObject.name];
            }
        }
        if (card!=null)
        {
            if (card.GetType() == typeof(Monsters) || card.GetType() == typeof(EffectedMonsters))
            {
                
                Monsters monster = (Monsters)card;
                print(monster.ID);
                image.GetComponent<Image>().sprite = monster.CardImage;
                txt.text = monster.CardName + "\r\n" + monster.CardDesc + "\r\n ATK:" + monster.TempattackPoints + "  DEF:" + monster.TempdefencePoints;
            }
            else if (card.GetType() == typeof(Spells))
            {
                Spells spell = (Spells)card;
                image.GetComponent<Image>().sprite = spell.CardImage;
                txt.text = spell.CardName + "\r\n" + spell.CardDesc;
            }
            else
            {
                Traps trap = (Traps)card;
                image.GetComponent<Image>().sprite = trap.CardImage;
                txt.text = trap.CardName + "\r\n" + trap.CardDesc;
            }
        }
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.GetComponent<Image>().sprite = null;
        txt.text = "";
    }
}
