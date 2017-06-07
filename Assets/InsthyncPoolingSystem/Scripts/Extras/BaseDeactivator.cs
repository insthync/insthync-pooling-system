using UnityEngine;
using UnityEngine.Events;

namespace Insthync.PoolingSystem
{
    public abstract class BaseDeactivator : MonoBehaviour
    {
        public bool isForceDeactivate;
        public UnityEvent onDisable;
        private PoolObject poolObject;
        protected virtual void Awake()
        {
            poolObject = GetComponent<PoolObject>();
        }

        protected virtual void Update()
        {
            UpdateLogic();
            if (IsDeactivating() || isForceDeactivate)
            {
                isForceDeactivate = false;
                if (poolObject != null)
                    poolObject.Disable();
                else
                    gameObject.SetActive(false);
            }
        }

        protected virtual void OnEnable()
        {
            isForceDeactivate = false;
        }

        protected virtual void OnDisable()
        {
            RaiseOnDisable();
        }

        void RaiseOnDisable()
        {
            if (onDisable != null)
                onDisable.Invoke();
        }

        public void ForceDeactive()
        {
            isForceDeactivate = true;
        }

        public abstract void UpdateLogic();
        public abstract bool IsDeactivating(); 
    }
}