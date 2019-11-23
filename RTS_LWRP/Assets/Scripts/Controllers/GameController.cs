/*
 * File: GameController.cs
 * File Created: Wednesday, 6th November 2019 10:39:46 am
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

public class GameController : MonoBehaviour
{
    public BoardBehaviour   boardBehaviour;
    public CameraBehaviour  cameraBehaviour;
    public UnitsPool        unitsPool;
    public AIController     aiController;
    public PlayerController playerController;
    public ScoreController  scoreController;
    
    static GameController   instance;

    TeamData playerTeamData;
    TeamData AITeamData;
    TeamData AIDebugTeamData;
    bool callToEnd;

    XmlFormationVariables formationVariablesXMLData;
    XmlUCB1ObjectData UCB1ObjectData;
    XmlRegretMatchingObjectData RegretMatchingObjectData;

    public static GameController Get()   => instance;
    public TeamData GetPlayerTeamData()  => playerTeamData;
    public TeamData GetAITeamData()      => AITeamData;
    public TeamData GetAIDebugTeamData() => AIDebugTeamData;

    public void OnBattleEnds()
    {
        if(!callToEnd)
        {
            callToEnd = true;

            foreach (Soldier soldier in playerTeamData.GetSoldiers())
            {
                soldier.SetReadyToFight(false);
            }
            foreach (Soldier soldier in AITeamData.GetSoldiers())
            {
                soldier.SetReadyToFight(false);
            }

            // Calculate score
            int score = scoreController.CalculateScoreRelativeToAI(playerTeamData, AITeamData);

            Debug.Log(score);
            Debug.Log($"At {playerController.GetPlayerFormation().Index}");

            // Comunicate score to IA
            aiController.OnBattleEnd(playerController.GetPlayerFormation(), score); 

            
            AIController.FormationAlgorithims usedAlgorithim = aiController.GetFormationAlgorithim();
            switch (usedAlgorithim)
            {
                case AIController.FormationAlgorithims.UCB1:
                UCB1ObjectData.ConvertArray(aiController.GetTeamFormer().GetUtility());
                break;

                case AIController.FormationAlgorithims.RegretMatching:
                RegretMatching<ArmyAction> regretMatchingFormer = (RegretMatching<ArmyAction>) aiController.GetTeamFormer();
                RegretMatchingObjectData.ConvertArray(regretMatchingFormer.GetUtility());
                RegretMatchingObjectData.regret     = regretMatchingFormer.GetRegret();
                RegretMatchingObjectData.chance     = regretMatchingFormer.GetChance();
                break;

            }

            OnGameEnds();
        }
    }

    private void Awake() 
    {
        instance = this;
        unitsPool.SetMaxUnitsCount(aiController.GetMaxUnitsInTeam() << 1);
        playerTeamData  = new PlayerTeam();
        AITeamData      = new AITeam();
        AIDebugTeamData = new AIDebugTeam();
        UCB1ObjectData  = new XmlUCB1ObjectData();
        RegretMatchingObjectData = new XmlRegretMatchingObjectData();
    }

    private void Start() 
    {
        CreateFormationVariablesXml();
       // ReadAlgorithimXml();
        Invoke("OnPlayerDecideFormation", 3);
    }
    private void OnPlayerDecideFormation()
    {
        // Center the camera in the player board
        BoardData   boardData                   = BoardData.Get();
        Vector3     playerCenterCellPosition    = boardBehaviour.GetWorldPositionOfCell(boardData.GetPlayerCellsCenterCell());
        cameraBehaviour.CenterToPosition(playerCenterCellPosition); 

        // Hide all cells but player's
        HideCells();
    }

    public void OnStartBattle()
    {
        // Center the camera in the board
        BoardData   boardData               = BoardData.Get();
        Vector3     boardCenterCellPosition = boardBehaviour.GetWorldPositionOfCell(boardData.GetBoardCenterCell());
        cameraBehaviour.CenterToPosition(boardCenterCellPosition); 

        // Show the hiden cells
        ShowCells();

        uint j = 0;
        for(
            j = 0; 
            j < AIController.Get().GetPossibleActions().GetCount() && 
            AIController.Get().GetPossibleActions().GetAt((uint)j) != playerController.GetPlayerFormation(); 
            ++j
            );

        playerController.GetPlayerFormation().Index = j;

        AIController.Get().OnBattleStart();

        
        for(int i = 0; i < playerTeamData.GetSoldiers().Count; ++i)
        {
            playerTeamData.GetSoldiers()[i].ApplyBuff();
            AITeamData.GetSoldiers()[i].ApplyBuff();
        }
        

        //Activate player battle ai 
        foreach (Soldier soldier in playerTeamData.GetSoldiers())
        {
            soldier.SetReadyToFight(true);
        }
        foreach (Soldier soldier in AITeamData.GetSoldiers())
        {
            soldier.SetReadyToFight(true);
        }
    }

    private void OnGameEnds()
    {
       //Write the xml file
       UpdateXmL();
      
    }

    void UpdateXmL()
    {
        AIController.FormationAlgorithims usedAlgorithim = aiController.GetFormationAlgorithim();
        
        switch (usedAlgorithim)
        {
            case AIController.FormationAlgorithims.UCB1:
            XmlManaging.CreateFile<XmlUCB1ObjectData>(UCB1ObjectData, formationVariablesXMLData.UCB1ObjectDataPath);
            break;

            case AIController.FormationAlgorithims.RegretMatching:
            XmlManaging.CreateFile<XmlRegretMatchingObjectData>(RegretMatchingObjectData, formationVariablesXMLData.RegretMatchingObjectDataPath);
            break;

        }
    }

    private void ShowCells()
    {
        BoardData boardData = BoardData.Get();
        int maxColumns      = boardData.GetColumns();
        int maxRows         = boardData.GetRows();

        CellData firstNonPlayerCell = boardData.GetBoardCenterCell();
        CellData lastCellOnBoard    = boardData.GetCellDataAt(maxColumns-1, maxRows-1);
        --firstNonPlayerCell;
        
        while(firstNonPlayerCell != lastCellOnBoard)
        {
            firstNonPlayerCell.GetVisibleItem().Show();
            ++firstNonPlayerCell;
        }

        lastCellOnBoard.GetVisibleItem().Show();
    }
    private void HideCells()
    {
        BoardData boardData = BoardData.Get();
        int maxColumns      = boardData.GetColumns();
        int maxRows         = boardData.GetRows();

        CellData firstNonPlayerCell = boardData.GetBoardCenterCell();
        CellData lastCellOnBoard    = boardData.GetCellDataAt(maxColumns-1, maxRows-1);
        --firstNonPlayerCell;
        
        while(firstNonPlayerCell != lastCellOnBoard)
        {
            firstNonPlayerCell.GetVisibleItem().Hide();
            ++firstNonPlayerCell;
        }

        lastCellOnBoard.GetVisibleItem().Hide();
    }
    private void CreateFormationVariablesXml()
    {
        string UCB1XmlFilePath           = "Assets/XMLData/";
        string RegretMatchingXmlFilePath = "Assets/XMLData/";
        string formationVariablesPath    = "Assets/XMLData/";

        byte rows           = (byte) BoardData.Get().GetRows();
        byte columns        = (byte) BoardData.Get().GetColumns();
        byte maxUnitsInTeam = (byte) aiController.GetMaxUnitsInTeam();
        byte unitTypesCount = (byte) System.Enum.GetNames(typeof(UnitType)).Length;
        uint actionsCount   = aiController.GetPossibleActionsCount();

        UCB1XmlFilePath           += $"UCB1_{rows}x{columns}_{maxUnitsInTeam}_of_{unitTypesCount}.xml";
        RegretMatchingXmlFilePath += $"RM_{rows}x{columns}_{maxUnitsInTeam}_of_{unitTypesCount}.xml";
        formationVariablesPath    += $"FormationVariables_{rows}x{columns}_{maxUnitsInTeam}_of_{unitTypesCount}.xml";
        
        formationVariablesXMLData = new XmlFormationVariables
                                                            (
                                                                rows,
                                                                columns,
                                                                maxUnitsInTeam,
                                                                unitTypesCount,
                                                                actionsCount,
                                                                UCB1XmlFilePath,
                                                                RegretMatchingXmlFilePath
                                                            );

        XmlManaging.CreateFile<XmlFormationVariables>(formationVariablesXMLData, formationVariablesPath);
    }

    private void ReadAlgorithimXml()
    {
        AIController.FormationAlgorithims usedAlgorithim = aiController.GetFormationAlgorithim();
        switch (usedAlgorithim)
        {
            case AIController.FormationAlgorithims.UCB1:
            UCB1ObjectData = XmlManaging.ReadFile<XmlUCB1ObjectData>(formationVariablesXMLData.UCB1ObjectDataPath);
            aiController.GetTeamFormer().SetUtility(UCB1ObjectData.CastToArrayOfArray());
            break;

            case AIController.FormationAlgorithims.RegretMatching:
            RegretMatchingObjectData = XmlManaging.ReadFile<XmlRegretMatchingObjectData>(formationVariablesXMLData.RegretMatchingObjectDataPath);
            RegretMatching<ArmyAction> regretMatchingFormer = (RegretMatching<ArmyAction>) aiController.GetTeamFormer();
            regretMatchingFormer.SetUtility(RegretMatchingObjectData.CastToArrayOfArray());
            regretMatchingFormer.SetRegret(RegretMatchingObjectData.regret);
            regretMatchingFormer.SetChance(RegretMatchingObjectData.chance);
            break;

        }
       
    }
    
}
