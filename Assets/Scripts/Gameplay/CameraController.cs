using UnityEngine;

namespace NAMESPACENAME.Gameplay.Ship
{
    public class CameraController : MonoBehaviour
    {
        [Header("Set Values")]
        [SerializeField] Transform player;
        [SerializeField][Tooltip("x as min & y as max")] Vector2 xRange;
        [SerializeField][Tooltip("x as min & y as max")] Vector2 yRange;
        [Header("Runtime Values")]
        [SerializeField] Vector2 originalPos;
        [SerializeField] Vector2 originalDis;
        [SerializeField] float zDistanceToPlayer;

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

            zDistanceToPlayer = player.position.z - transform.position.z;
            originalPos = transform.position;
            originalDis = player.position - transform.position;
        }
#if UNITY_EDITOR
        [ExecuteInEditMode]
        private void OnDrawGizmos()
        {
            if (!Application.isPlaying)
            {
                originalPos = transform.position;
            }

            Color newColor = Color.green;
            newColor.a /= 3;
            Gizmos.color = newColor;


            //Calculate map size
            Vector2 cameraLimit = Vector2.zero;
            cameraLimit.x = xRange.y - xRange.x;
            cameraLimit.y = yRange.y - yRange.x;

            //Calculate map center pos
            Vector3 pos = Vector3.zero;
            pos.x = xRange.x + cameraLimit.x / 2;
            pos.y = yRange.x + cameraLimit.y / 2;
            pos.z = player.position.z;

            //Draw
            Gizmos.DrawCube(pos, cameraLimit);
        }
#endif

        //Methods
        void Move()
        {
            //Get current pos
            Vector3 newPos = player.position - (Vector3)originalDis;
            
            //Move in Z
            newPos.z = player.position.z - zDistanceToPlayer;

            //Get camera limits
            Vector2 topRightPos = Vector2.zero;
            topRightPos += xRange.y * (Vector2)transform.right;
            topRightPos += yRange.y * (Vector2)transform.up;
            Vector2 bottomLeftPos = Vector2.zero;
            bottomLeftPos += xRange.x * (Vector2)transform.right;
            bottomLeftPos += yRange.x * (Vector2)transform.up;

            //Move X within limits
            if (newPos.x > topRightPos.x)
            {
                    newPos.x = topRightPos.x;
            }
            else if (newPos.x < bottomLeftPos.x)
            {
                newPos.x = bottomLeftPos.x;
            }

            //Move Y within limits
            if (newPos.y > topRightPos.y)
            {
                newPos.y = topRightPos.y;
            }
            else if (newPos.y < bottomLeftPos.y)
            {
                newPos.y = bottomLeftPos.y;
            }

            //Set New pos
            transform.position = newPos;
        }

        //Event Receivers
        void OnShipMoved()
        {
            Move();
        }
    }
}
