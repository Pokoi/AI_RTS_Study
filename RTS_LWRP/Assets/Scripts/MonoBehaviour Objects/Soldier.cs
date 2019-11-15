using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    Unit unit;
    UnitType unitType;

    public void SetUnit(Unit unit) => this.unit = unit;
    public Unit GetUnit() => this.unit;

    public void SetUnitType(UnitType unitType) => this.unitType = unitType;
    public UnitType GetUnitType() => this.unitType;

}
