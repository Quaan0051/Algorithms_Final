using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Chick : Animal
{
    private Target target;
    private Flock flock;

    private void Awake()
    {
        animalType = AnimalType.Chick;
    }

    // Update is called once per frame
    void Update()
    {
        if (target.speed == 0)
        {
            PlayAnimation("Chick-IdlePeck");
            flock.settings.minSpeed = 0;
            flock.settings.maxSpeed = 0;
        }
        else
        {
            PlayAnimation("Chick-Run");
            flock.settings.minSpeed = 8;
            flock.settings.maxSpeed = 13;
        }
    }

    protected override void OnStart()
    {
        Boid boid = GetComponent<Boid>();
        flock = boid.GetFlock;
        target = boid.GetFlock.target;
    }
}
