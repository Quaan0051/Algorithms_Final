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

    private void Awake()
    {
        animalType = AnimalType.Alpaca;
    }

    // Update is called once per frame
    void Update()
    {
        if (HungryTimer >= 0.0f && blackboard.GetValue<bool>("isHungry") == false)
        {
            HungryTimer -= Time.deltaTime;
            if(HungryTimer <= 0.0f)
            {
                blackboard.SetValue<bool>("isHungry", true);
                HungryTimer = Random.Range(30.0f, 60.0f);
            }
        }
    }

    protected override void OnStart()
    {
        Initialize(settings);
        HungryTimer = Random.Range(30.0f, 60.0f);
    }
}
