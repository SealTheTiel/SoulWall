using System.Collections;
using Meta.XR.ImmersiveDebugger.UserInterface.Generic;
using UnityEngine;
public class InitialEagleManager : MonoBehaviour
{
    [SerializeField]
    private GameObject initialEaglePrefab;
    
    [SerializeField]
    private GameObject eaglePrefab;

    private GameObject spawnedPrefab;
    [SerializeField]
    private float prefabScale = 0.2f;
    private GameObject spatialAnchor;

    private bool isPlaced = false;

    void Awake() {
        spawnedPrefab = Instantiate(initialEaglePrefab, Vector3.zero, Quaternion.identity);
        spawnedPrefab.SetActive(false);
    }

    void Start() {}

    IEnumerator AddAnchor(GameObject go) {
        var anchor = go.AddComponent<OVRSpatialAnchor>();
        yield return new WaitUntil(() => anchor.Created);
    }
    
    // Update is called once per frame
    void Update() {
        if (!OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) && !OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger)) { return; }
        if (isPlaced) { Destroy(spatialAnchor); }

        spatialAnchor = new GameObject("EagleAnchor");
        spatialAnchor.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 0.5f;
        StartCoroutine(AddAnchor(spatialAnchor));

        spawnedPrefab.SetActive(true);
        spawnedPrefab.transform.position = spatialAnchor.transform.position;
        spawnedPrefab.transform.LookAt(Camera.main.transform.position);
        spawnedPrefab.transform.localScale = new Vector3(prefabScale, prefabScale, prefabScale);

        isPlaced = true;
    }

    public GameObject getAnchor() {
        return spatialAnchor;
    }

    public void enableNext() {
        Transform originalTransform = spawnedPrefab.transform;
        Destroy(spawnedPrefab);

        spawnedPrefab = Instantiate(eaglePrefab, Vector3.zero, Quaternion.identity);
        spawnedPrefab.transform.position = originalTransform.position;
        spawnedPrefab.transform.rotation = originalTransform.rotation;
        spawnedPrefab.transform.localScale = new Vector3(prefabScale, prefabScale, prefabScale);
    }

    public void reset() {
        Transform originalTransform = spawnedPrefab.transform;
        Destroy(spawnedPrefab);

        spawnedPrefab = Instantiate(initialEaglePrefab, Vector3.zero, Quaternion.identity);
        spawnedPrefab.transform.position = originalTransform.position;
        spawnedPrefab.transform.rotation = originalTransform.rotation;
        spawnedPrefab.transform.localScale = new Vector3(prefabScale, prefabScale, prefabScale);    
    }
}
