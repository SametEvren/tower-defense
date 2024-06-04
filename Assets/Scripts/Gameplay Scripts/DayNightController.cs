using UnityEngine;
using DG.Tweening;

namespace Gameplay_Scripts
{
    public class DayNightController : MonoBehaviour
    {
        public Light directionalLight;

        [SerializeField] private Color dayColorLight;
        [SerializeField] private Color nightColorLight;

        [SerializeField] private MeshRenderer[] waterTiles;
        
        [SerializeField] private Color dayColorWater;
        [SerializeField] private Color nightColorWater;

        [SerializeField] private float transitionDuration = 1.0f;
        
        internal void MakeItDay()
        {
            directionalLight.DOColor(dayColorLight, transitionDuration);

            foreach (var waterTile in waterTiles)
            {
                waterTile.material.DOColor(dayColorWater, "_BaseColor", transitionDuration);
            }
        }

        internal void MakeItNight()
        {
            directionalLight.DOColor(nightColorLight, transitionDuration);

            foreach (var waterTile in waterTiles)
            {
                waterTile.material.DOColor(nightColorWater, "_BaseColor", transitionDuration);
            }
        }
    }
}