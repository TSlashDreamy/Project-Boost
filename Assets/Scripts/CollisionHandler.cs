using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] AudioClip successSound;
    [SerializeField] AudioClip crashSound;
    AudioSource objAudioSource;
    [SerializeField] ParticleSystem successParticle;
    [SerializeField] ParticleSystem crashParticle;

    [SerializeField] bool collisionDisabled = false;
    bool isTransitioning = false;

    void Start()
    {
        objAudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        RespondToDebugKeys();
    }

    private void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled; // switching collision (on/off)
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (isTransitioning || collisionDisabled) return;

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Collided with friendly object");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            case "Obstacle":
                StartCrashSequence();
                break;
            default:
                Debug.Log("Yo, you bumped in something . _.");
                break;
        }
    }

    void StartCrashSequence()
    {
        float delay = 1f;

        isTransitioning = true;
        GetComponent<Movement>().enabled = false;
        crashParticle.Play();
        objAudioSource.Stop();
        objAudioSource.PlayOneShot(crashSound);
        Invoke("ReloadScene", delay);
    }

    void StartSuccessSequence()
    {
        float delay = 3f;

        isTransitioning = true;
        GetComponent<Movement>().enabled = false;
        successParticle.Play();
        objAudioSource.Stop();
        objAudioSource.PlayOneShot(successSound);
        Invoke("LoadNextLevel", delay);
    }

    void LoadNextLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentScene + 1;

        if (nextScene == SceneManager.sceneCountInBuildSettings) nextScene = 0;

        SceneManager.LoadScene(nextScene);
    }

    void ReloadScene()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }
}
