using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public CatmullRomPath path;
    public Transform cameraTarget;
    public int startingIndex = 0;
    public float targetFOV = 10.0f;

    private new Camera camera;
    private const float kSpeed = 20.0f;
    private float movement = 1.0f;
    private int currentIndex = 0;
    private float lerpPerc = 1.0f;
    private float timer = 0.0f;
    private float zoomSpeed = 0.5f;
    private Vector3 lookAtTarget = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<Camera>();
        
        if (startingIndex >= 0 && startingIndex < path.Points.Count)
            currentIndex = startingIndex;

        Vector3 location = path.Points[currentIndex];
        camera.transform.position = location;
        camera.transform.rotation = Quaternion.LookRotation(cameraTarget.position - location);
    }

    // Update is called once per frame
    void Update()
    {
        if (lookAtTarget != Vector3.zero && lerpPerc <= 1.0f)
        {
            camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, targetFOV, lerpPerc);
            lerpPerc += Time.deltaTime * zoomSpeed;
        }
        else if (lerpPerc <= 1.0f)
        {
            camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, 60.0f, lerpPerc);
            lerpPerc += Time.deltaTime * zoomSpeed;
        }

        if (timer > 0.0f)
        {
            timer -= Time.deltaTime;
            if (timer <= 0.0f)
            {
                Vector3 location = path.Points[currentIndex];
                camera.transform.position = location;
                camera.transform.rotation = Quaternion.LookRotation(cameraTarget.position - location);
                lookAtTarget = Vector3.zero;
                lerpPerc = 0.0f;
                movement = 1.0f;
            }
        }

        if (movement > 0.0f)
        {
            HandleForwardMovement();
        }
        else if (movement < 0.0f)
        {
            HandleBackwardMovement();
        }
    }

    public void SetLookAt(Vector3 target, float duration)
    {
        if (timer <= 0.0f && lerpPerc >= 1.0f)
        {
            lookAtTarget = target;
            timer = duration;
            movement = 0.0f;
            lerpPerc = 0.0f;
            transform.LookAt(lookAtTarget);
        }
    }

    public void HandleForwardMovement()
    {
        Vector3 location = transform.position;
        Vector3 targetLocation = GetNextPoint();
        Vector3 direction = (targetLocation - location).normalized;
        float distance = Vector3.Distance(location, targetLocation);
        float distanceToTravel = kSpeed * Time.deltaTime;

        Vector3 displacement;

        if (distanceToTravel > distance)
        {
            while (distanceToTravel >= 0.0f)
            {
                if (distance > distanceToTravel)
                {
                    displacement = direction * movement * distanceToTravel;
                }
                else
                {
                    displacement = direction * movement * distance;
                }

                location += displacement;

                distanceToTravel -= distance;

                currentIndex = CalculateIndexIncrement(currentIndex);
                targetLocation = GetNextPoint();
                direction = (targetLocation - location).normalized;
                distance = Vector3.Distance(location, targetLocation);
            }
        }
        else
        {
            displacement = direction * movement * distanceToTravel;
            location += displacement;
        }

        camera.transform.position = location;
        camera.transform.rotation = Quaternion.LookRotation(cameraTarget.position - location);
    }

    public void HandleBackwardMovement()
    {
        Vector3 location = transform.position;
        Vector3 targetLocation = GetNextPoint();
        Vector3 direction = (location - targetLocation).normalized;
        float distance = Vector3.Distance(location, targetLocation);
        float distanceToTravel = kSpeed * Time.deltaTime;

        Vector3 displacement;

        if (distanceToTravel > distance)
        {
            while (distanceToTravel >= 0.0f)
            {
                if (distance > distanceToTravel)
                {
                    displacement = direction * movement * distanceToTravel;
                }
                else
                {
                    displacement = direction * movement * distance;
                }

                location += displacement;

                distanceToTravel -= distance;

                currentIndex = CalculateIndexDecrement(currentIndex);
                targetLocation = GetNextPoint();
                direction = (targetLocation - location).normalized;
                distance = Vector3.Distance(location, targetLocation);
            }
        }
        else
        {
            displacement = direction * movement * distanceToTravel;
            location += displacement;
        }

        camera.transform.position = location;
        camera.transform.rotation = Quaternion.LookRotation(cameraTarget.position - location);
    }

    Vector3 GetNextPoint()
    {
        int nextIndex = 0;

        if (movement > 0.0f)
        {
            nextIndex = CalculateIndexIncrement(currentIndex);
        }
        else if (movement < 0.0f)
        {
            nextIndex = CalculateIndexDecrement(currentIndex);
        }

        return path.Points[nextIndex];
    }

    private int CalculateIndexIncrement(int currentIndex)
    {
        int index = currentIndex + 1;
        if (index >= path.Points.Count)
        {
            index = 0;
        }
        return index;
    }

    private int CalculateIndexDecrement(int currentIndex)
    {
        int index = currentIndex - 1;
        if (index < 0)
        {
            index = path.Points.Count - 1;
        }
        return index;
    }
}
