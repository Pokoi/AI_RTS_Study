using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RPSAction
{
    Rock, Paper, Scissors
}
public class UCB1 : MonoBehaviour
{
    public int totalActions; // Rondas jugadas
    public int[] count; // Acciones jugadas de cada tipo
    public float[] score; // Acumulado de puntos conseguidos con cada acción
    public float[] UCB1scores; // Cálculo intermedio de cada acción de función a maximizar
    public int numActions; // Acciones disponibles
    public RPSAction lastAction; // Última acción realizada

    public Sprite[] sprites;
    SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        InitUCB1();
    }
    public void InitUCB1()
    {
        totalActions = 0;
        numActions = System.Enum.GetNames(typeof(RPSAction)).Length;
        count = new int[numActions];
        score = new float[numActions];
        UCB1scores = new float[numActions];
        for (int i=0; i <numActions; i++)
        {
            count[i] = 0;
            score[i] = 0;
        }
    }
    public RPSAction Play()
    {
        RPSAction result = GetNextActionUCB1();
        sr.sprite = sprites[(int)result];
        return result;
    }

    private RPSAction GetNextActionUCB1()
    {
        int i, best;
        float bestScore;
        float tempScore;
        // Las primeras numActions veces solo va probando cada una de las acciones.
        // Sería mejor hacer un Random de todas las acciones que aún no ha probado.
        for (i = 0; i <numActions; i++)
        {
            if (count[i] == 0)
            {
                lastAction = (RPSAction)i;
                return lastAction;
            }
        }
        // Si ya ha probado todas las acciones entonces aplica UCB1.
        best = -1;
        bestScore = int.MinValue;
        for(i = 0; i <numActions; i++)
        {
            tempScore = UCB1(score[i] / count[i], count[i], totalActions);
            UCB1scores[i] = tempScore;
            if (tempScore > bestScore)
            {
                best = i;
                bestScore = tempScore;
            }
        }
        lastAction = (RPSAction)best;
        return lastAction;    
    }
    private float UCB1(float averageUtility, float count, float totalActions)
    {
        return averageUtility + Mathf.Sqrt(2 + Mathf.Log10(totalActions) / count);
    }

    public void TellOponentAction (RPSAction action)
    {
        totalActions++;
        float utility;
        utility = GetUtility(lastAction, action);
        score[(int)lastAction] += utility;
        count[(int)lastAction]++;
    }

    public float GetUtility(RPSAction lastAction, RPSAction action)
    {
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}