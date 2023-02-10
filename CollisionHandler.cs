using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CollisionHandler : MonoBehaviour
{
  [SerializeField] float Delay = 2f;
  [SerializeField] AudioClip Death;
  [SerializeField] AudioClip Success;

  [SerializeField] ParticleSystem ExplosionParticles;
  [SerializeField] ParticleSystem SuccessParticles;
  


  bool isTransitioning = false;
  bool CollisionDisabled = false;

  AudioSource audioSource;
   void Start() 
  {
    audioSource = GetComponent<AudioSource>();
  }

  void Update()
  {
    RespondToDebugKey();
   
  }

    void RespondToDebugKey()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
          LoadNextLevel();
        }
        else if(Input.GetKeyDown(KeyCode.C))
        {
          CollisionDisabled = !CollisionDisabled;
        }
    }

    void OnCollisionEnter(Collision other) 
   {
   
     if(isTransitioning || CollisionDisabled)
     {
      return;
     }
     
      switch(other.gameObject.tag)
      {
        case "Friendly":

        break;
          
            case "Finish":
            StartSuccessSequence();
          break;

          default:
            StartCrashSequence();                                                                                                
            break;
      }
            
     
   }

     void StartSuccessSequence()
    { 
      isTransitioning = true;
      audioSource.Stop();
      audioSource.PlayOneShot(Success);
      SuccessParticles.Play();
      //we got movement script here because we need to stop the action after we crash so we use enable = false
       GetComponent<Movement>().enabled=false;
       Invoke("LoadNextLevel",Delay);
        }

    void StartCrashSequence()
   {
     isTransitioning = true;
     audioSource.Stop();
     audioSource.PlayOneShot(Death);
     ExplosionParticles.Play();
     GetComponent<Movement>().enabled=false;
      Invoke("ReloadLevel",Delay);
      }

   void LoadNextLevel()
   {
    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    int nextSceneIndex = currentSceneIndex + 1;
    if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
    {
      nextSceneIndex = 0;
    }
    SceneManager.LoadScene(nextSceneIndex);
   }

   void ReloadLevel()
   {
    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    SceneManager.LoadScene(currentSceneIndex);
    }
    
}
    


