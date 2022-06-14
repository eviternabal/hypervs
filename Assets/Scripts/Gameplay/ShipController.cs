using UnityEngine;

namespace NAMESPACENAME.Gameplay.Ship
{
    public class ShipController : MonoBehaviour
    { 
        [Header("Set Values")]
        [SerializeField] float XYSpeed;
        [SerializeField] float forwardSpeed;
        [Header("Runtime Values")]
        [SerializeField] Vector3 moveInput;

        //Unity Events
        private void Update()
        {
            moveInput.z = forwardSpeed;

            if (moveInput.magnitude > 0)
            {
                Move();
            }
        }

        //Methods
        void Move()
        {
            transform.Translate(moveInput * Time.deltaTime);
            moveInput = Vector2.zero;
        }
        public void GetInputX(float input)
        {
            moveInput.x = input * XYSpeed;
        }
        public void GetInputY(float input)
        {
            moveInput.y = input * XYSpeed;
        }
    }
}
