using UnityEngine;
using Utility;

namespace UI
{
    public class UIController : MonoBehaviour
    {
        public Canvas worldSpaceCanvas;
        private void Awake()
        {
            ServiceLocator.Add(this);
        }
    }
}
