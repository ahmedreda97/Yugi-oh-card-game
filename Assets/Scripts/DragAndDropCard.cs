using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

class Scales
{
    private  float x, y, z;
    public Scales(float x, float y, float z)
    {
        this.x = x;this.y = y;this.z = z;
    }

    public float X
    {
        get
        {
            return x;
        }
    }

    public float Y
    {
        get
        {
            return y;
        }
    }

    public float Z
    {
        get
        {
            return z;
        }
    }
}
public class DragAndDropCard : MonoBehaviour, IPointerClickHandler
{
    Scales DefenceScales = new Scales(55f, 40f, 65.28835f);
    Scales AttackScales = new Scales(43f, 65.4f, 65.28835f);
    
    public Sprite FlipedCard;
    [HideInInspector]
    public Button Attack;
    [HideInInspector]
    public Button Defence;
    [HideInInspector]
    public GameObject panel;
    [HideInInspector]
    public GameObject MonsterField;
    [HideInInspector]
    public GameObject SpecialField;
    [HideInInspector]
    public GameObject EnemyMonsterField;
    [HideInInspector]
    public GameObject EnemySpecialField;
    [HideInInspector]
    public GameObject EnmyHand,PlayerHand;
    [HideInInspector]
    public PhasesControl phaseControl;
    static bool TributeCheck;
    static GameObject TributeCard;
    bool playerOneCheck = false, PlayerTwoCheck = false;
    public Text Message;

