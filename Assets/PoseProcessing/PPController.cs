using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


namespace zzz
{
    public class PPController : MonoBehaviour
    {
        private Volume volume;
        private Vignette vignette;

        public bool shouldDO;
        public bool shouldBack;
        [SerializeField] private Color color;
        [SerializeField] private float speed;
        [SerializeField] private float maxValue;

        private void Awake()
        {
            volume = GetComponent<Volume>();
            volume.profile.TryGet<Vignette>(out vignette);
            
        }

        public void SetSpeedAndMaxValue(float speed,float maxValue)
        {
            this.speed = speed;
            this.maxValue = maxValue;
            shouldDO = true;
        }

        private void Update()
        {
            if (shouldDO && !shouldBack)
            {
                vignette.intensity.value += speed * Time.unscaledDeltaTime;
                vignette.color.value = color;

                if (vignette.intensity.value > maxValue)
                {
                    shouldDO = false;
                    shouldBack = true;
                }
            }
            if (shouldBack)
            {
                vignette.intensity.value -= 3.5f * speed * Time.unscaledDeltaTime;
                if(vignette.intensity.value <= 0.05f)
                {
                    shouldBack = false;
                }
            }
        }
    }
}
