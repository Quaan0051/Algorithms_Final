using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Alpaca : Animal
{
    [SerializeField] private Blackboard blackboard;
    [SerializeField] private AnimalSettings settings;

    Target target;

    private void Awake()
    {
        animalType = AnimalType.Alpaca;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void LateUpdate()
    {
        Vector3 current = target.transform.position;
        Vector3 previous = target.PreviousPosition;
        Vector3 direction = (current - previous).normalized;
        transform.forward = direction;
    }

    protected override void OnStart()
    {
        target = GetComponent<Target>();

        Initialize(settings);
    }
}
