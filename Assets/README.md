# insthync-pooling-system

The simple pooling system that allow you to manage pooling system via codes and editor<br>
<br>
# Class: PoolingSystem
# Variable<br>
- <b>GameObject poolingObject</b> - the object you want to make pool
- <b>string poolGroupName</b> - if you define its group name and added it to group list when you try to create new pooling system with detected group it won't creating new group but using old pooling system group
- <b>int startPoolSize</b> - start pool size
- <b>bool autoResize</b> - resizing pool size if creating object is over than current pool size
- <b>int maxPoolSize</b> - max pool size that can resizing
- <b>bool createPoolOnAwake</b> - create pool on game object awake
- <b>bool includedInGroup</b> - adding to group list with defined group name

# Functions<br>
- static PoolingSystem CreatePoolingSystem(GameObject poolingObject, int startPoolSize, bool autoResize = false, int maxPoolSize = 0, bool createPoolImmediately = true, bool includedInGroup = true)
- static PoolingSystem CreatePoolingSystem(GameObject poolingObject, string groupName, int startPoolSize, bool autoResize = false, int maxPoolSize = 0, bool createPoolImmediately = true, bool includedInGroup = true)
<br>Create new pooling system instance<br>
- static void ClearPoolGroup()
<br>Clear all pool groups<br>
- void SetProperties(GameObject poolingObject, string groupName, int startPoolSize, bool autoResize, int maxPoolSize)
<br>Set generic properties<br>
- void InstantiatePool()
<br>Instantiate/Re Instantiate pooling system<br>
- bool TryGetNextObject(Vector3 pos, Quaternion rot, out GameObject result)
- GameObject GetNextObject(Vector3 pos, Quaternion rot)
<br>Get available pooled objects<br>
- void ClearPool()
<br>Clear all pooled objects<br>
- void DeletePool(bool deleteActiveObjects)
<br>Delete pooled objects<br>
- void AddToAvailableObjects(GameObject obj)
<br>Add to available list should be call after pooled object inactive<br>
