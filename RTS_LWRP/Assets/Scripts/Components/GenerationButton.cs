using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerationButton : MonoBehaviour
{
    public void GenerateMeleeUnit()
    {
        if(AbleToGenerateMoreUnits())
        GenerateUnit(UnitType.melee);
    }

    public void GenerateRangedUnit()
    {
        if(AbleToGenerateMoreUnits())
        GenerateUnit(UnitType.ranged);
    }

    public void GenerateTankUnit()
    {
        if(AbleToGenerateMoreUnits())
        GenerateUnit(UnitType.tank);
    }

    public void GenerateHealerUnit()
    {
        if(AbleToGenerateMoreUnits())
        GenerateUnit(UnitType.healer);
    }

    private void GenerateUnit(UnitType unitType)
    {
        GameObject unit = GameController.Get().unitsPool.GetUnitInstance(unitType);
        unit.SetActive(true);

        DraggeableUnit draggeableUnitComponent = unit.GetComponent<DraggeableUnit>();
        
        if(draggeableUnitComponent == null)
        {
           draggeableUnitComponent = unit.AddComponent<DraggeableUnit>();
        }
        unit.GetComponent<Soldier>().SetUnitType(unitType);
        draggeableUnitComponent.AddFollowCursorComponent();
    }
   
   private bool AbleToGenerateMoreUnits()
   {
       return ( PlayerController.Get().GetChoosenUnitsCount() < AIController.Get().GetMaxUnitsInTeam()) && !(DraggeableUnit.draggingUnit);
   }
}
