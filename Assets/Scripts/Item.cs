using UnityEngine;

public abstract class BaseItem
{
        // Items can be purchased and sold - and have a cost
        public abstract void Purchase();
        public abstract void Sell();
        public abstract int GetCost();
}

public class Item : BaseItem
{
    public string itemName;
    public int cost;
    public int sellValue;

    public Item(string itemName, int cost, int sellValue)
    {
        this.itemName = itemName;
        this.cost = cost;
        this.sellValue = sellValue;
    }

    public override void Purchase()
    {
        Debug.Log("Purchased " + itemName + " for " + cost + " coins.");
    }

    public override void Sell()
    {
        Debug.Log("Sold " + itemName + " for " + sellValue + " coins.");
    }

    public override int GetCost()
    {
        return cost;
    }
}