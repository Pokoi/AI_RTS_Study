using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UCB1 <T> : AIAlgorithim <T> where T : Action
{
    int         playedRounds;    
    int     []  timesPlayedByAction;        
    float   []  scoreByAction;  
    float   []  UCB1scoresByAction; 
    uint         totalAvailableActions;   
    T           selfLastAction;

    public UCB1(Actions<T> _possibleActions) : base (_possibleActions)
    {
        playedRounds                = 0;
        this.totalAvailableActions  = this.possibleActions.GetCount();
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
                selfLastAction = possibleActions.GetAt(i);
                return selfLastAction;
            }
        }
        // Si ya ha probado todas las acciones entonces aplica UCB1.
        best        = -1;
        bestScore   = int.MinValue;
        for(i = 0; i <totalAvailableActions; i++)
        {
            tempScore  = GetUCB1(scoreByAction[i] / timesPlayedByAction[i], timesPlayedByAction[i], playedRounds);
            UCB1scoresByAction[i] = tempScore;
            if (tempScore > bestScore)
            {
                best = i;
                bestScore = tempScore;
            }
        }
        selfLastAction = possibleActions.GetAt(best);
        return selfLastAction;    
    }
    private float GetUCB1(float averageUtility, float count, float totalActions)
    {
        return averageUtility + Mathf.Sqrt(2 + Mathf.Log10(totalActions) / count);
    }

    public void TellOponentAction (T action)
    {
        playedRounds++;
        float utility;
        utility = GetUtility(selfLastAction, action);
        scoreByAction[selfLastAction.Index] += utility;
        timesPlayedByAction[selfLastAction.Index]++;
    }
}