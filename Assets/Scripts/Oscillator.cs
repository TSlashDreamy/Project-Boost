using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    [SerializeField] Vector3 movementVector; // moving in direction
    float movementFactor; // progress of moving in direction (from 0 to 1)
    [SerializeField] float period = 2f;
    Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon) return; // protecting code for not dividing by 0 (Mathf.Epsilon - The smallest value that a float can have different from zero)
        float cycles = Time.time / period; // continually growing over time

        const float tau = Mathf.PI * 2; // const value of 6.283
        float rawSinWave = Mathf.Sin(cycles * tau); // sinwave going from -1 to 1

        movementFactor = (rawSinWave + 1f) / 2f; // taking rawSinWave, transforming it to go from 0 to 1 and assigning it to variable

        Vector3 offset = movementFactor * movementVector;
        transform.position = startPosition + offset;
    }
}
