using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public enum AnimalType
{
    None,
    Chicken,
    Chick,
    Alpaca,
    Bull,
    Donkey,
    Fox
}

public class Animal : MonoBehaviour
{
    public List<Animal> animalsToAvoid;

    protected float animalDetectionDistance;
    protected AnimalType animalType;
    protected CameraController cameraController;

    public AnimalType GetAnimalType
    {
        get { return animalType; }
    }

    protected Animator animator;
    protected NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        OnStart();
        cameraController = Camera.main.GetComponent<CameraController>();
        animator = GetComponentInChildren<Animator>();
    }

    public void PlayAnimation(string animationState)
    {
        animator.Play(animationState);
    }

    public AnimalType CheckNearbyAnimals()
    {
        float closestDistance = float.PositiveInfinity;
        AnimalType animalType = AnimalType.None;
        foreach (Animal animal in animalsToAvoid)
        {
            float dist = Vector3.Distance(animal.transform.position, transform.position);
            if (dist < closestDistance && dist < animalDetectionDistance)
            {
                closestDistance = dist;
                animalType = animal.GetAnimalType;
            }
        }

        return animalType;
    }

    protected void Initialize(AnimalSettings settings)
    {
        agent = GetComponentInChildren<NavMeshAgent>();

        if (settings != null && agent != null)
        {
            agent.speed = settings.speed;
            agent.acceleration = settings.acceleration;
            agent.angularSpeed = settings.angularSpeed;
            agent.stoppingDistance = settings.stoppingDistance;
        }
    }

    protected virtual void OnStart()
    {
        // Override this in a derived class to be notified when start is called
    }
}
