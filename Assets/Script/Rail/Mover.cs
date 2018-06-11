using UnityEngine;
using System.Collections;
using System;

[ExecuteInEditMode]
public class Mover : MonoBehaviour
{
    public Rail rail;

    public PlayMode mode;
    public float speed = 1;
    public bool isReversed;
    public bool isLooping;
    public bool pingPong;

    private int currentSeg;
    private float transition;
    private bool isCompleted;

    private void Start()
    {
    }

    private void Update()
    {
        if (!rail)
            return;

        if(!isCompleted)
            Play(!isReversed);
    }

    private void Play(bool forward = true)
    {
        float m = (rail.nodes[currentSeg + 1].transform.position - rail.nodes[currentSeg].transform.position).magnitude;
        float s = (Time.deltaTime * 1 / m) * speed;
        transition += (forward)? s : -s;
        if (transition > 1)
        {
            transition = 0;
            currentSeg++;
            if (currentSeg == rail.nodes.Length - 1)
            {
                if (isLooping)
                {
                    if(pingPong)
                    {
                        transition = 1;
                        currentSeg = rail.nodes.Length - 2;
                        isReversed = !isReversed;
                    }
                    else
                    {
                        currentSeg = 0;
                    }
                }
                else
                {
                    isCompleted = true;
                    return;
                }
            }
        }
        else if (transition < 0)
        {
            transition = 1;
            currentSeg--;
            if (currentSeg == -1)
            {
                if (isLooping)
                {
                    if (pingPong)
                    {
                        currentSeg = 0;
                        transition = 0;
                        isReversed = !isReversed;
                    }
                    else
                    {
                        currentSeg = rail.nodes.Length - 2;
                    }
                }
                else
                {
                    isCompleted = true;
                    return;
                }
            }
        }

        transform.position = rail.PositionOnRail(currentSeg, transition, mode);
        transform.rotation = rail.LinearOrientation(currentSeg, transition);
    }
}
