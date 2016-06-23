using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace Insthync.PoolingSystem
{
    public class TimingDisabler : MonoBehaviour 
    {
        public float disableTime;
        public UnityEvent onDisable;

        float timer = 0;
        void OnEnable()
        {
            timer = 0;
        }

        void Update()
        {
            timer += Time.deltaTime;

            if (timer > disableTime)
            {
                RaiseOnDisable();
                gameObject.SetActive(false);
            }
        }

        void RaiseOnDisable()
        {
            if (onDisable != null)
                onDisable.Invoke();
        }
    }
}
