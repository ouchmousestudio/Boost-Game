using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    //Components
    Rigidbody rigidBody;
    AudioSource audioSource;
    SceneLoader sceneLoader;

    //Serialized Fields
    [Header("Rocket Parameters")]
    [SerializeField] float rcsThrust;
    [SerializeField] float mainThrust;
    [Header("Rocket Audio")]
    [SerializeField] AudioClip mainEngineSFX;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] AudioClip levelCompleteSFX;
    [Header("Particles")]
    [SerializeField] ParticleSystem mainEngineVFX;
    [SerializeField] ParticleSystem deathVFX;
    [SerializeField] ParticleSystem levelCompleteVFX;

    [SerializeField] float timeToWait = 1.3f;

    //Player states
    bool isTransitioning;

    //Debug mode
    bool collissionsOff = false;


    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isTransitioning)
        {
            //Respond to key inputs
            Thrust();
            Rotate();
            if (Debug.isDebugBuild)
            {
                DebugKeys();
            }
        }
    }

    private void DebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            sceneLoader.LoadNextScene();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            collissionsOff = !collissionsOff;
        }
    }

    private void Rotate()
    {


        float frameDepRotation = rcsThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            RocketRotation(frameDepRotation);
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            RocketRotation(-frameDepRotation);
        }

    }

    private void RocketRotation(float frameDepRotation)
    {
        //resume physics
        rigidBody.freezeRotation = true;
        transform.Rotate(Vector3.forward * frameDepRotation);
        //stop
        rigidBody.freezeRotation = false;
    }

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W))
        {
            ApplyThrust();
        }
        else
        {
            audioSource.Stop();
            mainEngineVFX.Stop();
        }
    }

    private void ApplyThrust()
    {
        rigidBody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngineSFX);
        }
        mainEngineVFX.Play();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Only allow one collision
        if (isTransitioning || collissionsOff) { return; }

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                print("Safe!"); //todo remove
                //Landing Pad
                break;
            case "Refuel":
                print("Refuelling"); //todo remove
                //Refuel ship
                break;
            case "Goal":
                LevelComplete();
                break;
            default:
                DeathSequence();
                break;
        }
    }

    private void DeathSequence()
    {
        isTransitioning = true;
        Invoke("LoadStartSceneRef", timeToWait);
        audioSource.Stop();
        audioSource.PlayOneShot(deathSFX);
        deathVFX.Play();
    }

    private void LevelComplete()
    {
        isTransitioning = true;
        Invoke("LoadNextSceneRef", timeToWait);
        audioSource.PlayOneShot(levelCompleteSFX);
        levelCompleteVFX.Play();
    }
    private void LoadStartSceneRef()
    {
        sceneLoader.LoadStartScene();
    }
    private void LoadNextSceneRef()
    {
        sceneLoader.LoadNextScene();
    }
}