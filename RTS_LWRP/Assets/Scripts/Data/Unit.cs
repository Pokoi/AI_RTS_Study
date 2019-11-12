using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit
{
    UnitData unitData;
    CellData position;


    public CellData GetPosition() => this.position;
    public void SetPosition(CellData position) => this.position = position;

    public Unit(CellData cd, UnitData ud)
    {
        this.unitData = ud;
        this.position = cd;
    }
    
}
