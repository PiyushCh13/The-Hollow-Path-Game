using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPG.Core
{
    public class FollowCam : MonoBehaviour
    {

        public GameObject Player;


        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void LateUpdate()
        {
            transform.position = Player.transform.position;
        }
    }
}

