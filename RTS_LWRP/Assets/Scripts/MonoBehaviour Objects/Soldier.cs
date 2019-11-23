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


using UnityEngine;

public class Soldier : MonoBehaviour
{
    Unit                unit;
    UnitType            unitType;
    BattleDecisionMaker battleDecisionMaker;
    Locomotion          locomotion;
    TeamData            team;

    public void     SetUnit(Unit unit)             => this.unit = unit;
    public void     SetUnitType(UnitType unitType) => this.unitType = unitType;
    public void     SetTeam(TeamData team)         => this.team = team;

    public Unit     GetUnit()        => this.unit;
    public UnitType GetUnitType()    => this.unitType;
    public CellData GetPosition()    => unit.GetPosition();
    public int      GetHealth()      => unit.GetUnitData().GetBehaviour().GetHealth();
    public int      GetTotalHealth() => unit.GetUnitData().GetBehaviour().GetTotalHealth();
    public int      GetDamageDone()  => unit.GetUnitData().GetBehaviour().GetDamageDone();

    public void SetReadyToFight(bool readyToFight)
    {
        if(readyToFight)
        {
            Battle();
        }
    }
    private void Start() 
    {
        battleDecisionMaker = new BattleDecisionMaker(this);
        locomotion          = new Locomotion(transform);
    }


    void Battle()
    {
        Soldier target = battleDecisionMaker.ChooseSoldierToAttack();
        locomotion.Move(target, unit.GetUnitData().GetBehaviour().GetAttackRange());
    }
    IEnumerator Fight(Soldier target)
    {
        while(true)
        {
            yield
        }
    }

}
