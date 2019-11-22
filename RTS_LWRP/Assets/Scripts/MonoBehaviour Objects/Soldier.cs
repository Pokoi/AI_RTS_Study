/*
 * File: Soldier.cs
 * File Created: Thursday, 14th November 2019 3:05:40 pm
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

public class Soldier : MonoBehaviour
{
    Unit unit;
    UnitType unitType;
    BattleDecisionMaker battleDecisionMaker;
    Locomotion locomotion;

    public void SetUnit(Unit unit) => this.unit = unit;
    public Unit GetUnit() => this.unit;

    public void SetUnitType(UnitType unitType) => this.unitType = unitType;
    public UnitType GetUnitType() => this.unitType;

    public CellData GetPosition() => unit.GetPosition();
    public int GetHealth()      => unit.GetUnitData().GetBehaviour().GetHealth();
    public int GetTotalHealth() => unit.GetUnitData().GetBehaviour().GetTotalHealth();
    public int GetDamageDone()  => unit.GetUnitData().GetBehaviour().GetDamageDone();

    private void Start() 
    {
        battleDecisionMaker = new BattleDecisionMaker(this);
        locomotion          = new Locomotion(transform);
    }

}