    private void Awake()
    {
        Message = GameObject.FindGameObjectWithTag("Message").GetComponent<Text>();
        phaseControl = GameObject.FindGameObjectWithTag("GameController").GetComponent<PhasesControl>();
        MonsterField = GameObject.FindGameObjectWithTag("MonsterCardField");
        SpecialField = GameObject.FindGameObjectWithTag("SpecialCardField");
		EnemyMonsterField = GameObject.FindGameObjectWithTag("EnemyMonster");
		EnemySpecialField = GameObject.FindGameObjectWithTag("EnemySpecial");
		EnmyHand=GameObject.FindGameObjectWithTag("EnemyHand");
        PlayerHand = GameObject.FindGameObjectWithTag("PlayerHand");
        panel = this.transform.GetChild(2).gameObject;
		
    }
    private void Start()
    {
        Message.text = "";
        panel.gameObject.SetActive(false);
        Attack = panel.transform.GetChild(0).GetComponent<Button>();
        Defence = panel.transform.GetChild(1).GetComponent<Button>();
        Attack.onClick.AddListener(() => {
            //Debug.Log(CardsDB.AllCardsInfo[panel.transform.parent.gameObject.name].CardName);
            if(CardsDB.PlayerOne.MyDeck.PlayerDeck.ContainsKey(panel.transform.parent.gameObject.name)==true && MonsterField.transform.childCount < 5)
            {

                if (CardsDB.PlayerOne.MyDeck.PlayerDeck[this.gameObject.name].GetType() == typeof(Monsters) || CardsDB.PlayerOne.MyDeck.PlayerDeck[this.gameObject.name].GetType() == typeof(EffectedMonsters))
                {
                    if (this.transform.parent.transform == MonsterField.transform && phaseControl.BPTrue.gameObject.activeSelf == false)
                    {
                        SetScaleAndRotation(0);
                        ((Monsters)CardsDB.PlayerOne.MyDeck.PlayerDeck[this.gameObject.name]).AttacDefencState = !((Monsters)CardsDB.PlayerOne.MyDeck.PlayerDeck[this.gameObject.name]).AttacDefencState;
                    }
                    else if (PhasesControl.summonCheck == true && phaseControl.BPTrue.gameObject.activeSelf == false )
                    {
                        if(((Monsters)CardsDB.PlayerOne.MyDeck.PlayerDeck[this.gameObject.name]).Rank>=5 && this.transform.parent == PlayerHand.transform && CardsDB.PlayerOne.IsTurn == true)
                        {
                            TributeCheck = true;
                            TributeCard = this.gameObject;
                            Message.text = "Send On Monster To Graveyard To Summon This Monster";
                        }
                        else if (this.transform.parent == PlayerHand.transform && CardsDB.PlayerOne.IsTurn == true)
                        {
                            print("HERE");
                            TributeCheck = false;
                            TributeCard = null;
                            panel.transform.parent.SetParent(MonsterField.transform);
                        }
                        PhasesControl.summonCheck = !PhasesControl.summonCheck;
                    }
                    SetScaleAndRotation(0);
                }
                else if(SpecialField.transform.childCount<5)
                {
                    if (panel.transform.parent.parent != EnmyHand.transform && CardsDB.PlayerOne.IsTurn == true)
                    {
                        panel.transform.parent.SetParent(SpecialField.transform);
                        ((Spells)CardsDB.PlayerOne.MyDeck.PlayerDeck[panel.transform.parent.gameObject.name]).InvokeFunction(CardsDB.PlayerOne.MyDeck.PlayerDeck[panel.transform.parent.gameObject.name].CardName);
                    }
                    this.GetComponent<RectTransform>().localScale = new Vector3(AttackScales.X, AttackScales.Y, AttackScales.Z);
                    this.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, 0);
                }
                
            }
            else if(CardsDB.PlayerTwo.MyDeck.PlayerDeck.ContainsKey(panel.transform.parent.gameObject.name) == true)
            {
                if (this.transform.parent.transform == EnemyMonsterField.transform && phaseControl.BPTrue.gameObject.activeSelf == false)
                {
                    Debug.Log("here");
                    panel.transform.parent = this.transform.parent;
                    this.GetComponent<RectTransform>().localScale = new Vector3(AttackScales.X, AttackScales.Y, AttackScales.Z);
                    this.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, 0);
                    panel.transform.parent = this.transform;
                    ((Monsters)CardsDB.PlayerOne.MyDeck.PlayerDeck[this.gameObject.name]).AttacDefencState = !((Monsters)CardsDB.PlayerOne.MyDeck.PlayerDeck[this.gameObject.name]).AttacDefencState;
                }
                else if ((CardsDB.PlayerTwo.MyDeck.PlayerDeck[panel.transform.parent.gameObject.name].GetType() == typeof(Monsters)|| CardsDB.PlayerTwo.MyDeck.PlayerDeck[panel.transform.parent.gameObject.name].GetType() == typeof(EffectedMonsters)) && EnemyMonsterField.transform.childCount<5)
                {
                    if (PhasesControl.summonCheck == true && phaseControl.BPTrue.gameObject.activeSelf == false)
                    {
                        if (((Monsters)CardsDB.PlayerTwo.MyDeck.PlayerDeck[panel.transform.parent.gameObject.name]).Rank >= 5 && panel.transform.parent.parent == EnmyHand.transform && CardsDB.PlayerTwo.IsTurn == true)
                        {
                            TributeCheck = true;
                            TributeCard = this.gameObject;
                            Message.text = "Send On Monster To Graveyard To Summon This Monster";
                        }
                        else if (panel.transform.parent.parent == EnmyHand.transform && CardsDB.PlayerTwo.IsTurn == true)
                        {
                            TributeCheck = false;
                            TributeCard = null;
                            panel.transform.parent.SetParent(EnemyMonsterField.transform);
                        }
                        this.GetComponent<RectTransform>().localScale = new Vector3(AttackScales.X, AttackScales.Y, AttackScales.Z);
                    }
                    
                }
                else if(EnemySpecialField.transform.childCount<5)
                {
                    if (panel.transform.parent.parent == EnmyHand.transform && CardsDB.PlayerTwo.IsTurn == true)
                    {
                        panel.transform.parent.SetParent(EnemySpecialField.transform);
                        
                    }
                    this.GetComponent<RectTransform>().localScale = new Vector3(AttackScales.X, AttackScales.Y, AttackScales.Z);
                    this.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, 0);
                }
            }
            //panel.transform.SetParent(this.transform.parent.parent);
            
