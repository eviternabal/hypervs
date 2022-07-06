using System.Collections.Generic;
using UnityEngine;

namespace NAMESPACENAME.Gameplay.Modules
{
    public class ModulesManager : MonoBehaviour
    {
        [System.Serializable]
        class Module
        {
            public ModuleLayerController mainLayerController;
            public Transform module;
            public float removeTimer;
            public int id;

            public Module(ModuleLayerController _layer, Transform _module, int _id)
            {
                mainLayerController = _layer;
                module = _module;
                id = _id;

                mainLayerController.moduleID = id;
                removeTimer = 0;
            }
        }

        [Header("Set Values")]
        [SerializeField] GameObject[] modules;
        [SerializeField] Transform player;
        [SerializeField] Vector2 spawnOffset;
        [SerializeField] float spawnDistance;
        [SerializeField] float firstSpawnTimer;
        [SerializeField] float spawnTimer;
        [SerializeField] float despawnTimer;
        [SerializeField] int maxModules;
        [Header("Runtime Values")]
        [SerializeField] List<Module> currentModules;
        [SerializeField] List<Module> removingModules;
        [SerializeField] Vector3 spawnPos;
        [SerializeField] float timer;
        [SerializeField] bool readyToSpawn;
        Dictionary<int, Module> moduleIndexes;

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

            moduleIndexes = new Dictionary<int, Module>();

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
            RemoveOldModules();
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
            if (!readyToSpawn) return;
            if (currentModules.Count >= maxModules)
            {
                return;
            }

            readyToSpawn = false;

            int moduleNumber = Random.Range(0, modules.Length - 1);

            GameObject newModule = Instantiate(modules[moduleNumber]);

            Vector3 pos = spawnPos - (Vector3)spawnOffset;
            pos.z = player.position.z + spawnDistance;
            newModule.transform.position = pos;

            ModuleLayerController moduleLayer;
            moduleLayer = newModule.GetComponentInChildren<ModuleLayerController>();

            int id;
            do
            {
                id = Random.Range(0, int.MaxValue);
            } while (moduleIndexes.ContainsKey(id));
            
            Module module;
            module = new Module(moduleLayer, newModule.transform, id);

            currentModules.Add(module);
            moduleIndexes.Add(id, module);

            module.mainLayerController.PlayerReached += OnPlayerReachedLayer;

            timer = spawnTimer;
        }
        void RemoveOldModules()
        {
            foreach (var module in removingModules)
            {
                if (module.removeTimer > 0)
                {
                    module.removeTimer -= Time.deltaTime;
                    continue;
                }

                //Remove from lists
                currentModules.Remove(module);
                removingModules.Remove(module);
                moduleIndexes.Remove(module.id);

                //Destroy
                Destroy(module.module.gameObject);
                return;
            }
        }

        //Event Receivers
        void OnPlayerReachedLayer(int moduleID)
        {
            Module moduleToRemove;
            if (!moduleIndexes.TryGetValue(moduleID, out moduleToRemove)) return;
            if (removingModules.Contains(moduleToRemove)) return;

            removingModules.Add(moduleToRemove);
            moduleToRemove.removeTimer = despawnTimer;

            readyToSpawn = true;
            timer = spawnTimer;
        }
    }
}
