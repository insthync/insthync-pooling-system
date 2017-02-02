using UnityEngine;

namespace Insthync.PoolingSystem
{
    public class PoolObject : MonoBehaviour
    {
        [System.NonSerialized]
        public PoolingSystem poolingSystem;

        void OnDisable()
        {
            if (poolingSystem == null)
            {
                Debug.LogWarning("There are no pooling system for " + name);
                return;
            }

            transform.SetParent(poolingSystem.transform);
            transform.localPosition = Vector3.zero;

            if (poolingSystem)
                poolingSystem.AddToAvailableObjects(gameObject);
        }
    }
}