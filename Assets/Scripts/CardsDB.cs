using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;
using System.IO;
using System.Linq;
using System.Collections;
using UnityEngine.UI;

public class CardsDB : MonoBehaviour {
    
    public static Dictionary<string, Sprite> ImageInfo;
    public static  Dictionary<string, Cards> AllCardsInfo;
    public static Player PlayerOne;
    public static Player PlayerTwo;
    public Sprite DefultSprite;
    BaseFunctions functions;
    
    Cards c;
    private void Awake()
    {
        functions = this.gameObject.GetComponent<BaseFunctions>();
        BaseFunctions.defultSprite = DefultSprite;
        PlayerOne = new Player();
        PlayerTwo = new Player();
        
        //MonstersInfo = new Dictionary<string, Monsters>();
        //SpellInfo = new Dictionary<string, Spells>();
        //TrapInfo = new Dictionary<string, Traps>();
        AllCardsInfo = new Dictionary<string, Cards>();
        ImageInfo = new Dictionary<string, Sprite>();
        
        ReadMonsterData();
        ReadSpell_TrapsData();
        ReadImages();
        Spells.Base_Functions = functions;
        for (int i = 0; i < ImageInfo.Count; i++)
        {
            AllCardsInfo[ImageInfo.ElementAt(i).Key].CardImage = ImageInfo.ElementAt(i).Value;
        }
        //foreach (var item in AllCardsInfo)
        //{
        //    AllCardsInfo[item.Key].CardImage = ImageInfo[item.Key];
        //}
        
        Cards card=null;
        
        System.Random rand = new System.Random();
        int counter = AllCardsInfo.Count;
        for (int i = 0; i < counter; i++)
        {
            var RandomKey = rand.Next(AllCardsInfo.Count);
            
            if (PlayerTwo.MyDeck.PlayerDeck.ContainsKey(AllCardsInfo.ElementAt(RandomKey).Key) == true)
            {
                //print("here");
                counter++;
                continue;

            }
            card = AllCardsInfo.ElementAt(RandomKey).Value;
            //print(card.ID);
            if (card.GetType() == typeof(Monsters))
            {

                Monsters m = new Monsters((Monsters)card);
                c = m;
            }
            else if (card.GetType() == typeof(EffectedMonsters))
            {

                EffectedMonsters m = new EffectedMonsters((EffectedMonsters)card);
                c = m;
            }
            else if (card.GetType() == typeof(Spells))
            {
                Spells s = new Spells((Spells)card);
                
                s.FillingFunctionsMap();
                c = s;
                //print(c.ID + " s "+s.ID+" s "+card.ID);
            }
            else if (card.GetType() == typeof(Traps))
            {
                Traps t = new Traps((Traps)card);
                c = t;
                //print(c.ID+" t "+t.ID);
            }
            c.CardOwner = 2;
           
            PlayerTwo.RandomOrderPlayingCard.Add(c);

            PlayerTwo.MyDeck.PlayerDeck.Add(c.ID, c);

        }
        counter = AllCardsInfo.Count;
        c = null;
        foreach (var item in AllCardsInfo)
        {
            AllCardsInfo[item.Key].ID += '0';
        }
        for (int i = 0; i < counter; i++)
        {
            var RandomKey = rand.Next(AllCardsInfo.Count);
            //print(RandomKey );
            if (PlayerOne.MyDeck.PlayerDeck.ContainsKey(AllCardsInfo.ElementAt(RandomKey).Value.ID) == true)
            {
                //print("here");
                counter++;
                continue;

            }
            //print(RandomKey + " Player1After");
            card = AllCardsInfo.ElementAt(RandomKey).Value; ;
            //print(card.ID);
            if (card.GetType() == typeof(Monsters))
            {
                Monsters m = new Monsters((Monsters)card);
                c = m;
                // print(c.ID + " m " + m.ID);
            }
            else if (card.GetType() == typeof(EffectedMonsters))
            {

                EffectedMonsters m = new EffectedMonsters((EffectedMonsters)card);
                c = m;
            }
            else if (card.GetType() == typeof(Spells))
            {
                Spells s = new Spells((Spells)card);
                s.FillingFunctionsMap();
                c = s;
                //print(c.ID + " s " + s.ID + " s " + card.ID);
            }
            else if (card.GetType() == typeof(Traps))
            {
                Traps t = new Traps((Traps)card);
                c = (Cards)t;
                //print(c.ID + " t " + t.ID);
            }
            //print(card.ID);
            c.CardOwner = 1;
            PlayerOne.RandomOrderPlayingCard.Add(c);
            PlayerOne.MyDeck.PlayerDeck.Add(c.ID, c);
        }
    }
   
