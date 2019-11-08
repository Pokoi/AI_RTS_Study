﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibleItem
{
    List<Transform> visualTransforms = new List<Transform>();
    float maxAparitionSpeed = 10f;
    float minAparitionSpeed = 0.5f;

    public VisibleItem() => visualTransforms = new List<Transform>();

    public void SetVisibleItems(List<Transform> visualTransforms) => this.visualTransforms = visualTransforms;
    public void Hide()
    {
        foreach (Transform t in this.visualTransforms)
        {
            t.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    public void Show()
    {
        float lastSpeed = Random.Range(maxAparitionSpeed, minAparitionSpeed);

        foreach (Transform t in this.visualTransforms)
        {
            Vector3 desiredPosition = t.localPosition;
            t.localPosition = new Vector3 (t.localPosition.x, t.localPosition.y - 100, t.localPosition.z);
            t.GetComponent<SpriteRenderer>().enabled = true;
            GameController.Get().StartCoroutine(this.InterpolatedMovement(desiredPosition, t, lastSpeed));
        }
    }

    IEnumerator InterpolatedMovement(Vector3 desiredPosition, Transform transformToMove, float speed)
    {
        float alpha = 1f;
        while ((desiredPosition - transformToMove.position).sqrMagnitude > alpha)
        {
            transformToMove.localPosition = Vector3.Lerp(transformToMove.localPosition, desiredPosition, Time.deltaTime * speed);
            yield return new WaitForEndOfFrame();
        }
    }
}
