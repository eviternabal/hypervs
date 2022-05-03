using UnityEngine;

namespace NAMESPACENAME.Gameplay.Ship
{
    public class InputReceiver : MonoBehaviour
    {
        [Header("Set Values")]
        //[SerializeField] Joystick joystick;
        [SerializeField] Vector2 screenCenterOffset;
        [Header("Runtime Values")]
        [SerializeField] Vector2 input;

        //Unity Events
        private void Update()
        {
            //input = joystick.Direction;

            GetTouch();
        }

        //Methods
        void GetTouch()
        {
            if (Input.touchCount > 0)
            {
                Vector2 touchPos = Input.GetTouch(0).position;

                input = (Vector2)Camera.main.ScreenToWorldPoint(touchPos) - screenCenterOffset;

                // Update the Text on the screen depending on current position of the touch each frame
                Debug.Log("Touch Position: " + touchPos);
            }
            else
            {
                //Debug.Log("Not Touching");
            }
        }
    }
}