    void ReadImages()
    {
		string filePath =Application.dataPath + "/CardEditor/Card Pictures DB/";
        DirectoryInfo DI = new DirectoryInfo(filePath);

        FileInfo[] FI = DI.GetFiles();
        
        for (int i = 0; i < FI.Length; i += 2)
        {

            Texture2D tex = new Texture2D(1, 1);
			FileStream file = new FileStream( Application.dataPath + "/CardEditor/Card Pictures DB/" + FI[i].Name, FileMode.Open, FileAccess.Read);
            byte[] b = new byte[file.Length];
            file.Read(b, 0, int.Parse(file.Length.ToString()));

            tex.LoadImage(b);
            //Sprite s=
            Sprite s = Sprite.Create(tex, new Rect(0f, 0f, tex.width, tex.height), new Vector2(0.5f, 0.5f));
            string Name = "";
            for (int j = 0; j < FI[i].Name.Length; j++)
            {
                if (FI[i].Name[j] == '.' && FI[i].Name[j + 1] == 'p' && FI[i].Name[j + 2] == 'n' && FI[i].Name[j + 3] == 'g')
                {
                    break;
                }
                Name += FI[i].Name[j];
            }
            s.name = Name;
            //ImagesList.Add(s);
            if (!ImageInfo.ContainsKey(s.name))
            {
                //print(s.name);
                ImageInfo.Add(s.name, s);
            }
        }
    }
    public  void ReadMonsterData()
    {
        string conn = "URI=file:" + Application.dataPath + "/CardEditor/CDBLite.db"; //Path to database.
        IDbConnection con;
        con = (IDbConnection)new SqliteConnection(conn);
        con.Open(); //Open connection to the database.
        
        //newMonsters = new List<Monsters>();
        IDbCommand cmd = con.CreateCommand();
        string command="Select * From Monsters";
        cmd.CommandText = command;
        IDataReader rdr = cmd.ExecuteReader();
        while (rdr.Read())
        {

            Cards m=null;
            if(((string)rdr["Id"])[0]=='1')
            {
                m = new Monsters();
            }
            else
            {
                m = new EffectedMonsters();
            }
            m.ID = (string)rdr["Id"];
            m.CardName = (string)rdr["Name"];
            m.CardDesc = (string)rdr["Description"];
            ((Monsters)m).Rank = Convert.ToInt32((string)rdr["Level"]);
            ((Monsters)m).Attribute = (string)rdr["Attribute"];
            ((Monsters)m).Race = (string)rdr["Race"];
            ((Monsters)m).AttackPoints = Convert.ToInt32((string)rdr["Attack"]);
            ((Monsters)m).DefencePoints = Convert.ToInt32((string)rdr["Defense"]);
            ((Monsters)m).TempattackPoints = ((Monsters)m).AttackPoints;
            ((Monsters)m).TempdefencePoints = ((Monsters)m).DefencePoints;

            //print(m.TempattackPoints);
            //newMonsters.Add(m);
            if(!AllCardsInfo.ContainsKey(m.ID))
            {
                //Debug.Log(m.CardName);
                AllCardsInfo.Add(m.ID, m);
                //Debug.Log(MonstersInfo[m.ID].CardName);
            }
        }
        rdr.Close();
        rdr = null;
        cmd.Dispose();
        cmd = null;
        con.Close();
        con = null;

    }
    public void ReadSpell_TrapsData()
    {
        string conn = "URI=file:" + Application.dataPath + "/CardEditor/CDBLite.db"; //Path to database.
        IDbConnection con;
        con = (IDbConnection)new SqliteConnection(conn);
        con.Open(); //Open connection to the database.

        //newMonsters = new List<Monsters>();
        IDbCommand cmd = con.CreateCommand();
        string command = "Select * From Spells";
        cmd.CommandText = command;
        IDataReader rdr = cmd.ExecuteReader();
        while (rdr.Read())
        {
            string Id = (string)rdr["Id"];
            string name = (string)rdr["Name"];
            string Desc = (string)rdr["Description"];
            if(Id[0]=='3')
            {
                Spells newCard = new Spells();

                newCard.ID = (string)rdr["Id"];
                newCard.CardName = (string)rdr["Name"];
                newCard.CardDesc = (string)rdr["Description"];

                //newMonsters.Add(m);
                  if (!AllCardsInfo.ContainsKey(newCard.ID))
                    {
                        AllCardsInfo.Add(newCard.ID, newCard);
                    }
            }
            else if (Id[0] == '4')
            {
                Traps newCard = new Traps();

                newCard.ID = (string)rdr["Id"];
                newCard.CardName = (string)rdr["Name"];
                newCard.CardDesc = (string)rdr["Description"];

                //newMonsters.Add(m);
                if (!AllCardsInfo.ContainsKey(newCard.ID))
                {
                    AllCardsInfo.Add(newCard.ID, newCard);
                }
            }

        }
        rdr.Close();
        rdr = null;
        cmd.Dispose();
        cmd = null;
        con.Close();
        con = null;

    }

    
}


