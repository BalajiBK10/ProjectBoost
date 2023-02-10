using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
   [SerializeField] float mainThrust = 100f;
   [SerializeField] float rotationThrust = 50f;
   [SerializeField] AudioClip LimitSnap;

   [SerializeField] ParticleSystem mainEngineParticles;
   [SerializeField] ParticleSystem RightThrusterParticles;
   [SerializeField] ParticleSystem LeftThrusterParticles;
   [SerializeField] ParticleSystem BackThrusterParticles;
   [SerializeField] ParticleSystem FrontThrusterParticles;
    Rigidbody rb;
    AudioSource audioSource;
        // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
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
            StopRotating();
        }

    }

      void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(LimitSnap);
        }
        
        if (!mainEngineParticles.isPlaying)
        {
            mainEngineParticles.Play();
        }
    }

    void StopThrusting()
    {
        audioSource.Stop();
        mainEngineParticles.Stop();
    }

     void ProcessRotation()
    {
         if(Input.GetKey(KeyCode.A))
        {
            RotateRight();
        }

        else if(Input.GetKey(KeyCode.D))
        {
            RotateLeft();

        }

        else
        {
            StopRotating();
        }
    }

     void RotateLeft()
    {
        ApplyRotation(-rotationThrust);
        if (!LeftThrusterParticles.isPlaying)
        {
            LeftThrusterParticles.Play();
        }
    }

     void RotateRight()
    {
        ApplyRotation(rotationThrust);
        if (!RightThrusterParticles.isPlaying)
        {
            RightThrusterParticles.Play();
        }
    }

     void StopRotating()
    {
        RightThrusterParticles.Stop();
        LeftThrusterParticles.Stop();
    }

     void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; // freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; // unfreezing the rotation so that the physics system(rigidbody) can take over
    }
}
