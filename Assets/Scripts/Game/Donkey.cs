using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Donkey : Animal
{
    [SerializeField] private Blackboard blackboard;
    [SerializeField] private AnimalSettings settings;

    private float HungryTimer;
    private float speed = 15.0f;

    AnimalType nearbyAnimal = AnimalType.None;

    private void Awake()
    {
        animalType = AnimalType.Donkey;
    }

    // Update is called once per frame
    void Update()
    {
        Animal tempAnimal = CheckNearbyAnimals();
        AnimalType tempAnimalType = AnimalType.None;
        if (tempAnimal != null)
        {
            tempAnimalType = tempAnimal.GetAnimalType;
        }

        if (nearbyAnimal != tempAnimalType)
        {
            nearbyAnimal = tempAnimalType;

            if (nearbyAnimal == AnimalType.Chicken || nearbyAnimal == AnimalType.Alpaca)
            {
                blackboard.SetValue<bool>("isHungry", false);
                blackboard.SetValue<bool>("canKick", true);
            }
            else if (nearbyAnimal == AnimalType.Bull)
            {
                agent.speed = 25.0f;
            }
            else
            {
                agent.speed = speed;
            }
        }

        //check hunger
        if (HungryTimer >= 0.0f && blackboard.GetValue<bool>("isHungry") == false)
        {
            HungryTimer -= Time.deltaTime;
            if (HungryTimer <= 0.0f)
            {
                blackboard.SetValue<bool>("isHungry", true);
                HungryTimer = Random.Range(15.0f, 30.0f);
            }
        }
    }

    protected override void OnStart()
    {
        Initialize(settings);
        blackboard.SetValue<bool>("isHungry", false);
        blackboard.SetValue<bool>("canKick", false);
        HungryTimer = Random.Range(15.0f, 30.0f);

        animalDetectionDistance = 15.0f;
    }
}
