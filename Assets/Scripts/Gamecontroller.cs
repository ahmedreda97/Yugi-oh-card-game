using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gamecontroller : MonoBehaviour
{

    BaseFunctions base_Functions;
    [HideInInspector]
    public string AttackPlayer;

    [HideInInspector]
    public bool player_Click, Enemy_Click, attack_on, Gograve, EmptyField, destroy_both, both_destroyed;

    //[HideInInspector]
    public Slider PlayerHealth, EnemyHealth;

    [HideInInspector]
    public Vector3 Pgrave, Egrave, startposition, endposition, Pposition, Eposition;

    [HideInInspector]
    public GameObject PlayerOneCardGameObject, PlayerTwoCardGameObject, attackCard, Graveyard, PlayerTwoGraveYard, PlayerOneGraveYard, PreviousCard;

    [HideInInspector]
    public Monsters Player_MonsterClass_Card, Enemy_MonsterClass_Card, Attack_MonsterClass_card;


    float Lerp = 0;

    private void Awake()
    {
        base_Functions = this.gameObject.GetComponent<BaseFunctions>();
        PlayerTwoGraveYard = GameObject.FindGameObjectWithTag("EnemyGrave");
        Egrave = PlayerTwoGraveYard.transform.position;
        PlayerOneGraveYard = GameObject.FindGameObjectWithTag("PlayerGrave");
        Pgrave = PlayerOneGraveYard.transform.position;

    }
    // Use this for initialization
    void Start()
    {
        PlayerHealth.value = CardsDB.PlayerOne.lifePoints;
        EnemyHealth.value = CardsDB.PlayerTwo.lifePoints;
    }

    // Update is called once per frame
    void Update()
    {
        SettingPlayerHealth(PlayerHealth, CardsDB.PlayerOne);
        SettingPlayerHealth(EnemyHealth, CardsDB.PlayerTwo);
        if (EmptyField == true)
        {
            Debug.Log("emptyy");
            if (AttackPlayer == "player" && PreviousCard != PlayerOneCardGameObject)
            {
                PreviousCard = PlayerOneCardGameObject;
                Player_MonsterClass_Card.Attack(CardsDB.PlayerTwo);

            }
            else if (AttackPlayer == "enemy" && PreviousCard != PlayerTwoCardGameObject)
            {
                PreviousCard = PlayerTwoCardGameObject;
                Enemy_MonsterClass_Card.Attack(CardsDB.PlayerOne);
            }
            AttackPlayer = "";
            EmptyField = false;
            //player_Click = false;
            //Enemy_Click = false;
        }
        else if (attack_on == true && EmptyField == false)
        {

            if (AttackPlayer == "player" && PreviousCard != PlayerOneCardGameObject)
            {
                PreviousCard = PlayerOneCardGameObject;
                Attack_MonsterClass_card = Player_MonsterClass_Card.Attack(Enemy_MonsterClass_Card, CardsDB.PlayerOne, CardsDB.PlayerTwo);
                //addCardToGraveyard(Attack_MonsterClass_card);
                AttackPlayer = "";
            }
            else if (AttackPlayer == "enemy" && PreviousCard != PlayerTwoCardGameObject)
            {
                PreviousCard = PlayerTwoCardGameObject;
                Attack_MonsterClass_card = Enemy_MonsterClass_Card.Attack(Player_MonsterClass_Card, CardsDB.PlayerTwo, CardsDB.PlayerOne);
                //addCardToGraveyard(Attack_MonsterClass_card);
                AttackPlayer = "";
            }

            if (Attack_MonsterClass_card == Player_MonsterClass_Card)
            {
                attackCard = PlayerOneCardGameObject;
                Gograve = true;
                Graveyard = PlayerOneGraveYard;
                startposition = attackCard.transform.position;
                endposition = PlayerOneGraveYard.transform.position;

                LerpingCards(Gograve);
            }
            else if (Attack_MonsterClass_card == Enemy_MonsterClass_Card)
            {
                attackCard = PlayerTwoCardGameObject;
                Graveyard = PlayerTwoGraveYard;
                Gograve = true;
                startposition = attackCard.transform.position;
                endposition = PlayerTwoGraveYard.transform.position;


                LerpingCards(Gograve);

            }
            else
            {
                if (Gograve == false || Gograve == null)
                {
                    Debug.Log("destroyBoth");
                    destroy_both = true;
                    attackCard = PlayerTwoCardGameObject;
                    Graveyard = PlayerTwoGraveYard;
                    startposition = attackCard.transform.position;
                    endposition = PlayerTwoGraveYard.transform.position;
                    Gograve = true;
                    LerpingCards(Gograve);
                }
                else
                {
                    LerpingCards(Gograve);
                }
            }

        }

        SettingPlayerHealth(PlayerHealth, CardsDB.PlayerOne);
        SettingPlayerHealth(EnemyHealth, CardsDB.PlayerTwo);
    }


    public static void SettingPlayerHealth(Slider PlayerHealth, Player player)
    {
        PlayerHealth.value = player.lifePoints;
    }
    public void LerpingCards(bool Check)
    {
        if (Check == true)
        {

            Lerp += Time.deltaTime / 1;
            attackCard.transform.position = Vector3.Lerp(startposition, endposition, Lerp);
            if (attackCard.transform.transform.position == endposition)
            {
                Gograve = false;

                attackCard.SetActive(false);
                if (destroy_both == true)
                {

                    attackCard.transform.parent = Graveyard.transform;
                    destroy_both = false;
                    attackCard = PlayerOneCardGameObject;
                    Gograve = true;
                    Graveyard = PlayerOneGraveYard;
                    startposition = attackCard.transform.position;
                    endposition = PlayerOneGraveYard.transform.position;
                    Lerp = 0;
                    LerpingCards(Gograve);

                }

                else
                {

                    Gograve = false;
                    attackCard.transform.parent = Graveyard.transform;
                    //Destroy(attackCard);
                    PlayerTwoCardGameObject = null;
                    PlayerOneCardGameObject = null;
                    Player_MonsterClass_Card = null;
                    Attack_MonsterClass_card = null;
                    Enemy_MonsterClass_Card = null;
                    Enemy_Click = false;
                    player_Click = false;
                    attack_on = false;

                    Lerp = 0;
                }

            }
        }
    }

    //public void addCardToGraveyard(Monsters DeadMosnter)
    //{
    //    print(DeadMosnter.ID);
    //    if (CardsDB.PlayerOne.MyDeck.PlayerDeck.ContainsKey(DeadMosnter.ID) == true)
    //    {
    //        CardsDB.PlayerOne.GraveYard.Add(DeadMosnter);
    //        //Debug.Log("Player 1" + CardsDB.PlayerOne.GraveYard.Count);
    //    }
    //    else if (CardsDB.PlayerTwo.MyDeck.PlayerDeck.ContainsKey(DeadMosnter.ID) == true)
    //    {
    //        CardsDB.PlayerTwo.GraveYard.Add(DeadMosnter);
    //        //Debug.Log("Player 2" + CardsDB.PlayerTwo.GraveYard.Count);
    //    }
    //}
}


