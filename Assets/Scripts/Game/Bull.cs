using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Bull : Animal
{
    [SerializeField] private Blackboard blackboard;
    [SerializeField] private AnimalSettings settings;

    private float HungryTimer;
    //private float RageTimer;

    private void Awake()
    {
        animalType = AnimalType.Bull;
    }

    // Update is called once per frame
    void Update()
    {
        
        //if (RageTimer >= 0.0f && blackboard.GetValue<bool>("Rage") == false)
            //RageTimer -= Time.deltaTime;
            //if (RageTimer <= 0.0f)
            //{
            //    blackboard.SetValue<bool>("Rage", true);
            //    RageTimer = Random.Range(20.0f, 40.0f);
            //}
        if (HungryTimer >= 0.0f && blackboard.GetValue<bool>("isHungry") == false)
        {
            HungryTimer -= Time.deltaTime;
            if (HungryTimer <= 0.0f)
            {
                blackboard.SetValue<bool>("isHungry", true);
                HungryTimer = Random.Range(45.0f, 90.0f);
            }
        }
        //else 
            //get rand chicken location
            //modify blackboard, put chicken in target
    }

    protected override void OnStart()
    {
        Initialize(settings);
        HungryTimer = Random.Range(45.0f, 90.0f);
        //RageTimer = Random.Range(20.0f, 40.0f);
    }
}
