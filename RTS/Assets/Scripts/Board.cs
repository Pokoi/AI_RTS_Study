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
        
        InitializeCells();
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
    public Cell GetCellAt (int x, int y) 
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
            this.cells[r, c] = new Cell(r, c);
            
            ++c;
            if(c == columns)
            {
                c = 0;
                ++r;
            }
        }
    }
}

public class Cell
{
    Vector2 position; 

    public Cell (byte x, byte y)
    {
        this.position   = new Vector2 (x, y);
    }

    public static bool operator == (Cell thisCell, Cell otherCell)
    {
        return thisCell.GetPosition() == otherCell.GetPosition();
    }

    public static bool operator != (Cell thisCell, Cell otherCell)
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