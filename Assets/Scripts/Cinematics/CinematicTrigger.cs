using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour
    {
        public PlayableDirector introSequence;
        bool alreadyTriggered = false;
        bool skipAvailable = false;
        

      private void OnTriggerEnter(Collider other)
      {
        if(other.tag == "Player" && !alreadyTriggered)
        {
                introSequence.Play();
                alreadyTriggered = true;
                skipAvailable = true;
        }

       }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.S) && skipAvailable)
            {
                introSequence.Stop();
                skipAvailable = false;
            }
        }

    }

}

