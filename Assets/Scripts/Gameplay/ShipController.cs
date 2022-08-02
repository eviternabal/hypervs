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
        private GameManager gm;
        private void Start()
        {
            Ring.IncrementSpeed += IncrementForwardSpeed;
            GameManager.DecrementShipSpeed += DecrementForwardSpeed;
            meshRenderer = GetComponent<MeshRenderer>();
            gm = GameObject.Find("Gameplay Manager").GetComponent<GameManager>();
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
            Debug.Log(other.transform.CompareTag("Obstacle"));

            if (other.transform.CompareTag("Obstacle"))
            {
                if (!hasCollided)
                {
                    FindObjectOfType<AudioManager>().Play("ShipHit");
                    DecrementForwardSpeed(Mathf.Ceil(forwardSpeed / 2));
                    hitCounter++;
                    DestroyTrail();
                    hasCollided = true;
                    meshRenderer.sharedMaterial = materials[1];
                }
            }
            
        }

        private int hitCounter = 0;
        [SerializeField] GameObject trail_one;
        [SerializeField] GameObject trail_two;        
        private void DestroyTrail()
        {
            switch (hitCounter)
            {
                
                case 1:
                    trail_one.gameObject.SetActive(false);
                    Debug.Log("Golpeo 1");
                    break;
                case 2: 
                    trail_two.gameObject.SetActive(false);
                    Debug.Log("Golpeo 2");
                    break;
                case 3:
                    gm.canLose = true;
                    gm.GameOverByHits();
                    Debug.Log("Perdes por hits");
                    break;
                    
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
                    currentISeconds += Time.deltaTime; 
                }
                else
                {
                    meshRenderer.sharedMaterial = materials[0];
                    hasCollided = false;
                    currentISeconds = 0;
                }
            }
        }        
    }    
}
