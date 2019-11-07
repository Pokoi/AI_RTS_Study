/*
 * File: GameController.cs
 * File Created: Wednesday, 6th November 2019 10:39:46 am
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

public class GameController : MonoBehaviour
{
    public BoardBehaviour   boardBehaviour;
    public CameraBehaviour  cameraBehaviour;
    static GameController instance;


    public static GameController Get()  => instance;
    private void Awake()                => instance = this; 

    
    private void OnPlayerDecideFormation()
    {
        // Center the camera in the player board
        BoardData   boardData                   = BoardData.Get();
        Vector3     playerCenterCellPosition    = boardBehaviour.GetWorldPositionOfCell(boardData.GetPlayerCellsCenterCell());
        cameraBehaviour.CenterToPosition(playerCenterCellPosition); 
    }

    private void OnStartBattle()
    {
        // Center the camera in the board
        BoardData   boardData               = BoardData.Get();
        Vector3     boardCenterCellPosition = boardBehaviour.GetWorldPositionOfCell(boardData.GetBoardCenterCell());
        cameraBehaviour.CenterToPosition(boardCenterCellPosition); 
        
    }
}
