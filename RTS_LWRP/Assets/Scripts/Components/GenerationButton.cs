using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerationButton : MonoBehaviour
{
    public void GenerateMeleeUnit()
    {
        GenerateUnit(UnitType.melee);
    }

    public void GenerateRangedUnit()
    {
        GenerateUnit(UnitType.ranged);
    }

    public void GenerateTankUnit()
    {
        GenerateUnit(UnitType.tank);
    }

    public void GenerateHealerUnit()
    {
        GenerateUnit(UnitType.healer);
    }

    private void GenerateUnit(UnitType unitType)
    {
        GameObject unit = GameController.Get().unitsPool.GetUnitInstance(unitType);
        unit.SetActive(true);

    }
   
}
