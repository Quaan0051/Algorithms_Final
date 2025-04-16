using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationNode : ActionNode
{
    public float rotation;
    private float current;
    private float lerpPercent = 0.0f;

    protected override void OnStart()
    {
        
    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        current = Mathf.Lerp(current, rotation, lerpPercent);

        lerpPercent += Time.deltaTime;

        context.transform.rotation.Set(context.transform.rotation.x,current,context.transform.rotation.z, context.transform.rotation.w);

        if (lerpPercent >= 1)
        {
            return State.Success;
        }

        return State.Running;
    }
}
