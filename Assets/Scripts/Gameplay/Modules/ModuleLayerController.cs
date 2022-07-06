using UnityEngine;

namespace NAMESPACENAME.Gameplay.Modules
{
    public class ModuleLayerController : MonoBehaviour
    {
        [Header("Set Values")]
        [SerializeField] Transform player;
        [SerializeField] LayerMask playerLayer;
        [SerializeField] Vector3 size;
        [Header("Runtime Values")]
        public int moduleID;

        public System.Action<int> PlayerReached;

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
        }
        private void Update()
        {
            CheckForPlayer();
        }

#if UNITY_EDITOR
        [ExecuteInEditMode]
        private void OnDrawGizmos()
        {
            Color newColor = Color.yellow;
            newColor.a /= 3;
            Gizmos.color = newColor;

            Gizmos.DrawCube(transform.position, size);
        }
#endif

        //Methods
        void CheckForPlayer()
        {
            //Get check area
            Vector3 pos = transform.position;
            Vector3 radius = size/2;
            Quaternion rot = transform.rotation;
            
            //Check for player
            Collider[] colls = Physics.OverlapBox(pos, radius, rot, playerLayer);
            
            //Search player between collider
            foreach (Collider coll in colls)
            {
                if (coll.transform != player) continue;

                PlayerReached.Invoke(moduleID);
                return;
            }
        }
    }
}
