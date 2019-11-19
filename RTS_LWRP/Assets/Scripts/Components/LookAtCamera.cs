﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    public float secondsBetweenRefresh = 0.25f;

    public Transform transformToFace;
    Transform cameraTransform;

    // Start is called before the first frame update
    void Start()
    {
    
        cameraTransform = Camera.main.transform;
        StartCoroutine(FacingToCamera(secondsBetweenRefresh));
    }

    IEnumerator FacingToCamera(float seconds)
    {
        while(true)
        {
            transformToFace.LookAt(cameraTransform);
            yield return new WaitForSeconds(seconds);
        }
    }
}
