using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actions <T> where T : Action
{
    int count;
    int maxUnits;
    T[] actions;

    public Actions(int maxUnits)
    {
        this.maxUnits   = maxUnits;
        this.count      = CalculateCount();
        this.actions    = new T[count];
        
        InitializeActions();
    }

    public T GetAt(int index) 
    { 
        return index < count ? actions [index] : null; 
    }

    public int GetIndex(T aA)
    {
        int i = 0; 
        for (; i < count && actions [i] != aA; ++i);
        return i;
    }

    public int GetCount() 
    { 
        return this.count; 
    }

    private int CalculateCount()
    {
        int size    = Board.Get().GetTotalCells();
        return (3 * size * (size -1 ) * maxUnits * System.Enum.GetNames(typeof(UnitType)).Length) >> 2;
    }

    private void InitializeActions()
    {
        
    }

    private bool ContainsAction(T aA)
    {
        return GetIndex(aA) < count;
    }
}

public class Action
{
    protected int index;
    public int Index 
    {
        get { return index; }
        set { index = value;}
    }

}

public class ArmyAction : Action 
{
    Unit[] units;

    public ArmyAction(int units)
    {
        this.units = new Unit[units];
    }

    public void AddUnitAt(Unit unit, int index)
    {
        units[index] = unit;
    }
    //init 
    
}
