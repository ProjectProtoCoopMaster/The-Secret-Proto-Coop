using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchBehavior : MonoBehaviour
{
    public float rotateSpeed;

    public List<Vector3> directions { get; set; }
    private Vector3 currentDirection;

    private Quaternion currentRotation;

    public int _pos { get; set; }

    public bool watch { get; set; } = false;

    private float time;

    public void SetDirection(int index)
    {
        currentDirection = directions[index];

        Vector3 target = transform.position + currentDirection;
        Vector3 position = target - transform.position;
        position.y = 0.0f;

        currentRotation = Quaternion.LookRotation(position, Vector3.up);
        Debug.Log(currentRotation.eulerAngles);

        time = 0.0f;
    }

    void Update()
    {
        if (watch)
        {
            WatchDirection();

            if (transform.rotation == currentRotation)
            {
                NextDirection();
            }
        }
    }

    public void WatchDirection()
    {
        time += (Time.deltaTime * rotateSpeed);

        transform.rotation = Quaternion.Lerp(transform.rotation, currentRotation, time);
    }

    private void NextDirection()
    {
        _pos++;

        if (directions.Count == _pos) watch = false;

        else SetDirection(_pos);
    }
}
