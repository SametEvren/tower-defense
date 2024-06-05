using System.Collections.Generic;
using Gameplay_Scripts;
using UnityEngine;
using Utility;

namespace UI
{
    public class UIController : MonoBehaviour
    {
        public Canvas worldSpaceCanvas;
        public GameObject tapToStart;
        public List<GameObject> onStartObjects;

        private GameplayController GameplayController => ServiceLocator.Get<GameplayController>();
        private void Awake()
        {
            ServiceLocator.Add(this);
            SetStartUI();
        }

        

        private void Start()
        {
            GameplayController.OnGameStarted += SetInGameUI;
        }

        private void SetStartUI()
        {
            tapToStart.SetActive(true);

            foreach (var uiObject in onStartObjects)
            {
                uiObject.SetActive(false);
            }
        }
        
        private void SetInGameUI()
        {
            tapToStart.SetActive(false);

            foreach (var uiObject in onStartObjects)
            {
                uiObject.SetActive(true);
            }
        }
    }
}
