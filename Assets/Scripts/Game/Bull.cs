using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Bull : Animal
{
    [SerializeField] private Blackboard blackboard;
    [SerializeField] private AnimalSettings settings;

    private float HungryTimer;
    private float RageTimer;
    private int TargetNum;
    private Vector3 Target;

    public GameObject Chicken1;
    public GameObject Chicken2;
    public GameObject Chicken3;

    private void Awake()
    {
        animalType = AnimalType.Bull;
    }

    // Update is called once per frame
    void Update()
    {
        if (RageTimer >= 0.0f && blackboard.GetValue<bool>("Rage") == false)
        {
            RageTimer -= Time.deltaTime;
            agent.speed = 3.5f;
            agent.acceleration = 4.0f;
            agent.angularSpeed = 150.0f;
            agent.stoppingDistance = 6.0f;

            if (RageTimer <= 0.0f)
            {
                blackboard.SetValue<bool>("Rage", true);
                agent.speed = 50.0f;
                agent.acceleration = 35.0f;
                agent.angularSpeed = 250.0f;
                agent.stoppingDistance = 3.0f;

                //choose rand chicken
                TargetNum = Random.Range(1, 4);

                //get rand chicken location
                if (TargetNum == 1)
                {
                    //put chicken in target
                    Target = Chicken1.transform.position;
                    //modify blackboard
                    blackboard.SetValue<Vector3>("Target", Target);
                }
                else if (TargetNum == 2)
                {
                    Target = Chicken2.transform.position;
                    blackboard.SetValue<Vector3>("Target", Target);
                }
                else
                {
                    Target = Chicken3.transform.position;
                    blackboard.SetValue<Vector3>("Target", Target);
                }

                RageTimer = Random.Range(40.0f, 90.0f);
            }
        
            if (HungryTimer >= 0.0f && blackboard.GetValue<bool>("isHungry") == false)
            {
                HungryTimer -= Time.deltaTime;
                if (HungryTimer <= 0.0f)
                {
                    blackboard.SetValue<bool>("isHungry", true);
                    HungryTimer = Random.Range(30.0f, 50.0f);
                }
            }
        }   
    }

    protected override void OnStart()
    {
        Initialize(settings);

        blackboard.SetOrAddValue<bool>("Rage", false);
        blackboard.SetOrAddValue<bool>("isHungry", false);

        HungryTimer = Random.Range(30.0f, 50.0f);
        RageTimer = Random.Range(10.0f, 30.0f);
    }
}
