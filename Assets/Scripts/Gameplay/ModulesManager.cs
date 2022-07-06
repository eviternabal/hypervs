using System.Collections.Generic;
using UnityEngine;

namespace NAMESPACENAME.Gameplay
{
    public class ModulesManager : MonoBehaviour
    {
        [Header("Set Values")]
        [SerializeField] GameObject[] modules;
        [SerializeField] Transform player;
        [SerializeField] Vector2 spawnOffset;
        [SerializeField] float spawnDistance;
        [SerializeField] float despawnDistance;
        [SerializeField] float firstSpawnTimer;
        [SerializeField] float spawnTimer;
        [SerializeField] int maxModules;
        [Header("Runtime Values")]
        [SerializeField] List<Transform> currentModules;
        [SerializeField] Vector3 spawnPos;
        [SerializeField] float timer;

        //Unity Events
        private void Start()
        {
            if (!player)
            {
                player = GameObject.FindGameObjectWithTag("Player").transform;
            }
            if (!player)
            {
                Debug.Log(this + " unable to find player");
                Destroy(this);
            }
            if (modules.Length < 1)
            {
                Debug.Log(this + " no modules loaded");
                Destroy(this);
            }

            spawnPos = player.position;

            timer = firstSpawnTimer;
        }
        private void Update()
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                return;
            }

            SpawnModule();
        }
#if UNITY_EDITOR
        [ExecuteInEditMode]
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;

            if (!Application.isPlaying)
            {
                spawnPos = player.position;
            }
            
            Gizmos.DrawWireSphere(spawnPos - (Vector3)spawnOffset, 5);
        }
#endif

        //Methods
        void SpawnModule()
        {
            if (currentModules.Count >= maxModules)
            {
                RemoveOldModules();
                return;
            }
            
            int moduleNumber = Random.Range(0, modules.Length - 1);

            GameObject newModule = Instantiate(modules[moduleNumber]);

            Vector3 pos = spawnPos - (Vector3)spawnOffset;
            pos.z = player.position.z + spawnDistance;
            newModule.transform.position = pos;

            currentModules.Add(newModule.transform);

            timer = spawnTimer;
        }
        void RemoveOldModules()
        {
            foreach (var module in currentModules)
            {
                if (module.position.z < player.position.z - despawnDistance)
                {
                    currentModules.Remove(module);
                    Destroy(module.gameObject);
                    return;
                }
            }
        }
    }
}
