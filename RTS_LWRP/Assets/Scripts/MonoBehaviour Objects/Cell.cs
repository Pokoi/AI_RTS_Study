﻿/*
 * File: Cell.cs
 * File Created: Saturday, 23rd November 2019 3:19:10 am
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


using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Collider))]
public class Cell : MonoBehaviour
{
    CellBehaviour           cellBehaviour;
    public List<Transform>  visualTransforms;

    public CellBehaviour GetBehaviour() => cellBehaviour;
    void                 Awake()        => cellBehaviour = new CellBehaviour(GetComponent<Collider>());

    private void Start() 
    {
        cellBehaviour.GetCellData().GetVisibleItem().SetVisibleItems(visualTransforms);
        cellBehaviour.GetCellData().GetVisibleItem().SetCollider(GetComponent<Collider>());     
    }
    

}
