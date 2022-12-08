using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust;
    [SerializeField] float rotationThrust;

    [SerializeField] AudioClip thrustSound;
    [SerializeField] ParticleSystem mainThruster;
    [SerializeField] ParticleSystem leftThruster;
    [SerializeField] ParticleSystem rightThruster;
    Rigidbody rb;
    AudioSource objAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        // initializing components
        rb = GetComponent<Rigidbody>();
        objAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
        {
            ActivateBothEngines();
        }
        else if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopRotating();
        }
    }

    void StartThrusting()
    {
        if (!objAudioSource.isPlaying)
        {
            objAudioSource.PlayOneShot(thrustSound);
        }
        if (!mainThruster.isPlaying)
        {
            mainThruster.Play();
        }
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
    }

    void StopThrusting()
    {
        objAudioSource.Stop();
        mainThruster.Stop();
    }

    void ActivateBothEngines()
    {
        if (!rightThruster.isPlaying && !leftThruster.isPlaying)
        {
            leftThruster.Play();
            rightThruster.Play();
        }
    }

    void RotateLeft()
    {
        if (!rightThruster.isPlaying)
        {
            rightThruster.Play();
        }
        ApplyRotation(rotationThrust);
    }

    void RotateRight()
    {
        if (!leftThruster.isPlaying)
        {
            leftThruster.Play();
        }
        ApplyRotation(-rotationThrust);
    }

    void StopRotating()
    {
        rightThruster.Stop();
        leftThruster.Stop();
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; // freezing rb rotation physics for preventing conflict of manual rotation force
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; // unfreez rb rotations physics
    }

}
