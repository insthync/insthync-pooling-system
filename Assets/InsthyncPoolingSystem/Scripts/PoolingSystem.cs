using UnityEngine;
using System.Collections.Generic;

namespace Insthync.PoolingSystem
{
    public class PoolingSystem : MonoBehaviour
    {
        protected const string PoolingSystenPrefix = "_Insthync_PoolingSystem_";
        protected const string IdPrefix = "_Insthync_PoolingId_";
        protected static Dictionary<string, PoolingSystem> poolGroups = new Dictionary<string, PoolingSystem>();
        public GameObject poolingObject;
        public string poolGroupName;
        public int startPoolSize = 100;
        public bool autoResize = false;
        [Range(0, 1000)]
        public int maxPoolSize = 0;
        public bool createPoolOnAwake;
        public bool includedInGroup;

        protected List<GameObject> pooledObjects;
        protected List<GameObject> availableObjects;

        protected static GameObject poolingContainer;
        public static GameObject PoolingContainer
        {
            get
            {
                if (poolingContainer == null)
                    poolingContainer = new GameObject("PoolingContainer");
                return poolingContainer;
            }
        }

        public static PoolingSystem CreatePoolingSystem(GameObject poolingObject, int startPoolSize, bool autoResize = false, int maxPoolSize = 0, bool createPoolImmediately = true, bool includedInGroup = true)
        {
            return CreatePoolingSystem(poolingObject, IdPrefix + poolingObject.GetInstanceID(), startPoolSize, autoResize, maxPoolSize, createPoolImmediately, includedInGroup);
        }

        public static PoolingSystem CreatePoolingSystem(GameObject poolingObject, string groupName, int startPoolSize, bool autoResize = false, int maxPoolSize = 0, bool createPoolImmediately = true, bool includedInGroup = true)
        {
            if (includedInGroup)
            {
                if (poolGroups.ContainsKey(groupName))
                    return poolGroups[groupName];
                else
                {
                    GameObject poolSystemObject = new GameObject(PoolingSystenPrefix + groupName);
                    PoolingSystem poolSystem = poolSystemObject.AddComponent<PoolingSystem>();
                    poolSystem.createPoolOnAwake = false;
                    poolSystem.SetProperties(poolingObject, groupName, startPoolSize, autoResize, maxPoolSize);
                    poolGroups.Add(groupName, poolSystem);

                    if (createPoolImmediately)
                        poolSystem.InstantiatePool();

                    poolSystemObject.transform.SetParent(PoolingContainer.transform);

                    return poolSystem;
                }
            }
            else
            {
                GameObject poolSystemObject = new GameObject(PoolingSystenPrefix + groupName);
                PoolingSystem poolSystem = poolSystemObject.AddComponent<PoolingSystem>();
                poolSystem.createPoolOnAwake = false;
                poolSystem.SetProperties(poolingObject, groupName, startPoolSize, autoResize, maxPoolSize);

                if (createPoolImmediately)
                    poolSystem.InstantiatePool();

                poolSystemObject.transform.SetParent(PoolingContainer.transform);

                return poolSystem;
            }
        }

        public static PoolingSystem GetPoolByGroup(string groupName)
        {
            if (poolGroups.ContainsKey(groupName))
                return poolGroups[groupName];
            return null;
        }

        public static void ClearPoolGroup()
        {
            foreach (PoolingSystem poolingSystem in poolGroups.Values)
                poolingSystem.ClearPool();
            poolGroups.Clear();
        }

        void Awake()
        {
            if (createPoolOnAwake)
            {
                pooledObjects = new List<GameObject>();
                availableObjects = new List<GameObject>();
                InstantiatePool();
            }

            if (includedInGroup)
                poolGroups.Add(poolGroupName, this);
        }

        #region Instance Functions
        
        public void SetProperties(GameObject poolingObject, string groupName, int startPoolSize, bool autoResize, int maxPoolSize)
        {
            this.poolingObject = poolingObject;
            this.poolGroupName = groupName;
            this.startPoolSize = startPoolSize;
            this.autoResize = autoResize;
            this.maxPoolSize = maxPoolSize;
            this.pooledObjects = new List<GameObject>();
            this.availableObjects = new List<GameObject>();
        }
        
        public void InstantiatePool()
        {
            ClearPool();

            for (int i = 0; i < startPoolSize; ++i)
            {
                GameObject newObject = NewPooledObjects();
                newObject.SetActive(false);
                pooledObjects.Add(newObject);
                availableObjects.Add(newObject);
            }
        }
        
        public bool TryGetNextObject(Vector3 pos, Quaternion rot, out GameObject result)
        {
            result = GetNextObject(pos, rot);
            return result != null;
        }
        
        public GameObject GetNextObject(Vector3 pos, Quaternion rot)
        {
            GameObject result = null;
            if (pooledObjects.Count == 0)
                Debug.LogWarning("Pooled object " + poolGroupName + ", the pool has not been instantiated but you are trying to retrieve an object!");
            
            for (int i = availableObjects.Count - 1; i >= 0; --i)
            {
                GameObject availableObject = availableObjects[i];
                if (availableObject  == null)
                {
                    Debug.LogError("Pooled object " + poolGroupName + " has missing objects in its pool! Are you accidentally destroying any GameObjects retrieved from the pool?");
                    continue;
                }
                availableObject.transform.position = pos;
                availableObject.transform.rotation = rot;
                availableObject.SetActive(true);
                result = availableObject;
                availableObjects.RemoveAt(i);
                return result;
            }

            if (autoResize && (pooledObjects.Count < maxPoolSize || maxPoolSize <= 0))
            {
                GameObject newObject = NewPooledObjects();
                newObject.transform.position = pos;
                newObject.transform.rotation = rot;
                newObject.SetActive(true);
                pooledObjects.Add(newObject);
                result = newObject;
            }

            return result;
        }

        GameObject NewPooledObjects()
        {
            GameObject newObject = Instantiate(poolingObject);
            newObject.transform.SetParent(transform);
            PoolObject pooledObject = newObject.GetComponent<PoolObject>();
            if (pooledObject == null)
                pooledObject = newObject.AddComponent<PoolObject>();
            pooledObject.poolingSystem = this;
            return newObject;
        }
        
        public void ClearPool()
        {
            for (int i = pooledObjects.Count - 1; i >= 0; --i)
                Destroy(pooledObjects[i]);

            pooledObjects.Clear();
            availableObjects.Clear();
        }
        
        public void DeletePool(bool deleteActiveObjects)
        {
            for (int i = pooledObjects.Count - 1; i >= 0; --i)
            {
                if (!pooledObjects[i].activeInHierarchy || (pooledObjects[i].activeInHierarchy && deleteActiveObjects))
                    Destroy(pooledObjects[i]);
            }
        }
        
        public void AddToAvailableObjects(GameObject obj)
        {
            if (!availableObjects.Contains(obj))
                availableObjects.Add(obj);
        }
        #endregion

        #region Statistics
        public int ActiveObjectCount()
        {
            return pooledObjects.Count - availableObjects.Count;
        }

        public int AvailableObjectCount()
        {
            return availableObjects.Count;
        }
        #endregion
    }
}
