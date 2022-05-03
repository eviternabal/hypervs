using UnityEngine;

namespace NAMESPACENAME.Gameplay.Ship
{
    public class ShipController : MonoBehaviour
    { 
        [Header("Set Values")]
        [SerializeField] float XYspeed;
        [Header("Runtime Values")]
        [SerializeField] Vector2 moveInput;
        [SerializeField] Vector2 moveInputTEMP;

        //Unity Events
        private void Update()
        {
            moveInput = moveInputTEMP;

            if (moveInput.magnitude > 0)
            {
                Move();
            }
        }

        //Methods
        void Move()
        {
            transform.Translate(moveInput.normalized * XYspeed * Time.deltaTime);
            moveInput = Vector2.zero;
        }
        public void GetInputX(float input)
        {
            moveInputTEMP.x = input;
        }
        public void GetInputY(float input)
        {
            moveInputTEMP.y = input;
        }
    }
}
