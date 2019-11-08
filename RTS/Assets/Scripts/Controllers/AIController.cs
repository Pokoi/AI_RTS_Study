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
    [SerializeField] 
    private int         maxUnitsInTeam = 0; 
    Actions<ArmyAction> possibleActions;
    UCB1<ArmyAction>    teamFormer = null;
    ArmyAction          selfFormation; 
    AIController        instance;

    int score;    


    public AIController Get() => instance;
    public int GetMaxUnitsInTeam() => maxUnitsInTeam;
    
    public AIController Create()
    {
        if(instance == null)
        {
            instance = new AIController();
        }

        return instance;
    }
    
    public void OnBattleEnd(ArmyAction oponentFormation, int score)
    {
        teamFormer.UpdateUtility(selfFormation, oponentFormation, score);
        int utility = teamFormer.GetUtility(selfFormation, oponentFormation);

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

    AIController ()                 => possibleActions = new Actions<ArmyAction>(maxUnitsInTeam);
    void ChooseFormation()          =>  selfFormation = teamFormer.Play();
    void UpdateScore (int newScore) => this.score += newScore;

}


