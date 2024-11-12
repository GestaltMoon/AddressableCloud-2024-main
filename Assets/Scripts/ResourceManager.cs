using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ResourceManager : MonoBehaviour
{
    public AssetReference assetReference;
    AsyncOperationHandle<GameObject> handle;
    List<GameObject> prefabs = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(prefabs.Count <= 0) LoadAsset();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(prefabs.Count > 0) UnloadAsset();
        }
    }

    void LoadAsset()
    {
        handle = assetReference.InstantiateAsync(transform.position, Quaternion.identity);
        handle.Completed += handle => {prefabs.Add(handle.Result);};
    }

    void UnloadAsset()
    {
        foreach(GameObject prefab in prefabs)
        {
            Addressables.ReleaseInstance(prefab);
        }
        prefabs.Clear();
    }
}
