using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAlgorithim <T> where T : Action
{
    protected Actions<T>  possibleActions;
    private int [,] utility;
    
    public AIAlgorithim(Actions<T> _possibleActions)
    {
        this.possibleActions    = _possibleActions;
        this.utility            = new int[this.possibleActions.GetCount(), this.possibleActions.GetCount()]; 
    }

    public virtual T GetNextAction () 
    {
        return default(T);
    }

    public void UpdateUtility(T selfAction, T oponentAction, int score)
    {
        utility[oponentAction.Index, selfAction.Index] = score;
    }

    protected virtual int GetUtility(T selfAction, T oponentAction)
    {
        return utility[oponentAction.Index, selfAction.Index];
    }
}
