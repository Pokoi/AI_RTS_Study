using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Numerics;

public class Actions <T> where T : Action
{
    uint count;
    int maxUnits;
    T[] actions;

    public Actions(int maxUnits)
    {
        this.maxUnits   = maxUnits;
        this.count      = (uint) CalculateCount();
        this.actions    = new T[count];
        
        CreateActions();
    }

    public T GetAt(int index) 
    { 
        return index < count ? actions [index] : null; 
    }

    public int GetIndex(T aA)
    {
        int i = 0; 
        for (; i < count && actions [i] != aA; ++i);
        return i;
    }

    public uint GetCount() 
    { 
        return this.count; 
    }

    private BigInteger CalculateCount()
    {
        uint size        = (uint) Board.Get().GetTotalCells();
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
        int maxX = Board.Get().GetRows();
        int maxY = Board.Get().GetColumns();
        int maxT = System.Enum.GetNames(typeof(UnitType)).Length;

        Unit [] possibleUnits = new Unit[maxX * maxY * maxT];

        for (int x = 0; x < maxX; ++x)
        {
            for (int y = 0; y < maxY; ++y)
            {
                for(int t = 0; t < maxT; ++t)
                {
                    possibleUnits[x+y+t] = new Unit((UnitType)t, Board.Get().GetCellAt(x, y));
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

    public ArmyAction(int unitsCount)
    {
        this.units = new Unit[unitsCount];
    }

    public ArmyAction (Unit[] units)
    {
        this.units = units;
    }

    public void AddUnitAt(Unit unit, int index)
    {
        units[index] = unit;
    }
}
