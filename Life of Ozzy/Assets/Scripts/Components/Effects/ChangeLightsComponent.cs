using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace LifeOfOzzy.Components
{
    public class ChangeLightsComponent : MonoBehaviour
    {
        [SerializeField] private Light2D[] _lights;

        [ColorUsage(true, true)][SerializeField] private Color _color;

        [ContextMenu("Set color")]
        public void SetColor()
        {
            foreach (var Light2D in _lights)
            {
                Light2D.color = _color;
            }
        }

    }

}
