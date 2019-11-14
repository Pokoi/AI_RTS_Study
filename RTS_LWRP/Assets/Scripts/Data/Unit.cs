using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit
{
    UnitData unitData;
    CellData position;


    public CellData GetPosition() => this.position;
    public void SetPosition(CellData position) => this.position = position;

    public UnitData GetUnitData() => this.unitData;

    public Unit(CellData cd, UnitData ud)
    {
        this.unitData = ud;
        this.position = cd;
    }

    static public bool operator == (Unit thisCell, Unit otherCell)
    {
        return thisCell.GetPosition() == otherCell.GetPosition() && thisCell.GetUnitData() == otherCell.GetUnitData();
    }

    static public bool operator != (Unit thisCell, Unit otherCell) => !(thisCell == otherCell);
    
    
}
