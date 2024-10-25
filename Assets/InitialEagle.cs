using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.EventSystems;

[RequireComponent(typeof(ARTrackedImageManager))]
public class InitialEagle : MonoBehaviour
{
    [SerializeField]
    private GameObject eaglePrefab;
    [SerializeField]
    private imageTracking nextAnimation;
    private ARTrackedImageManager trackedImageManager;
    private GameObject spawnedPrefab;
    [SerializeField]
    private float prefabScale = 0.1f;


    // Start is called before the first frame update
    void Awake() {
        trackedImageManager = FindFirstObjectByType<ARTrackedImageManager>();
        spawnedPrefab = Instantiate(eaglePrefab, Vector3.zero, Quaternion.identity);
        spawnedPrefab.SetActive(false);
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
            spawnedPrefab.SetActive(false);
        }
    }
    private void updateImage(ARTrackedImage trackedImage)
    {
        spawnedPrefab.transform.parent = trackedImage.gameObject.transform;
        spawnedPrefab.transform.localRotation = new Quaternion(0, 180, 180, 0);  
        spawnedPrefab.transform.localScale = new Vector3(prefabScale, prefabScale, 0.01f);
        spawnedPrefab.SetActive(true);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 0 || Input.GetTouch(0).phase != TouchPhase.Began) { return; }
        //check if the button is clicked
        if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)) {
            Proceed();
        }
    }

    public void Proceed() {
        Debug.Log("Proceed");
        spawnedPrefab.SetActive(false);
        nextAnimation.enabled = true;
        enabled = false;
    }
}
