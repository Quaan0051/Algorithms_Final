using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Chick : Animal
{
    private Target target;
    private Flock flock;
    public bool pauseBoid;

    private void Awake()
    {
        animalType = AnimalType.Chick;
    }

    // Update is called once per frame
    void Update()
    {
        if (target.speed == 0 && Vector3.Distance(transform.position, target.transform.position) <= 10.0f)
        {
            PlayAnimation("Chick-IdlePeck");
            //flock.settings.minSpeed = 0;
            //flock.settings.maxSpeed = 0;
            pauseBoid = true;
        }
        else if (target.speed != 0)
        {
            PlayAnimation("Chick-Run");
            //flock.settings.minSpeed = 8;
            //flock.settings.maxSpeed = 13;
            pauseBoid = false;
        }

        if (target.speed > 8)
        {
            flock.settings.minSpeed = target.speed - 5;
            flock.settings.maxSpeed = target.speed;
        }
        else
        {
            flock.settings.minSpeed = 8;
            flock.settings.maxSpeed = 13;
        }
    }

    protected override void OnStart()
    {
        Boid boid = GetComponent<Boid>();
        flock = boid.GetFlock;
        target = flock.target;
    }
}
