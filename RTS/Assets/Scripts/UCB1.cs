using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UCB1 <T> : AIAlgorithim <T>
{
    public int playedRounds;    
    public int [] timesPlayedByAction;        
    public float [] scoreByAction;  
    public float [] UCB1scoresByAction; 
    public int totalAvailableActions;   
    public T lastAction; 


    UCB1()
    {
        InitUCB1();
    }

    public void InitUCB1(int totalAvailableActions)
    {
        playedRounds                = 0;
        this.totalAvailableActions  = totalAvailableActions;
        timesPlayedByAction         = new int   [totalAvailableActions];
        scoreByAction               = new float [totalAvailableActions];
        UCB1scoresByAction          = new float [totalAvailableActions];
    }

    public T Play()
    {
        return GetNextAction();
    }

    public override T GetNextAction()
    {
        int i, best;
        float bestScore;
        float tempScore;
        // Las primeras numActions veces solo va probando cada una de las acciones.
        // Sería mejor hacer un Random de todas las acciones que aún no ha probado.
        for (i = 0; i <totalAvailableActions; i++)
        {
            if (timesPlayedByAction[i] == 0)
            {
                lastAction = (T)i;
                return lastAction;
            }
        }
        // Si ya ha probado todas las acciones entonces aplica UCB1.
        best = -1;
        bestScore = int.MinValue;
        for(i = 0; i <totalAvailableActions; i++)
        {
            tempScore = GetUCB1(scoreByAction[i] / timesPlayedByAction[i], timesPlayedByAction[i], playedRounds);
            UCB1scoresByAction[i] = tempScore;
            if (tempScore > bestScore)
            {
                best = i;
                bestScore = tempScore;
            }
        }
        lastAction = (T)best;
        return lastAction;    
    }
    private float GetUCB1(float averageUtility, float count, float totalActions)
    {
        return averageUtility + Mathf.Sqrt(2 + Mathf.Log10(totalActions) / count);
    }

    public void TellOponentAction (T action)
    {
        playedRounds++;
        float utility;
        utility = GetUtility(lastAction, action);
        scoreByAction[(int)lastAction] += utility;
        timesPlayedByAction[(int)lastAction]++;
    }

    public float GetUtility(T lastAction, T action)
    {
         /* 
        float utility = 0;
        if (lastAction == RPSAction.Rock && action == RPSAction.Paper)
            utility--;
        else if (lastAction == RPSAction.Rock && action == RPSAction.Scissors)
            utility++;
        else if (lastAction == RPSAction.Paper && action == RPSAction.Scissors)
            utility--;
        else if (lastAction == RPSAction.Paper && action == RPSAction.Rock)
            utility++;
        else if (lastAction == RPSAction.Scissors && action == RPSAction.Rock)
            utility--;
        else if (lastAction == RPSAction.Scissors && action == RPSAction.Paper)
            utility++;
        return utility;
        */

        return 1;
    }
}