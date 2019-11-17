using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Numerics;

public class test : MonoBehaviour
{
   

    // Start is called before the first frame update
    void Start()
    {
        ArrayBidimensional AB = new ArrayBidimensional();
        XmlManaging.CreateFile<ArrayBidimensional>(AB, "test_arrayBD.xml");

        ArrayBidimensional BA = XmlManaging.ReadFile<ArrayBidimensional>("test_arrayBD.xml");
        Debug.Log("");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick(UnitType type)
    {
        //DoSomething
    }

    public void OnClick()
    {}
}

public class ArrayBidimensional
{
    public int [][] array;
    public string name;
    public int peso;
    public ArrayBidimensional()
    {
        array = new int [10][];
        for (int i = 0; i< array.Length; ++i)
        {
            array[i] = new int[20];
        }
    }
}
