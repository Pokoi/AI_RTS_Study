/*
 * File: Board.cs
 * File Created: Tuesday, 29th October 2019 4:32:55 pm
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

public class BoardData
{
    byte rows = 0, columns = 0;
    CellData [,] cells;
    static BoardData instance;

    private BoardData(byte rows, byte columns)
    {
        rows    = this.rows;
        columns = this.columns;
        cells   = new CellData[rows, columns];
        
        InitializeCells();
    }
    public static BoardData Create(byte rows, byte columns)
    {   
        if(instance == null)
        {
            instance = new BoardData(rows, columns);
        }

        return instance;
    }
    
    public static BoardData Get()
    {
        return instance;
    }
    public CellData GetCellAt (int x, int y) 
    { 
        return cells [x,y];
    }

    public int GetTotalCells() 
    { 
        return rows * columns;
    }

    public int GetRows()
    {
        return this.rows;
    }

    public int GetColumns()
    {
        return this.columns;
    }

    private void InitializeCells()
    {
        byte r, c;
        r = c = 0;

        for (int i = 0; i < cells.Length; ++i)
        {
            this.cells[r, c] = new CellData(r, c);
            
            ++c;
            if(c == columns)
            {
                c = 0;
                ++r;
            }
        }
    }
}

public class CellData
{
    Vector2 position; 

    public CellData (byte x, byte y)
    {
        this.position   = new Vector2 (x, y);
    }

    public static bool operator == (CellData thisCell, CellData otherCell)
    {
        return thisCell.GetPosition() == otherCell.GetPosition();
    }

    public static bool operator != (CellData thisCell, CellData otherCell)
    {
        return !(thisCell == otherCell);
    }    
    public byte GetX()
    { 
        return (byte) position.x;
    }
    public byte GetY()
    { 
        return (byte) position.y;
    }
    public Vector2 GetPosition() 
    { 
        return this.position;
    }
    
}