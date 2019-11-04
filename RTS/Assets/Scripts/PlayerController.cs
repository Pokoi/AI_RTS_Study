using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController
{
    int score;
    static PlayerController instance;
    
    private PlayerController()
    {
        this.score = 0;
    }

    public PlayerController Create()
    {
        if(instance == null)
        {
            instance = new PlayerController();
        }

        return instance;
    }

    public static PlayerController Get()
    {
        return instance;
    }

    public void UpdateScore(int score)
    {
        this.score += score;
    }

}
