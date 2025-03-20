using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class ObjSpawner : MonoBehaviour
    {
        [SerializeField] GameObject spawnObject;
        static bool hasSpawned;
        private void Awake()
        {
            if (hasSpawned == true) return;
            SpawnPersistentObject();
            hasSpawned = true;
        }

        private void SpawnPersistentObject()
        {
            GameObject Obj = Instantiate(spawnObject);
            DontDestroyOnLoad(Obj);
        }
    }
}

