﻿/*
 * File: BoardBehaviour.cs
 * File Created: Wednesday, 6th November 2019 8:38:41 am
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

public class BoardBehaviour : MonoBehaviour
{
    [SerializeField] byte boardRows = 0, boardColumns = 0;
    [SerializeField] GameObject cellPrefab = null;
    
    //Components
    BoardData boardData;
    Transform cachedTransform;

    private void Awake() 
    {
        cachedTransform             = transform;
        cachedTransform.position    = Vector3.zero;
        boardData                   = BoardData.Create(boardRows, boardColumns);
        InstantiateBoard();
    }

    private void InstantiateBoard()
    {
        int currentRow      = 0;
        int currentColumn   = 0;
        int totalCells      = boardData.GetTotalCells();

        for (int index = 0; index < totalCells; ++index)
        {
            InstantiateCell(currentRow, currentColumn);

            ++currentColumn;
            if(currentColumn == currentRow)
            {
                currentColumn = 0;
                ++currentRow;
            }
        }
    }

    private void InstantiateCell(int localX, int localZ)
    {
        Vector3     boardPosition           = cachedTransform.position;
        GameObject  newCellInstance         = Instantiate(cellPrefab, boardPosition, Quaternion.identity);
        Vector3     cellSize                = newCellInstance.GetComponent<CellBehaviour>().GetColliderSize();
        Vector3     newCellInstancePosition = new Vector3   (   localX + cellSize.x + boardPosition.x, 
                                                                boardPosition.y, 
                                                                localZ + cellSize.z + boardPosition.z
                                                            );
        
        newCellInstance.transform.position  = newCellInstancePosition;
        newCellInstance.GetComponent<CellBehaviour>().CellDataInitializer(localX, localZ);
    }
}
