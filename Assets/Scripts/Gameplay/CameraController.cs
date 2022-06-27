using UnityEngine;

namespace NAMESPACENAME.Gameplay.Ship
{
    public class CameraController : MonoBehaviour
    {
        [Header("Set Values")]
        [SerializeField] Transform player;
        [SerializeField] Vector2 cameraLimit;
        [Header("Runtime Values")]
        [SerializeField] float distanceToPlayer;

        //Unity Events
        private void Start()
        {
            //Get player if null
            if (!player)
            {
                player = GameObject.FindGameObjectWithTag("Player").transform;
            }

            //Get ship and link event
            ShipController ship = player.GetComponent<Ship.ShipController>();
            if (ship)
            {
                ship.ShipMoved += OnShipMoved;
                ship.ShipAdvanced += OnShipMoved;
            }

            distanceToPlayer = player.position.z - transform.position.z;
        }

        //Event Receivers
        void OnShipMoved()
        {
            Vector3 newPos = transform.position;
            newPos.z = player.position.z - distanceToPlayer;
            transform.position = newPos;
        }
    }
}
