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


        public System.Action ShipMoved;
        public System.Action ShipAdvanced;

        private void Start()
        {
            Ring.IncrementSpeed += IncrementForwardSpeed;
            GameManager.DecrementShipSpeed += DecrementForwardSpeed;
        }

        private void OnDisable()
        {
            Ring.IncrementSpeed -= IncrementForwardSpeed;
            GameManager.DecrementShipSpeed -= DecrementForwardSpeed;
        }

        //Unity Events
        private void Update()
        {
            MoveForward();

            if (moveInput.magnitude > 0)
            {
                MoveSides();
            }
        }

        //Methods
        void MoveForward()
        {
            //Move
            transform.Translate(transform.forward * forwardSpeed * Time.deltaTime);

            //Send Event
            ShipAdvanced?.Invoke();
        }
        void MoveSides()
        {
            //Move
            transform.Translate(moveInput * Time.deltaTime);
            
            //Reset input
            moveInput = Vector2.zero;

            //Send event
            ShipMoved();
        }
        public void GetInputX(float input)
        {
            moveInput.x = input * XYSpeed;
        }
        public void GetInputY(float input)
        {
            moveInput.y = input * XYSpeed;
        }

        private void IncrementForwardSpeed(float value)
        {
            forwardSpeed += value;
        }

        private void DecrementForwardSpeed(float value)
        {
            forwardSpeed -= value;
        }
    }
}
