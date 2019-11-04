using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    [SerializeField] private int maxUnitsInTeam = 0; 
    Actions<ArmyAction> possibleActions;
    UCB1<ArmyAction> teamFormer;
    ArmyAction selfFormation; 
    AIController instance;

    int score;    


    public AIController Get()
    {
        return instance;
    }
    
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

    AIController ()
    {
        possibleActions = new Actions<ArmyAction>(maxUnitsInTeam);
    }
    
    
    void ChooseFormation()
    {
        selfFormation = teamFormer.Play();
    }


    void UpdateScore (int newScore)
    {
        this.score += newScore;
    }

}


