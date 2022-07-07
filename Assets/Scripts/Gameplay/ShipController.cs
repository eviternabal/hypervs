using UnityEngine;

namespace NAMESPACENAME.Gameplay.Ship
{
    public class ShipController : MonoBehaviour
    { 
        [Header("Set Values")]
        [SerializeField] float XYSpeed;
        [SerializeField] float forwardSpeed;
        [SerializeField] float maxForwardSpeed;
        [Header("Runtime Values")]
        [SerializeField] Vector3 moveInput;

        public System.Action ShipMoved;
        public System.Action ShipAdvanced;
        public float GetForwardSpeed { get { return forwardSpeed; } }

        private MeshRenderer meshRenderer;
        private void Start()
        {
            Ring.IncrementSpeed += IncrementForwardSpeed;
            GameManager.DecrementShipSpeed += DecrementForwardSpeed;
            meshRenderer = GetComponent<MeshRenderer>();
        }

        private void OnDisable()
        {
            Ring.IncrementSpeed -= IncrementForwardSpeed;
            GameManager.DecrementShipSpeed -= DecrementForwardSpeed;
        }

        //Unity Events
        private void Update()
        {
            ISecondCount();
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
            if (forwardSpeed < maxForwardSpeed)
            {
                forwardSpeed += value;
            }
            
        }

        private void DecrementForwardSpeed(float value)
        {
            if(forwardSpeed > 0)
                forwardSpeed -= value;
        }

        [SerializeField] Material[] materials;

        private void OnTriggerExit(Collider other)
        {
            if (other.transform.CompareTag("Obstacle"))
            {
                if (!hasCollided)
                {
                    
                    DecrementForwardSpeed(Mathf.Ceil(forwardSpeed / 2));
                    hasCollided = true;
                    
                }
            }
            
        }
        
        private bool hasCollided = false;
        private float maxISeconds = 2;
        private float currentISeconds = 0;
        private void ISecondCount()
        {
            if (hasCollided)
            {
            
            
                if (currentISeconds < maxISeconds)
                {
                    meshRenderer.material = materials[1];
                    currentISeconds += Time.deltaTime; 
                    Debug.Log(currentISeconds);
                }
                if (currentISeconds >= maxISeconds)
                {
                    meshRenderer.material = materials[0];
                    hasCollided = false;
                }
            }
        }
        
    }
    
}