            panel.gameObject.SetActive(false);

        });
        Defence.onClick.AddListener(() => {
            if (CardsDB.PlayerOne.MyDeck.PlayerDeck.ContainsKey(panel.transform.parent.gameObject.name) == true && MonsterField.transform.childCount < 5)
            {
                if (CardsDB.PlayerOne.MyDeck.PlayerDeck[panel.transform.parent.gameObject.name].GetType() == typeof(Monsters) || CardsDB.PlayerTwo.MyDeck.PlayerDeck[panel.transform.parent.gameObject.name].GetType() == typeof(EffectedMonsters) )
                {
                    if (this.transform.parent.transform == MonsterField.transform && phaseControl.BPTrue.gameObject.activeSelf == false)
                    {
                        SetScaleAndRotation(90);
                        ((Monsters)CardsDB.PlayerOne.MyDeck.PlayerDeck[this.gameObject.name]).AttacDefencState = !((Monsters)CardsDB.PlayerOne.MyDeck.PlayerDeck[this.gameObject.name]).AttacDefencState;
                    }

                    else if (PhasesControl.summonCheck == true && phaseControl.BPTrue.gameObject.activeSelf == false)
                    {
                        if (((Monsters)CardsDB.PlayerOne.MyDeck.PlayerDeck[this.gameObject.name]).Rank >= 5 && this.transform.parent == PlayerHand.transform && CardsDB.PlayerOne.IsTurn == true)
                        {
                            TributeCheck = true;
                            TributeCard = this.gameObject;
                            Message.text = "Send On Monster To Graveyard To Summon This Monster";
                        }
                        else if (this.transform.parent == PlayerHand.transform && CardsDB.PlayerOne.IsTurn == true)
                        {
                            print("HERE");
                            TributeCheck = false;
                            TributeCard = null;
                            panel.transform.parent.SetParent(MonsterField.transform);
                        }
                        PhasesControl.summonCheck = !PhasesControl.summonCheck;
                    }
                    //panel.transform.SetParent(this.transform.parent.parent);
                    SetScaleAndRotation(90);
                    PhasesControl.summonCheck = false;
                }
                else if (SpecialField.transform.childCount < 5)
                {
                    this.transform.GetChild(0).GetComponent<Image>().sprite = FlipedCard;
                    if (panel.transform.parent.parent == EnmyHand.transform && CardsDB.PlayerTwo.IsTurn == true)
                    {
                        panel.transform.parent.SetParent(EnemySpecialField.transform);
                    }
                    else if (panel.transform.parent.parent != EnmyHand.transform && CardsDB.PlayerOne.IsTurn == true)
                    {
                        panel.transform.parent.SetParent(SpecialField.transform);
                    }
                    //panel.transform.SetParent(this.transform.parent.parent);
                    this.GetComponent<RectTransform>().localScale = new Vector3(AttackScales.X, AttackScales.Y, AttackScales.Z);
                    panel.gameObject.SetActive(false);
                }
            }
            if (CardsDB.PlayerTwo.MyDeck.PlayerDeck.ContainsKey(panel.transform.parent.gameObject.name) == true)
            {
                if (CardsDB.PlayerTwo.MyDeck.PlayerDeck[panel.transform.parent.gameObject.name].GetType() == typeof(Monsters) || CardsDB.PlayerTwo.MyDeck.PlayerDeck[panel.transform.parent.gameObject.name].GetType() == typeof(EffectedMonsters))
                {
                    if (this.transform.parent.transform == EnemyMonsterField.transform && phaseControl.BPTrue.gameObject.activeSelf == false)
                    {
                        SetScaleAndRotation(90);
                        ((Monsters)CardsDB.PlayerOne.MyDeck.PlayerDeck[this.gameObject.name]).AttacDefencState = !((Monsters)CardsDB.PlayerOne.MyDeck.PlayerDeck[this.gameObject.name]).AttacDefencState;
                    }

                    else if (PhasesControl.summonCheck == true && phaseControl.BPTrue.gameObject.activeSelf == false)
                    {
                        if (((Monsters)CardsDB.PlayerTwo.MyDeck.PlayerDeck[this.gameObject.name]).Rank >= 5 && this.transform.parent == EnmyHand.transform && CardsDB.PlayerTwo.IsTurn == true)
                        {
                            TributeCheck = true;
                            TributeCard = this.gameObject;
                            Message.text = "Send On Monster To Graveyard To Summon This Monster";
                        }
                        else if (this.transform.parent == EnmyHand.transform && CardsDB.PlayerTwo.IsTurn == true)
                        {
                            print("HERE");
                            TributeCheck = false;
                            TributeCard = null;
                            panel.transform.parent.SetParent(MonsterField.transform);
                        }
                        PhasesControl.summonCheck = !PhasesControl.summonCheck;
                    }
                    //panel.transform.SetParent(this.transform.parent.parent);
                    panel.gameObject.SetActive(false);
                    SetScaleAndRotation(90);
                    PhasesControl.summonCheck = false;
                }
                else if (EnemySpecialField.transform.childCount < 5)
                {
                    this.transform.GetChild(0).GetComponent<Image>().sprite = FlipedCard;
                    if (panel.transform.parent.parent == EnmyHand.transform && CardsDB.PlayerTwo.IsTurn == true)
                    {
                        panel.transform.parent.SetParent(EnemySpecialField.transform);
                    }
                    else if (panel.transform.parent.parent != EnmyHand.transform && CardsDB.PlayerOne.IsTurn == true)
                    {
                        panel.transform.parent.SetParent(SpecialField.transform);
                    }
                    //panel.transform.SetParent(this.transform.parent.parent);
                    this.GetComponent<RectTransform>().localScale = new Vector3(AttackScales.X, AttackScales.Y, AttackScales.Z);
                    panel.gameObject.SetActive(false);
                }
            }
        });
    }
    
    private void Update()
    {   
        HideIfClickedOutside(panel);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (TributeCheck == true && this.transform.parent == MonsterField.transform)
        {
            Debug.Log("ok");
            TributeCard.transform.parent = MonsterField.transform;
            Destroy(this.gameObject);
            TributeCheck = false;
            Message.gameObject.SetActive(false);
        }
        else if(phaseControl.battle == false)
        {
            if (this.transform.parent == MonsterField.transform || this.transform.parent == EnemyMonsterField.transform)
            {
                panel.gameObject.SetActive(false);
            }

            //Vector3 pos = Input.mousePosition;
            Vector3 newVector = this.transform.position;
            if (this.transform.parent == PlayerHand.transform || this.transform.parent == MonsterField.transform || this.transform.parent == SpecialField.transform)
            {
                newVector.y += 12f;
            }
            else
            {
                newVector.y -= 12f;
            }

            if (panel.activeSelf == false)
            {
                panel.gameObject.SetActive(true);
            }
            panel.transform.position = newVector;
            panel.transform.SetParent(this.transform);


        }

    }
    private void SetScaleAndRotation(float RotationZ)
    {
        if(RotationZ==90f)
        {
            panel.gameObject.SetActive(false);
            panel.transform.parent = this.transform.parent;
            this.GetComponent<RectTransform>().localScale = new Vector3(DefenceScales.X, DefenceScales.Y, DefenceScales.Z);
            this.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, 90);
            panel.transform.parent = this.transform;
        }
        else
        {
            panel.gameObject.SetActive(false);
            panel.transform.parent = this.transform.parent;
            this.GetComponent<RectTransform>().localScale = new Vector3(AttackScales.X, AttackScales.Y, AttackScales.Z);
            this.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, 0);
            panel.transform.parent = this.transform;
        }
        
    }
    private void HideIfClickedOutside(GameObject panel1)
    {
        if (Input.GetMouseButton(0) && panel.activeSelf && !RectTransformUtility.RectangleContainsScreenPoint(panel.GetComponent<RectTransform>(),Input.mousePosition,Camera.main) )
        {
            panel1.SetActive(false);
        }
    }

  
}
