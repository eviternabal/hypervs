using UnityEngine;
using Universal.Singletons;

namespace NAMESPACENAME
{
    public class ScreenManager : MonoBehaviourSingleton<ScreenManager>
    {
        //[Header("Set Values")]
        //[Header("Runtime Values")]

        //Unity Events
        private void Start()
        {
            Screen.orientation = ScreenOrientation.LandscapeLeft;
        }

        //Methods
    }
}
