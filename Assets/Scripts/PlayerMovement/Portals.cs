using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using UnityEngine.UI;

namespace RPG.SceneManagement
{
    public class Portals : MonoBehaviour
    {
        enum Destination
        {
            A,B,C,D,E
        };


        [SerializeField] int sceneToLoadIndex;
        [SerializeField] Transform spawnPoint;
        [SerializeField] Destination destination;
        float fadeIn = 1f;
        float fadeOut = 0.5f;
        float Wait = 0.5f;

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                StartCoroutine(Transition());
            }
        }

        private IEnumerator Transition()
        {
            if(sceneToLoadIndex < 0)
            {
                Debug.LogError("No Scene to Load.");
                yield break;
            }


            
            DontDestroyOnLoad(gameObject);
            Fader fader = FindObjectOfType<Fader>();

            yield return fader.FadeOut(fadeOut);
            yield return SceneManager.LoadSceneAsync(sceneToLoadIndex);
            Portals otherPortal = GetNewPortal();
            UpdatePlayer(otherPortal);
            yield return new WaitForSeconds(Wait);
            yield return fader.FadeIn(fadeIn);
            Destroy(gameObject);
        }

        private static void UpdatePlayer(Portals otherPortal)
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<NavMeshAgent>().Warp(otherPortal.spawnPoint.position);
          //player.transform.position = otherPortal.spawnPoint.position;
            player.transform.rotation = otherPortal.spawnPoint.rotation;
        }

        private Portals GetNewPortal()
        {
            foreach (Portals portals in FindObjectsOfType<Portals>())
            {
                if (portals == this) continue;
                if (portals.destination != destination) continue;

                return portals;
            }

            return null;
        }
    }
}

