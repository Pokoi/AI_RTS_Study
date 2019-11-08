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

public class UnitsPool 
{
    public GameObject tankPrefab, rangedPrefab, meleePrefab, healerPrefab;
    List<GameObject> units;
   
   public UnitsPool
    (
       int maxUnitsCount, 
       GameObject meleePrefab,
       GameObject rangedPrefab, 
       GameObject healerPrefab, 
       GameObject tankPrefab
    )
    {
        this.meleePrefab = meleePrefab;
        this.rangedPrefab = rangedPrefab;
        this.healerPrefab = healerPrefab;
        this.tankPrefab = tankPrefab;
        units = new List<GameObject>();
        FillPool(maxUnitsCount);

    }

   void FillPool(int maxUnitsCount)
   {
       int maxT = System.Enum.GetNames(typeof(UnitType)).Length;
       
       for(int t = 0; t < maxT; ++t)
       {
           for(int i = 0; i < maxUnitsCount; ++i)
           {
               UnitData unitData = new UnitData((UnitType)t, null); 
           }

       }
                
   }
}
