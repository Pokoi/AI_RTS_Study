/*
 * File: Action.cs
 * File Created: Friday, 1st November 2019 10:31:59 am
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
using System.Numerics;

public class Actions <T> where T : Action
{
    uint    count;
    int     maxUnits;
    T[]     actions;

    public Actions(int maxUnits)
    {
        this.maxUnits   = maxUnits;
        this.count      = (uint) CalculateCount();
        this.actions    = new T[count];
        
        CreateActions();
    }

    public T GetAt(int index) =>  index < count ? actions [index] : null; 

    public int GetIndex(T aA)
    {
        int i = 0; 
        for (; i < count && actions [i] != aA; ++i);
        return i;
    }

    public uint GetCount() => this.count;  

    private BigInteger CalculateCount()
    {
        uint size        = (uint) BoardData.Get().GetTotalCells();
        uint typesCount  = (uint) System.Enum.GetNames(typeof(UnitType)).Length;
        uint n           = size * typesCount; 

        return (factorial (n)) / (factorial (n-(uint)maxUnits) * factorial ((uint)maxUnits)) - factorial(typesCount);
    }

    private void CreateActions()
    { 
        Unit [] possibleUnits = CreatePossibleUnits();
        InitializeActions(possibleUnits, maxUnits);
    }

    private Unit[] CreatePossibleUnits()
    {
        int maxX = BoardData.Get().GetRows();
        int maxY = BoardData.Get().GetColumns();
        int maxT = System.Enum.GetNames(typeof(UnitType)).Length;

        Unit [] possibleUnits = new Unit[maxX * maxY * maxT];

        for (int x = 0; x < maxX; ++x)
        {
            for (int y = 0; y < maxY; ++y)
            {
                for(int t = 0; t < maxT; ++t)
                {
                    possibleUnits[x+y+t] = new Unit((UnitType)t, BoardData.Get().GetCellDataAt(x, y));
                }
            }
        }

        return possibleUnits;
    }
    private void InitializeActions(Unit[] array, int m)
    {
        Unit [] result  = new Unit [m];
        int     index   = 0;

        foreach (Unit[] u in UnitCombinations (m, array))
        {
            for (int i = 0; i < m; ++i)
            {
                result [i] = u[i]; 
            }
            
            if(HaveDifferentPossitions(result))
            {
                this.actions [index] = new ArmyAction(result) as T;
                ++index;
            }
        }
    }

    IEnumerable <Unit []> UnitCombinations  (int size, Unit[] n)
    {
        Unit [] result      = new Unit [size];
        Stack<int> stack    = new Stack<int>(size);
        stack.Push(0);

        while(stack.Count > 0)
        {
            int index       = stack.Count - 1;
            int valueIndex  = stack.Pop();

            while (valueIndex < n.Length)
            {
                result[index++] = n[valueIndex++];
                stack.Push(valueIndex);
                if(index != size) continue;
                yield return result.Clone() as Unit[];
                break;
            }
        }
    }

   private BigInteger factorial(ulong number)
    {
        BigInteger result = number;
        for (uint i = 1; i < number; ++i)
        {
            result *= i;
        } 
        return result;
    }

    private bool HaveDifferentPossitions(Unit[] u)
    {
        for(int i = 0; i < (u.Length - 1); ++i)
        {
            for (int j = i + 1; j < u.Length; ++j)
            {
                if (u[i].GetPosition() == u[j].GetPosition())
                {
                  return false;  
                }  
            }
        }
        
        return true;
    }
}

public class Action
{
    protected int index;
    public int Index 
    {
        get { return index; }
        set { index = value;}
    }

}

public class ArmyAction : Action 
{
    Unit[] units;

    public ArmyAction(int unitsCount)           => this.units = new Unit[unitsCount];
    public ArmyAction (Unit[] units)            =>  this.units = units;
    public void AddUnitAt(Unit unit, int index) => units[index] = unit;
    
}
