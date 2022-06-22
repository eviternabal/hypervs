using UnityEngine;

namespace NAMESPACENAME.Gameplay
{
    public class GameplayManager : MonoBehaviour
    {
        [Header("Set Values")]
        [SerializeField] Transform player;
        [SerializeField] [Tooltip("x as min & y as max")] Vector2 xRange;
        [SerializeField] [Tooltip("x as min & y as max")] Vector2 yRange;

        //Unity Events
        private void Start()
        {
            //Get player if null
            if (!player)
            {
                player = GameObject.FindGameObjectWithTag("Player").transform;
            }

            //Get ship and link event
            Ship.ShipController ship = player.GetComponent<Ship.ShipController>();
            if (ship)
            {
                ship.ShipMoved += OnShipMoved;
            }
        }
#if UNITY_EDITOR
        [ExecuteInEditMode]
        private void OnDrawGizmos()
        {
            Color newColor = Color.white;
            newColor.a /= 3;
            Gizmos.color = newColor;

            //Calculate map size
            Vector2 mapSize = Vector2.zero;
            mapSize.x = xRange.y - xRange.x;
            mapSize.y = yRange.y - yRange.x;

            //Calculate map center pos
            Vector3 pos = Vector3.zero;
            pos.x = xRange.x + mapSize.x / 2;
            pos.y = yRange.x + mapSize.y / 2;
            pos.z = player.position.z;

            //Draw
            Gizmos.DrawCube(pos, mapSize);
        }
#endif

        //Methods

        //Event Receivers
        void OnShipMoved()
        {
            Vector3 fixedPos = player.position;
            if (player.position.x > xRange.y)
            {
                fixedPos.x = xRange.y;
            }
            else if (player.position.x < xRange.x)
            {
                fixedPos.x = xRange.x;
            }

            if (player.position.y > yRange.y)
            {
                fixedPos.y = yRange.y;
            }
            else if (player.position.y < yRange.x)
            {
                fixedPos.y = yRange.x;
            }

            player.position = fixedPos;
        }
    }
}
