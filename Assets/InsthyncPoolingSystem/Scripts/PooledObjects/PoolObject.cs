using UnityEngine;

namespace Insthync.PoolingSystem
{
    public class PoolObject : MonoBehaviour
    {
        [HideInInspector]
        public PoolingSystem poolingSystem;

        void OnDisable()
        {
            transform.position = Vector3.zero;

            if (poolingSystem)
                poolingSystem.AddToAvailableObjects(gameObject);
            else
                Debug.LogWarning("There are no pooling system for " + name);
        }
    }
}