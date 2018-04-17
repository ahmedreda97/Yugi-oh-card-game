using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;


public class EffectedMonsters: Monsters
{
    private String effect;
    public EffectedMonsters(EffectedMonsters m)
    {
        this.AttackPoints = m.AttackPoints;
        this.ID = m.ID;
        this.CardImage = m.CardImage;
        this.CardName = m.CardName;
        this.CardDesc = m.CardDesc;
        this.Rank = m.Rank;
        this.Attribute = m.Attribute;
        this.Race = m.Race;
        this.DefencePoints = m.DefencePoints;
        this.AttacDefencState = m.AttacDefencState;
        this.TempattackPoints = m.TempattackPoints;
        this.TempdefencePoints = m.TempdefencePoints;
    }
    public EffectedMonsters()
    {

    }
    public string Effect
    {
        get
        {
            return effect;
        }

        set
        {
            effect = value;
        }
    }

    public void EffectFunction()
    {

    }
}
