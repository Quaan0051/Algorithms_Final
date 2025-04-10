using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Chicken : Animal
{
    [SerializeField] private Blackboard blackboard;

    Target target;

    float Timer;

    private void Awake()
    {
        animalType = AnimalType.Chicken;
    }

    private void Update()
    {
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
                    target.speed = 8;
                }
                else
                {
                    blackboard.SetValue<bool>("CanPeck", true);
                    target.speed = 0;
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

        Timer = Random.Range(5.0f, 10.0f);
    }
}
