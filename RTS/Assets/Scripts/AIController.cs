using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{

}

public struct ArmyActions
{
    int count;
    int maxUnits;
    ArmyAction[] actions;

    private void CalculateCount()
    {
        int size    = Board.Get().GetTotalCells();
        count       =  (3 * size * (size -1 ) * maxUnits * System.Enum.GetNames(typeof(UnitType)).Length) >> 2;
    }

    public ArmyAction GetAt(int index)
    {
        return index < count ? actions [index] : null;
    }

    public int GetIndex(ArmyAction aA)
    {
        int i = 0; 
        for (; i < count && actions [i] != aA; ++i);
        return i;
    }

}

public class ArmyAction
{
    Unit[] units;
    int index;

    public ArmyAction(int units)
    {
        this.units = new Unit[units];
    }
}
