/*
 * File: UnitsPool.cs
 * File Created: Friday, 8th November 2019 3:52:39 pm
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

public class UnitsPool : MonoBehaviour
{
    public GameObject tankPrefab, rangedPrefab, meleePrefab, healerPrefab;
    List<GameObject> meleeUnits;
    List<GameObject> tankUnits;
    List<GameObject> rangedUnits;
    List<GameObject> healerUnits;
    int maxUnitsCount;
   
    public void SetMaxUnitsCount(int maxUnitsCount) => this.maxUnitsCount = maxUnitsCount;
    public GameObject GetUnitInstance(UnitType unitType)
    {
        GameObject to_return = null;
         
        switch(unitType)
        {
            case UnitType.ranged:
            to_return = GetFromPool(rangedUnits);
            break;
            case UnitType.melee:
            to_return = GetFromPool(meleeUnits);
            break;
            case UnitType.healer:
            to_return = GetFromPool(healerUnits);
            break;
            case UnitType.tank:
            to_return = GetFromPool(tankUnits);
            break;
        }

        return to_return;
    }
    private void Awake() 
    {
        meleeUnits  = new List<GameObject>(); 
        tankUnits   = new List<GameObject>(); 
        rangedUnits = new List<GameObject>(); 
        healerUnits = new List<GameObject>();    
    }
    
    private void Start() 
    {
        FillPool(maxUnitsCount);
    }
    
    void FillPool(int maxUnitsCount)
    {
        int maxT = System.Enum.GetNames(typeof(UnitType)).Length;
        
        for(int t = 0; t < maxT; ++t)
        {
            for(int i = 0; i < maxUnitsCount << 1; ++i)
            {
                UnitData unitData = new UnitData((UnitType)t, null); 
            }

        }               
    }

    GameObject GetFromPool(List<GameObject> pool)
    {
        foreach(GameObject go in pool)
        {
            if (!go.activeSelf) return go;
        }

        return null;
    }
}
