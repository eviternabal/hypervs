using UnityEngine;

namespace NAMESPACENAME.Gameplay.Ship
{
    public class CameraController : MonoBehaviour
    {
        [Header("Set Values")]
        [SerializeField] Transform player;
        [SerializeField] Vector2 cameraLimit;
        [Header("Runtime Values")]
        [SerializeField] Vector2 originalPos;
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
            originalPos = transform.position;
        }

        //Methods
        void Move()
        {
            Vector3 newPos = transform.position;
            newPos.z = player.position.z - distanceToPlayer;
            Vector2 fixedLimits = cameraLimit + originalPos;
            if(Mathf.Pow(newPos.x, 2) > Mathf.Pow(fixedLimits, 2))
            {
                if(newPos.x > originalPos.x)
                {
                    newPos.x = fixedLimits.x;
                }
                else if(newPos.x < originalPos.x)
                {
                    newPos.x = cameraLimit.x - originalPos.x;
                }
            }
            transform.position = newPos;
        }

        //Event Receivers
        void OnShipMoved()
        {
            Move();
        }
    }
}
