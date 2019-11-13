using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCursor
{
    bool following;
    Transform transformToMove;
    float movementSpeed = 2.6f;

    public FollowCursor(Transform transformToMove) => this.transformToMove = transformToMove;

    public void StartMoving()
    {
        GameController.Get().StartCoroutine(UpdatePosition());
        this.following = true;
    }

    public void StopMoving()
    {
        GameController.Get().StopCoroutine(UpdatePosition());
        this.following = false;
    }

    IEnumerator UpdatePosition()
    {
        while(true)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                transformToMove.position = Vector3.Slerp(transformToMove.position, hit.point, Time.deltaTime * movementSpeed);
            }

            yield return new WaitForEndOfFrame();
        }
    }


}
