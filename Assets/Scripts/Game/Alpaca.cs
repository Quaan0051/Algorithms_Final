using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Alpaca : Animal
{
    [SerializeField] private Blackboard blackboard;
    [SerializeField] private AnimalSettings settings;

    private float HungryTimer;

    private AnimalType nearbyAnimalType = AnimalType.None;
    private Animal nearbyAnimal;
    private float speed = 12.0f;
    private Rigidbody spitRb;
    private Vector3 spitPos;

    private void Awake()
    {
        animalType = AnimalType.Alpaca;
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

        if (nearbyAnimalType != tempAnimalType)
        {
            nearbyAnimalType = tempAnimalType;
            nearbyAnimal = tempAnimal;

            if (nearbyAnimalType == AnimalType.Chicken || nearbyAnimalType == AnimalType.Donkey)
            {
                blackboard.SetValue<bool>("isHungry", false);
                blackboard.SetValue<bool>("canHawkTuah", true);
                if (HungryTimer <= 5.0f)
                {
                    HungryTimer = Random.Range(30.0f, 60.0f);
                }
            }
            else if (nearbyAnimalType == AnimalType.Bull)
            {
                agent.speed = 25.0f;
            }
            else
            {
                agent.speed = speed;
            }
        }


        if (HungryTimer >= 0.0f && blackboard.GetValue<bool>("isHungry") == false)
        {
            HungryTimer -= Time.deltaTime;
            if(HungryTimer <= 0.0f)
            {
                blackboard.SetValue<bool>("isHungry", true);
                HungryTimer = Random.Range(30.0f, 60.0f);
            }
        }

        if(spitRb.transform.position.y < 1.0f)
        {
            spitRb.velocity = Vector3.zero;
            spitRb.transform.localPosition = spitPos;
            spitRb.useGravity = false;
            spitRb.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    protected override void OnStart()
    {
        Initialize(settings);
        HungryTimer = Random.Range(30.0f, 60.0f);

        spitRb = GetComponentInChildren<Rigidbody>();
        spitPos = spitRb.transform.localPosition;

        blackboard.SetOrAddValue("isHungry", false);
        blackboard.SetOrAddValue("canHawkTuah", false);

        animalDetectionDistance = 20.0f;
    }

    public void Spit()
    {
        if (nearbyAnimal != null)
        {
            MeshRenderer spitRenderer = spitRb.GetComponent<MeshRenderer>();
            spitRb.useGravity = true;
            spitRenderer.enabled = true;
            Vector3 force = (nearbyAnimal.transform.position - spitRb.position).normalized;
            force *= 10;
            spitRb.AddForce(force, ForceMode.Impulse);
        }
    }
}
