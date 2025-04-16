using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusTargetCameraNode : ActionNode
{
    public float duration;
    public string target;
    private Vector3 targetPos;

    protected override void OnStart()
    {
        targetPos = blackboard.GetValue<Vector3>(target);
    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        Camera.main.GetComponent<CameraController>().SetLookAt(targetPos, duration);

        return State.Success;
    }
}
