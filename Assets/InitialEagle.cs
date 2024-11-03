using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.XR;

[RequireComponent(typeof(ARTrackedImageManager))]
public class InitialEagle : MonoBehaviour
{
    [SerializeField]
    private GameObject eaglePrefab;
    private ImageTracking nextAnimation;
    private ARTrackedImageManager trackedImageManager;
    private GameObject spawnedPrefab;
    [SerializeField]
    private float prefabScale = 0.1f;
    private ManomotionManager manomotion;


    // Start is called before the first frame update
    void Awake() {
        manomotion = GameObject.Find("ManomotionManager").GetComponent<ManomotionManager>();

        nextAnimation = FindFirstObjectByType<ImageTracking>();
        trackedImageManager = FindFirstObjectByType<ARTrackedImageManager>();
        spawnedPrefab = Instantiate(eaglePrefab, Vector3.zero, Quaternion.identity);
        spawnedPrefab.SetActive(false);
    }

        
    void OnEnable() => trackedImageManager.trackedImagesChanged += imageChanged;
    void OnDisable() => trackedImageManager.trackedImagesChanged -= imageChanged;
    
    private void imageChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach(var trackedImage in eventArgs.added)
        {
            updateImage(trackedImage);
        }
        foreach (var trackedImage in eventArgs.updated)
        {
            updateImage(trackedImage);
        }
        foreach (var trackedImage in eventArgs.removed)
        {
            spawnedPrefab.SetActive(false);
        }
    }
    private void updateImage(ARTrackedImage trackedImage)
    {
        spawnedPrefab.SetActive(true);
        spawnedPrefab.transform.parent = trackedImage.gameObject.transform;
        // spawnedPrefab.transform.position = trackedImage.transform.position;
        // spawnedPrefab.transform.localPosition = new Vector3(0, 0, 0);
        spawnedPrefab.transform.localRotation = new Quaternion(0, 180, 180, 0);  
        spawnedPrefab.transform.localScale = new Vector3(prefabScale, prefabScale, 0.01f);
    }
    void Start()
    {
    }



    IEnumerator Proceed() {
        float duration = 3;
        float counter = 0;
        Renderer renderer = spawnedPrefab.transform.Find("Eagle").GetComponent<Renderer>();
        Color color = renderer.material.color;
        while (counter < duration) {
            counter += Time.deltaTime;
            float alpha = Mathf.Lerp(1, 0, counter/duration);
            renderer.material.color = new Color(color.r, color.g, color.b, alpha);
            yield return 1;
        }
        spawnedPrefab.SetActive(false);
        nextAnimation.enabled = true;
        enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
    //     if (Input.touchCount == 0 || Input.GetTouch(0).phase != TouchPhase.Began) { return; }
    //     //check if the button is clicked
    //     if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)) {
    //         Proceed();
    //     }
        HandInfo handInfo = manomotion.Hand_infos[0].hand_info;
        GestureInfo gestureInfo = handInfo.gesture_info;
        TrackingInfo trackingInfo = handInfo.tracking_info;
        if (gestureInfo.mano_gesture_trigger == ManoGestureTrigger.NO_GESTURE) {return;}
        Debug.Log(gestureInfo.mano_gesture_trigger);
        if (gestureInfo.mano_gesture_trigger == ManoGestureTrigger.CLICK || gestureInfo.mano_gesture_trigger == ManoGestureTrigger.DROP) {
            //cast ray from screen to fingertip
            Ray ray = Camera.main.ScreenPointToRay(trackingInfo.skeleton.joints[8]); //8 is the index of the tip of the index finger
            if (Physics.Raycast(ray, out RaycastHit hit)) {
                if (hit.collider.transform.parent.transform.parent == spawnedPrefab) {
                   StartCoroutine(Proceed());
                }
            }
            StartCoroutine(Proceed());
        }
    }


}
