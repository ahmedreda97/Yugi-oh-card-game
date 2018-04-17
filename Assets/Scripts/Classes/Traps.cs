using UnityEngine;
using System.Collections;

public class Traps :Cards  {

    string type;
    public void functionality()
    {
        //waiting for ideas
    }
    public Traps(Traps t)
    {
        this.CardName = t.CardName;
        this.CardDesc = t.CardDesc;
        this.CardImage = t.CardImage;
        this.CardOwner = t.CardOwner;
        this.ID = t.ID;
    }
    public Traps()
    {
        
    }
    public override void destroyCard()
    {

    }
}
