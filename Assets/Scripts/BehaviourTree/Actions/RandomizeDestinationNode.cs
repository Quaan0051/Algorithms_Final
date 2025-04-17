using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RandomizeDestinationNode : ActionNode
{
    public string destinationKey;
    public float walkRadius = 5.0f;

    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        Vector3 randomPoint = Random.insideUnitCircle * walkRadius;
        randomPoint.z = randomPoint.y;
        randomPoint.y = 0.3f;
        randomPoint += new Vector3(240.04f, 0.0f, 333.2f);
        NavMeshHit hit;
        NavMesh.SamplePosition(randomPoint, out hit, walkRadius, 1);
        

        blackboard.SetOrAddValue(destinationKey, hit.position);

        return State.Success;
    }
}
