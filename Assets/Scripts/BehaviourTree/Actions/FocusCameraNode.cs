using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusCameraNode : ActionNode
{
    public float duration;

    protected override void OnStart()
    {
        
    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        Camera.main.GetComponent<CameraController>().SetLookAt(context.transform.position, duration);

        return State.Success;
    }
}
