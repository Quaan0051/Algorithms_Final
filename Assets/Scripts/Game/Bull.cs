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
    private float TargetNum;
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

            if (RageTimer <= 0.0f)
            {
                blackboard.SetValue<bool>("Rage", true);
                agent.speed = 100.0f;

                //choose rand chicken
                TargetNum = Random.Range(1.0f, 3.0f);

                //get rand chicken location
                if (TargetNum <= 1.0f)
                {
                    //put chicken in target
                    Target = Chicken1.transform.position;
                    //modify blackboard
                    blackboard.SetValue<Vector3>("Target", Target);
                }
                else if (TargetNum >= 2.0f)
                {
                    Target = Chicken3.transform.position;
                    blackboard.SetValue<Vector3>("Target", Target);
                }
                else
                {
                    Target = Chicken2.transform.position;
                    blackboard.SetValue<Vector3>("Target", Target);
                }

                RageTimer = Random.Range(20.0f, 40.0f);
            }
        
            if (HungryTimer >= 0.0f && blackboard.GetValue<bool>("isHungry") == false)
            {
                HungryTimer -= Time.deltaTime;
                if (HungryTimer <= 0.0f)
                {
                    blackboard.SetValue<bool>("isHungry", true);
                    HungryTimer = Random.Range(45.0f, 90.0f);
                }
            }
        }   
    }

    protected override void OnStart()
    {
        Initialize(settings);

        HungryTimer = Random.Range(40.0f, 60.0f);
        RageTimer = Random.Range(10.0f, 30.0f);

        blackboard.SetOrAddValue<bool>("Rage", false);

        Target.x = 225;
        Target.z = 325;
    }
}
