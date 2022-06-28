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

            //Calculate map center pos
            Vector3 pos = originalPos;
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

            //Get player side
            bool beyondX = (newPos.x > originalPos.x + originalDis.x);
            bool beyondY = (newPos.y > originalPos.y + originalDis.y);

            //Move in Z
            newPos.z = player.position.z - zDistanceToPlayer;
            
            //Get camera limits
            Vector2 fixedRadius = cameraLimit / 2 + originalPos;

            //Move X within limits
            if (Mathf.Pow(newPos.x, 2) > Mathf.Pow(fixedRadius.x, 2))
            {
                if(beyondX)
                {
                    newPos.x = fixedRadius.x;
                }
                else
                {
                    newPos.x = originalPos.x - cameraLimit.x / 2;
                }
            }

            //Move Y within limits
            if (Mathf.Pow(newPos.y, 2) > Mathf.Pow(fixedRadius.y, 2))
            {
                if (beyondY)
                {
                    newPos.y = fixedRadius.y;
                }
                else
                {
                    newPos.y = originalPos.y - cameraLimit.y / 2;// - originalPos.y;
                }
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
