/*
 * File: AIController.cs
 * File Created: Wednesday, 30th October 2019 4:03:51 pm
 * ––––––––––––––––––––––––
 * Author: Jesus Fermin, 'Pokoi', Villar  (hello@pokoidev.com)
 * ––––––––––––––––––––––––
 * MIT License
 * 
 * Copyright (c) 2019 Pokoidev
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy of
 * this software and associated documentation files (the "Software"), to deal in
 * the Software without restriction, including without limitation the rights to
 * use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
 * of the Software, and to permit persons to whom the Software is furnished to do
 * so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public enum FormationAlgorithims {UCB1, RegretMatching};
    public FormationAlgorithims formationAlgorithim;
    public int              maxUnitsInTeam; 
    public BoardBehaviour   boardBehaviour;
    
    static AIController     instance;

    Actions<ArmyAction>     possibleActions;
    ArmyAction              selfFormation; 
    Strips                  decisionMaker;
    AIAlgorithim<ArmyAction> teamFormer = null;



    int score;    


    public static AIController Get()                    => instance;
    public int GetMaxUnitsInTeam()                      => maxUnitsInTeam;
    public AIAlgorithim<ArmyAction> GetTeamFormer()     => teamFormer;
    public uint GetPossibleActionsCount()               => possibleActions.GetCount();
    public FormationAlgorithims GetFormationAlgorithim()  => formationAlgorithim;
    
    public AIController Create()
    {
        if(instance == null)
        {
            instance = this;
        }

        return instance;
    }
    
    public void OnBattleEnd(ArmyAction oponentFormation, int score)
    {
        teamFormer.UpdateUtility(selfFormation, oponentFormation, score);
        int utility = teamFormer.GetUtilityOf(selfFormation, oponentFormation);

        if (utility < 0)
        {
            UpdateScore(utility);
        }
        else if (utility == 0)
        {
            // Empate
        }
        else
        {
            PlayerController.Get().UpdateScore(utility);
        }
        
        teamFormer.UpdateValues(oponentFormation);
    }

    public void InterpreateFormation()
    {
        ArmyAction formation  = selfFormation;
        foreach (Unit unit in formation.GetUnits())
        {
            int cellDataRelativeX = BoardData.Get().GetColumns() - unit.GetPosition().GetX() - 1;
            int cellDataRelativeY = BoardData.Get().GetRows()    - unit.GetPosition().GetY() - 1;

            CellData relativeCellData       = BoardData.Get().GetCellDataAt(cellDataRelativeX, cellDataRelativeY);
            Vector3  cellDataWorldPosition  = boardBehaviour.GetWorldPositionOfCell(relativeCellData);
        
            UnitType    AI_unitUnitType = unit.GetUnitData().GetType();
            GameObject  AI_unit         = GameController.Get().unitsPool.GetUnitInstance(AI_unitUnitType);
            
            AI_unit.SetActive(true);
            AI_unit.GetComponent<Soldier>().SetUnitType(AI_unitUnitType);
            AI_unit.GetComponent<DraggeableUnit>().enabled = false;
            
            AI_unit.transform.position = new Vector3 (cellDataWorldPosition.x, cellDataWorldPosition.y + 0.5f, cellDataWorldPosition.z);
        }
    }

    void Awake() 
    {
        Create();
        
        decisionMaker   = new Strips();
        CreateOperators();

        possibleActions = new Actions<ArmyAction>(maxUnitsInTeam);
        
        switch(formationAlgorithim)
        {
            case FormationAlgorithims.UCB1:
            teamFormer = new UCB1<ArmyAction>(possibleActions);
            break;

            case FormationAlgorithims.RegretMatching:
            teamFormer = new RegretMatching<ArmyAction>(possibleActions);
            break;
        }
        
        Invoke(decisionMaker.GetNextAction(), 0);
        
    }

    void ToChooseFormation()
    {
        selfFormation = teamFormer.Play();
    }

    void ToStartBattle()
    {

    }
    void UpdateScore (int newScore) => this.score += newScore;

    void ToUpdateXML()
    {
        
    }

    void ToUpdateScore()
    {
        
    }

    void CreateOperators()
    {
        List<OperatorStrips> operators      = new List<OperatorStrips>();

        PropertyStrips formationChoosen     = new PropertyStrips("formationChoosen");
        OperatorStrips toChooseFormation    = new OperatorStrips(null, formationChoosen,"ToChooseFormation");
        operators.Add(toChooseFormation);
        
        PropertyStrips battled              = new PropertyStrips("battled");
        OperatorStrips toBattle             = new OperatorStrips(formationChoosen, battled,"ToStartBattle");
        operators.Add(toBattle);
        
        PropertyStrips scoreUpdated         = new PropertyStrips("scoreUpdated");
        OperatorStrips toUpdateScore        = new OperatorStrips(battled, scoreUpdated,"ToUpdateScore");
        operators.Add(toUpdateScore);

        PropertyStrips goal                 = new PropertyStrips("Goal");
        OperatorStrips toUpdateXML          = new OperatorStrips(scoreUpdated, goal,"ToUpdateXML");
        operators.Add(toUpdateXML);

        foreach (OperatorStrips stripOperator in operators)
        {
            decisionMaker.AddOperator(stripOperator);
        }
    }


}


