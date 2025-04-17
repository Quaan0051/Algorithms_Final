using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationNode : ActionNode
{
    public Vector3 lookPosition;
    private float lerpPercent = 0.0f;
    private Quaternion originalRotation;
    private Quaternion newRotation;

    protected override void OnStart()
    {
        originalRotation = context.transform.rotation;
        context.transform.LookAt(lookPosition);
        newRotation = context.transform.rotation;
    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        context.transform.rotation = Quaternion.Slerp(originalRotation, newRotation, lerpPercent);

        lerpPercent += Time.deltaTime;

        if (lerpPercent >= 1)
        {
            return State.Success;
        }

        return State.Running;
    }
}
