using UnityEngine;

namespace Insthync.PoolingSystem
{
    public class PoolObjectTimingDisabler : PoolObject
    {
        float timer = 0;
        public float disableTime;

        void OnEnable()
        {
            timer = 0;
        }

        void Update()
        {
            timer += Time.deltaTime;

            if (timer > disableTime)
            {
                transform.parent = poolingSystem.transform;
                gameObject.SetActive(false);
            }
        }
    }
}