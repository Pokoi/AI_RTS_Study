using UnityEngine;

public class Board
{
    byte rows, columns;
    Cell [,] cells;
    static Board instance;

    private Board(byte rows, byte columns)
    {
        rows    = this.rows;
        columns = this.columns;
        cells   = new Cell[rows, columns];
    }

    public static Board Create(byte rows, byte columns)
    {   
        if(instance == null)
        {
            instance = new Board(rows, columns);
        }

        return instance;
    }
    
    public static Board Get()
    {
        return instance;
    }
    public Cell GetCellAt (byte x, byte y) 
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
}

public struct Cell
{
    Vector2 position; 

    public Cell (byte x, byte y)
    {
        this.position   = new Vector2 (x, y);
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