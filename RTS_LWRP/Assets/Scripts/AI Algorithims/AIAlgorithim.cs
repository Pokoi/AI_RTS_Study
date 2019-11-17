/*
 * File: AIAlgorithim.cs
 * File Created: Tuesday, 29th October 2019 5:27:09 pm
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

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAlgorithim <T> where T : Action
{
   
    public Actions<T>    possibleActions;
    private byte [][]    utility;
    
    public AIAlgorithim(Actions<T> _possibleActions)
    {
        this.possibleActions    = _possibleActions;
        this.utility            = new byte[this.possibleActions.GetCount()][];
        for (int index = 0; index < this.utility.Length; ++index)
        {
            utility[index] = new byte[this.possibleActions.GetCount()];
        }

    }

    public virtual T GetNextAction () => default(T);

    public void UpdateUtility(T selfAction, T oponentAction, int score)
    {
        utility[oponentAction.Index][selfAction.Index] = (byte)score;
    }

    public virtual byte GetUtilityOf(T selfAction, T oponentAction)
    {
        return utility[oponentAction.Index][selfAction.Index];
    }
    public virtual byte [][] GetUtility() => this.utility; 

    public virtual void SetUtility(byte [][] utility) => this.utility = utility;
}
