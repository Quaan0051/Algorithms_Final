using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;


public class Chicken : Animal
{
    [SerializeField] private Blackboard blackboard;

    Target target;

    float speed = 8;

    float Timer;
    AnimalType nearbyAnimal = AnimalType.None;

    private void Awake()
    {
        animalType = AnimalType.Chicken;
    }

    private void Update()
    {
        AnimalType tempAnimal = CheckNearbyAnimals();
        if(nearbyAnimal != tempAnimal)
        {
            nearbyAnimal = tempAnimal;
            switch (nearbyAnimal)
            {
                case AnimalType.None:
                    target.speed = speed;
                    break;
                case AnimalType.Alpaca:
                case AnimalType.Bull:
                case AnimalType.Donkey:
                    blackboard.SetValue<bool>("CanPeck", false);
                    target.speed = 15;
                    speed = 8;
                    cameraController.SetLookAt(transform.position, 5);
                    break;
            }
        }
        
        if (Timer > 0.0f)
        {
            Timer -= Time.deltaTime;
            if (Timer <= 0.0f)
            {
                Timer = Random.Range(5.0f, 10.0f);
                blackboard.SetValue<bool>("CanPeck", !blackboard.GetValue<bool>("CanPeck"));

                if (target.speed == 0)
                {
                    blackboard.SetValue<bool>("CanPeck", false);
                    speed = 8;
                }
                else
                {
                    blackboard.SetValue<bool>("CanPeck", true);
                    speed = 0;
                }
            }
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 current = target.transform.position;
        Vector3 previous = target.PreviousPosition;
        Vector3 direction = (current - previous).normalized;
        transform.forward = direction;
    }

    protected override void OnStart()
    {
        blackboard.SetOrAddValue<bool>("CanPeck", false);
        blackboard.SetOrAddValue<float>("Speed", 8.0f);
        target = GetComponent<Target>();

        animalDetectionDistance = 30.0f;

        Timer = Random.Range(5.0f, 10.0f);
    }
}
