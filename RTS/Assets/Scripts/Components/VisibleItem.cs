using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibleItem : MonoBehaviour
{
    List<Transform> visualTransforms = new List<Transform>();
    float maxAparitionSpeed = 20f;
    float minAparitionSpeed = 2f;

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
            Vector3 desiredPosition = t.position;
            t.position = new Vector3 (t.position.x, t.position.y + 50, t.position.z);

            StartCoroutine(InterpolatedMovement(desiredPosition, t, lastSpeed));
            lastSpeed = Random.Range(lastSpeed, minAparitionSpeed);

        }
    }

    IEnumerator InterpolatedMovement(Vector3 desiredPosition, Transform transformToMove, float speed)
    {
        float alpha = 1f;
        while ((desiredPosition - transformToMove.position).sqrMagnitude > alpha)
        {
            transformToMove.position = Vector3.Lerp(transformToMove.position, desiredPosition, Time.deltaTime * speed);
            yield return new WaitForEndOfFrame();
        }
    }
}
