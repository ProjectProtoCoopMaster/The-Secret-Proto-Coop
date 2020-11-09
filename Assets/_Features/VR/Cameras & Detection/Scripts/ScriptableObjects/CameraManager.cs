using Gameplay.VR;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public List<TestThreadedOverwatchBehavior> detection = new List<TestThreadedOverwatchBehavior>();

    private void Awake()
    {
        detection.AddRange(FindObjectsOfType<TestThreadedOverwatchBehavior>());
    }

    private void Start()
    {
        StartCoroutine(CamerasGo());
    }

    // start threads with frame differences
    private IEnumerator CamerasGo()
    {
        /*for (int i = 0; i < detection.Count; i++)
        {
            detection[i].StartLoop();
            yield return new WaitForEndOfFrame();
        }*/
    }
}
