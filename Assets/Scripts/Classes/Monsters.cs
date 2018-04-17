using UnityEngine;
using System.Collections;
using System;

public class Monsters : Cards
{
     bool _attacDefencState;
	private  int _attackPoints;
    private  int _defencePoints;
    private int _tempattackPoints;
    private int _tempdefencePoints;
    int _rank;
    string _type;
    string _attribute;
    string _race;
    public int DefencePoints

    {
        get
        {
            return _defencePoints;
        }

        set
        {
            _defencePoints = value;
        }
    }

    public int AttackPoints
    {
        get
        {
            return _attackPoints;
        }

        set
        {
            _attackPoints = value;
        }
    }

    public bool AttacDefencState
    {
        get
        {
            return _attacDefencState;
        }

        set
        {
            _attacDefencState = value;
        }
    }

    public int Rank
    {
        get
        {
            return _rank;
        }

        set
        {
            _rank = value;
        }
    }

    public string Type
    {
        get
        {
            return _type;
        }

        set
        {
            _type = value;
        }
    }

    public string Attribute
    {
        get
        {
            return _attribute;
        }

        set
        {
            _attribute = value;
        }
    }

    public string Race
    {
        get
        {
            return _race;
        }

        set
        {
            _race = value;
        }
    }
   

    public int TempdefencePoints
    {
        get
        {
            return _tempdefencePoints;
        }

        set
        {
            _tempdefencePoints = value;
        }
    }

    public int TempattackPoints
    {
        get
        {
            return _tempattackPoints;
        }

        set
        {
            _tempattackPoints = value;
        }
    }

    public Monsters(string name,string Desc,Sprite image)
    {
        CardName = name;
        CardDesc = Desc;
        CardImage = image;
    }

    public Monsters()
    {
        
    }
    public Monsters(Monsters m)
    {
        this.AttackPoints = m.AttackPoints;
        this.ID=m.ID;
        this.CardImage = m.CardImage;
        this.CardName=m.CardName ;
        this.CardDesc=m.CardDesc ;
        this.Rank=m.Rank ;
        this.Attribute=m.Attribute;
        this.Race= m.Race ;
        this.DefencePoints=m.DefencePoints ;
        this.AttacDefencState = m.AttacDefencState;
        this.TempattackPoints = m.TempattackPoints;
        this.TempdefencePoints = m.TempdefencePoints;
    }
    public override void destroyCard()
    {


    }
	public Monsters Attack(Monsters mon, Player attk, Player def) // if a card is attacked we also need to know the owners of the attacking and the attacked monsters
    {
		
        if (mon.AttacDefencState == true)// If The monster attacked is on attack state
        {
			if (this._tempattackPoints == mon.TempdefencePoints)
			{
				Debug.Log("5");

			}
			if (this.TempattackPoints > mon.TempdefencePoints)
			{
				int points =def.get_LifePoints()- (this.TempattackPoints - mon.TempdefencePoints);
				def.set_LifePoints (points);
				Debug.Log ("3");
				return mon;

			}
			if (this.TempattackPoints < mon.TempdefencePoints)
			{
				int points = attk.get_LifePoints() - (mon.TempdefencePoints - this.TempattackPoints);
				attk.set_LifePoints (points);
				Debug.Log ("4");
				return this;
			}


        }
       else                        // If The monster attacked is on defense state
        {
			if (this.TempattackPoints == mon.TempdefencePoints)
			{


			}
			if (this.TempattackPoints > mon.TempattackPoints)
			{

				int points =def.get_LifePoints()- (this.TempattackPoints - mon.TempattackPoints);
				def.set_LifePoints (points);
				return mon;


			}
			else if(this.TempattackPoints<mon.TempattackPoints)
			{
				
				int points = attk.get_LifePoints() - (mon.TempattackPoints - this.TempattackPoints);
				attk.set_LifePoints (points);
				return this;
			}
          

        }

		return null;


    }
    public void Attack(Player ply)// when Player is attacked directly(THERE IS MUST BE NO MONSTERS IN HIS FIELD)
    {
		int points=ply.get_LifePoints() - this.AttackPoints;
		ply.set_LifePoints (points);


    }
}
