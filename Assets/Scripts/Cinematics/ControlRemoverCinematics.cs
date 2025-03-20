using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.Playables;
using RPG.Core;

namespace RPG.Cinematics
{
    public class ControlRemoverCinematics : MonoBehaviour
    {
        GameObject player;
        public Text skipTest;

        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            GetComponent<PlayableDirector>().played += DisableControl;
            GetComponent<PlayableDirector>().stopped += EnableControl;
            skipTest.enabled = false;
        }

        void EnableControl(PlayableDirector nonsense)
        {
            player.GetComponent<PlayerController>().enabled = true;
            skipTest.enabled = false;
        }

        void DisableControl(PlayableDirector nonsense)
        {
            player.GetComponent<Scheduler>().CancelCurrentAction();
            player.GetComponent<PlayerController>().enabled = false;
            skipTest.enabled = true;
        }
    }

}