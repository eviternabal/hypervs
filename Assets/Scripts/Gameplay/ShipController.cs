using UnityEngine;

namespace NAMESPACENAME.Gameplay.Ship
{
    public class ShipController : MonoBehaviour
    { 
        [Header("Set Values")]
        [SerializeField] float XYspeed;
        [SerializeField] float forwardSpeed;
        [Header("Runtime Values")]
        [SerializeField] Vector3 moveInput;
        [SerializeField] Vector2 moveInputTEMP;
        [SerializeField] private float timeToDecrementForward;
        [SerializeField] private float decrementForwardValue;

        private float timerDecrementForward;

        //Unity Events

        private void Start()
        {
            Ring.IncrementSpeed += IncrementForwardSpeed;
        }

        private void OnDisable()
        {
            Ring.IncrementSpeed -= IncrementForwardSpeed;
        }

        private void Update()
        {
            if (timerDecrementForward < timeToDecrementForward)
                timerDecrementForward += Time.deltaTime;
            else
            {
                forwardSpeed -= decrementForwardValue;
                timerDecrementForward = 0.0f;
            }

            moveInput = moveInputTEMP;
            moveInput.z = forwardSpeed;

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

        public void IncrementForwardSpeed(float value)
        {
            forwardSpeed += value;
        }
    }
}
