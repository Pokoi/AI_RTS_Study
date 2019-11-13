using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCursor
{
    bool following;
    Transform transformToMove;
    float movementSpeed = 2.6f;

    LayerMask layerMask;

    public FollowCursor(Transform transformToMove, LayerMask layerMask) 
    {
        this.transformToMove    = transformToMove;
        this.layerMask          = layerMask;
    }

    public void StartMoving()
    {
        this.following = true;
        GameController.Get().StartCoroutine(this.UpdatePosition());
    }

    public void StopMoving()
    {
        this.following = false;
        GameController.Get().StopCoroutine(this.UpdatePosition());
    }

    public bool GetFollowing() => following;

    IEnumerator UpdatePosition()
    {
        while(following)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                transformToMove.position = Vector3.Slerp(transformToMove.position, hit.point, Time.deltaTime * movementSpeed);
            }

            yield return new WaitForEndOfFrame();
        }
    }


}
