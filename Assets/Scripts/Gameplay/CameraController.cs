using UnityEngine;

namespace NAMESPACENAME.Gameplay.Ship
{
    public class CameraController : MonoBehaviour
    {
        [Header("Set Values")]
        [SerializeField] Transform player;
        [Header("Runtime Values")]
        [SerializeField] float distanceToPlayer;

        //Unity Events
        private void Start()
        {
            distanceToPlayer = player.position.z - transform.position.z;
        }
        private void Update()
        {
            Vector3 newPos = transform.position;
            newPos.z = player.position.z - distanceToPlayer;
            transform.position = newPos;
        }

        //Methods
    }
}
