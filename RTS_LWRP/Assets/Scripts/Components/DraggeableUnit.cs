/*
 * File: PlaceableItem.cs
 * File Created: Tuesday, 5th November 2019 3:39:23 pm
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

public class DraggeableUnit : MonoBehaviour
{
    FollowCursor followCursorComponent;
    public LayerMask layerMask;
    Cell lastCellCollidedWith;

    private void Start()
    {
        AddFollowCursorComponent();    
    }
    public void AddFollowCursorComponent()
    {
        if(this.followCursorComponent == null)
        {
            this.followCursorComponent = new FollowCursor(transform, layerMask);
        }

        DragPlaceable();
    }

    private void Update() 
    {
        if(Input.GetMouseButtonDown(0)&& followCursorComponent.GetFollowing())
        {
            DropPlaceable();
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        Cell otherCell = other.GetComponent<Cell>();
        if(otherCell)
        {
            this.lastCellCollidedWith = otherCell;
        }
    }

    private void DropPlaceable()
    {
        if(lastCellCollidedWith)
        {
            transform.position  = lastCellCollidedWith.transform.position;
            this.followCursorComponent.StopMoving();
            Soldier thisSoldier = transform.GetComponent<Soldier>();

            CellData    targetCellData  = lastCellCollidedWith.GetComponent<Cell>().GetBehaviour().GetCellData();
            Unit        placedUnit      = new Unit(targetCellData, new UnitData(thisSoldier.GetUnitType())); 

            thisSoldier.SetUnit(placedUnit);
            PlayerController.Get().AddUnitToFormation(placedUnit);
        }
    }

    private void DragPlaceable()
    {
        this.followCursorComponent.StartMoving();
    }
    
    
}
