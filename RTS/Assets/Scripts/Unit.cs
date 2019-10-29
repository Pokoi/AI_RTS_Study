using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit 
{
    private UnitType    type;
    private Cell        position;

    public Unit(UnitType type, Cell position)
    {
        this.type       = type;
        this.position   = position;
    }
}

public enum UnitType
{
    ranged, melee, flyer, ninja
}


