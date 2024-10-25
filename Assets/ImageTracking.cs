using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARTrackedImageManager))]
public class imageTracking : MonoBehaviour
{
    [SerializeField]
    private GameObject[] placedPrefab;

    [SerializeField]
    private float prefabScale = 0.1f;

    [SerializeField]
    private XRReferenceImageLibrary ReferenceImages;

    private Dictionary<string, GameObject> spawnedPrefab = new Dictionary<string, GameObject>();
    private ARTrackedImageManager trackedImageManager;
    private void Awake()
    {
        trackedImageManager = FindFirstObjectByType<ARTrackedImageManager>();
        foreach(GameObject prefab in placedPrefab)
        {
            GameObject newPrefab = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            newPrefab.name = prefab.name;
            spawnedPrefab.Add(prefab.name, newPrefab);
            newPrefab.SetActive(false);
        }
    }
    private void Start() {
        trackedImageManager.referenceLibrary = ReferenceImages;
    }
    private void OnEnable()
    {
        trackedImageManager.trackablesChanged.AddListener(imageChanged);
    }
    private void OnDisable()
    {
        trackedImageManager.trackablesChanged.RemoveListener(imageChanged);
    }
    private void imageChanged(ARTrackablesChangedEventArgs<ARTrackedImage> eventArgs)
    {
        foreach(ARTrackedImage trackedImage in eventArgs.added)
        {
            updateImage(trackedImage);
        }
        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            updateImage(trackedImage);
        }
        foreach (ARTrackedImage trackedImage in eventArgs.removed)
        {
            spawnedPrefab[trackedImage.name].SetActive(false);
        }
    }
    private void updateImage(ARTrackedImage trackedImage)
    {
        string name = trackedImage.referenceImage.name;

        GameObject prefab = spawnedPrefab[name];
        prefab.transform.parent = trackedImage.gameObject.transform;

        prefab.transform.localPosition = new Vector3(0, 0, 0);
        prefab.transform.localRotation = new Quaternion(0, 180, 180, 0);  
        prefab.transform.localScale = new Vector3(prefabScale, prefabScale, 0.01f);
        prefab.SetActive(true);
    }
    private void Update() {
    }
}