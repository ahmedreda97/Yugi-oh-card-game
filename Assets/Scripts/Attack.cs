using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Attack : MonoBehaviour, IPointerClickHandler
{

    //[HideInInspector]
    public GameObject Player_Field, Enemy_Field;

    //[HideInInspector]
    Monsters Player_monster, Enemy_monster;

    Cards Card;
    Gamecontroller gamecontroller;

    //[HideInInspector]
    PhasesControl phasecontrol;

    void Start()
    {
        phasecontrol = GameObject.FindGameObjectWithTag("GameController").GetComponent<PhasesControl>();
        gamecontroller = GameObject.FindGameObjectWithTag("GameController").GetComponent<Gamecontroller>();
        Player_Field = GameObject.FindGameObjectWithTag("MonsterCardField");
        Enemy_Field = GameObject.FindGameObjectWithTag("EnemyMonster");
    }

    // Update is called once per frame
    void Update()
    {

        if (phasecontrol.battle == true)
        {

            if (gamecontroller.Enemy_Click == true && gamecontroller.player_Click == true)
            {
                gamecontroller.attack_on = true;
            }
            else if (gamecontroller.Enemy_Click == false && gamecontroller.player_Click == true && CardsDB.PlayerOne.IsTurn == true && phasecontrol.FirstCase == false)
            {
                gamecontroller.AttackPlayer = "player";
                //print(CardsDB.PlayerOne.IsTurn == true);
            }
            else if (gamecontroller.Enemy_Click == true && gamecontroller.player_Click == false && CardsDB.PlayerOne.IsTurn == false && phasecontrol.FirstCase == false)
            {
                gamecontroller.AttackPlayer = "enemy";
            }

        }
        else
        {
            gamecontroller.AttackPlayer = "";
            gamecontroller.Enemy_Click = false;
            gamecontroller.player_Click = false;
            gamecontroller.attack_on = false;
            gamecontroller.EmptyField = false;
            gamecontroller.Gograve = false;
        }

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (phasecontrol.battle == true)
        {
            //print(this.transform.parent == Player_Field.transform);
            if (this.transform.parent == Player_Field.transform && phasecontrol.FirstTurn == false)
            {
                gamecontroller.player_Click = true;
                if (CardsDB.PlayerOne.MyDeck.PlayerDeck.ContainsKey(this.transform.gameObject.name))
                {
                    gamecontroller.Player_MonsterClass_Card = (Monsters)CardsDB.PlayerOne.MyDeck.PlayerDeck[this.transform.gameObject.name];
                }
                gamecontroller.Pposition = this.transform.position;
                gamecontroller.PlayerOneCardGameObject = this.gameObject;
                if (Enemy_Field.transform.childCount == 0)
                {
                    gamecontroller.EmptyField = true;
                    gamecontroller.AttackPlayer = "player";
                }
            }
            else if (this.transform.parent == Enemy_Field.transform && phasecontrol.FirstTurn == false)
            {

                gamecontroller.Enemy_Click = true;
                if (CardsDB.PlayerTwo.MyDeck.PlayerDeck.ContainsKey(this.transform.gameObject.name))
                {
                    gamecontroller.Enemy_MonsterClass_Card = (Monsters)CardsDB.PlayerTwo.MyDeck.PlayerDeck[this.transform.gameObject.name];
                }
                gamecontroller.Eposition = this.transform.position;
                gamecontroller.PlayerTwoCardGameObject = this.gameObject;
                if (Player_Field.transform.childCount == 0)
                {
                    gamecontroller.EmptyField = true;
                    gamecontroller.AttackPlayer = "enemy";

                }
            }
        }



    }

}
