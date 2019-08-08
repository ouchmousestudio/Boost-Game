using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    //Components
    Rigidbody rigidBody;
    AudioSource audioSource;

    //Serialized Fields
    [SerializeField] float rcsThrust;
    [SerializeField] float mainThrust;
    [Header("Rocket Audio")]
    [SerializeField] AudioClip mainEngineSFX;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] AudioClip levelCompleteSFX;

    enum State { Alive, Dying, Succeed }
    State state = State.Alive;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Alive)
        {
            //Respond to key inputs
            Thrust();
            Rotate();
        }
    }

    private void Rotate()
    {
        //manual control
        rigidBody.freezeRotation = true;

        float frameDepRotation = rcsThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * frameDepRotation);
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * frameDepRotation);
        }
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
        }
    }

    private void ApplyThrust()
    {
        rigidBody.AddRelativeForce(Vector3.up * mainThrust);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngineSFX);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Only allow one collision
        if (state != State.Alive) { return; }

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
                state = State.Succeed;
                audioSource.PlayOneShot(levelCompleteSFX);
                Invoke("LoadNextScene", 1f);
                break;
            default:
                state = State.Dying;
                Invoke("LoadFirstLevel", 1f);
                audioSource.Stop();
                audioSource.PlayOneShot(deathSFX);
                // Kill Player
                break;
        }
    }

    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(1); //todo add more levels
    }
}
