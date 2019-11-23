/*
 * File: UnitBehaviour.cs
 * File Created: Friday, 8th November 2019 8:52:04 pm
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

public abstract class UnitBehaviour
{
    protected int   health;
    protected int   totalHealth;
    protected int   damage;
    protected float actionSpeed;
    protected int   damageDone;
    protected int   attackRange;


    public void Attack(Soldier target)
    {
        target.GetUnit().GetUnitData().GetBehaviour().ReceiveDamage(damage);
        damageDone += damage;
    }

    public void ReceiveDamage(int damage) => health -= damage;

    public int      GetHealth()      => health;
    public int      GetTotalHealth() => totalHealth;
    public int      GetDamageDone()  => damageDone;
    public int      GetAttackRange() => attackRange;
    public float    GetActionSpeed() => actionSpeed;

}

public class HealerSoldier : UnitBehaviour
{
    public HealerSoldier()
    {
      this.health = this.totalHealth = 400;
      this.damage = -10;
      this.actionSpeed = 0.3f;
      this.attackRange = 3;
    }
}

public class MeleeSoldier : UnitBehaviour
{
    public MeleeSoldier()
    {
        this.health = this.totalHealth = 500;
        this.damage = 10;
        this.actionSpeed = 0.45f;
        this.attackRange = 1;
    }
}

public class TankSoldier : UnitBehaviour
{
    public TankSoldier()
    {
        this.health = this.totalHealth = 1000;
        this.damage = 3;
        this.actionSpeed = 0.2f;
        this.attackRange = 1;
    }
}

public class RangedSoldier : UnitBehaviour
{
    public RangedSoldier()
    {
        this.health = this.totalHealth = 200;
        this.damage = 15;
        this.actionSpeed = 0.6f;
        this.attackRange = 2;
    }

}
