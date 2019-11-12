using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Collider))]
public class Cell : MonoBehaviour
{
    CellBehaviour   cellBehaviour;
    public List<Transform> visualTransforms;

    public CellBehaviour GetBehaviour() => cellBehaviour;

    void Awake()
    {
        cellBehaviour       = new CellBehaviour(GetComponent<Collider>());
    }

        private void Start() {
            cellBehaviour.GetCellData().GetVisibleItem().SetVisibleItems(visualTransforms);   
        }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) 
    {
        cellBehaviour.OnTriggerEnter(other);
        
    }
}
