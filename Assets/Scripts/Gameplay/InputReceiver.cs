using UnityEngine;

namespace NAMESPACENAME.Gameplay.Ship
{
    public class InputReceiver : MonoBehaviour
    {
        [Header("Set Values")]
        //[SerializeField] Joystick joystick;
        [SerializeField] ShipController ship;
        [SerializeField] Vector2 screenCenterOffset;
        [SerializeField] bool useDefaultOffset;
        [Header("Runtime Values")]
        [SerializeField] Vector2 input;
        [SerializeField] bool usingTouch;

        //Unity Events
        private void Start()
        {
            usingTouch = Input.touchSupported;
            if (useDefaultOffset)
            {
                screenCenterOffset = Camera.main.transform.position;
            }
        }
        private void Update()
        {
            //input = joystick.Direction;
            GetTouch();

            ship.GetInputX(input.x);
            ship.GetInputY(input.y);
        }

        //Methods
        void GetTouch()
        {
            bool touchThisFrame;
            touchThisFrame = usingTouch ? Input.touchCount > 0 : Input.GetMouseButtonDown(0);

            if (touchThisFrame)
            {
                //Get Mouse
                Vector3 touchPos;
                touchPos = usingTouch ? Input.GetTouch(0).position : Input.mousePosition;
                touchPos.z = Camera.main.nearClipPlane;
                if (touchPos.z < 1)
                {
                    touchPos.z = 1;
                }

                //Apply to world coordinates
                input = (Vector2)Camera.main.ScreenToWorldPoint(touchPos) - screenCenterOffset;
            }
        }
    }
}
