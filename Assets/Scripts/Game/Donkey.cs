using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Donkey : Animal
{
    [SerializeField] private Blackboard blackboard;
    [SerializeField] private AnimalSettings settings;

    private float HungryTimer;

    private void Awake()
    {
        animalType = AnimalType.Donkey;
    }

    // Update is called once per frame
    void Update()
    {
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
        HungryTimer = Random.Range(15.0f, 30.0f);
    }
}
