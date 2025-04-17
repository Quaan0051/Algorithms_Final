using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitNode : ActionNode
{
    protected override void OnStart()
    {
        
    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        if (context.animalType == AnimalType.Alpaca)
        {
            context.gameObject.GetComponent<Alpaca>().Spit();
        }

        return State.Success;
    }
}
