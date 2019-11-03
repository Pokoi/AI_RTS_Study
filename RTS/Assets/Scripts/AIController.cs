using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController 
{
    [SerializeField] private int maxUnitsInTeam; 
    Actions<ArmyAction> possibleActions = new Actions<ArmyAction>(this.maxUnitsInTeam);
    
    
    AIAlgorithim<ArmyAction> teamFormer;



}


