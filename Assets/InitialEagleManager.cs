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

    private bool firstStart = true;

    void Awake() {
        spawnedPrefab = Instantiate(initialEaglePrefab, Vector3.zero, Quaternion.identity);
        spawnedPrefab.SetActive(false);
    }

    void Start() {
       
    }

    void OnEnable() {
        
    }

    IEnumerator AddAnchor() {
        var anchor = spatialAnchor.AddComponent<OVRSpatialAnchor>();
        yield return new WaitUntil(() => anchor.Created);
    }
    
    // Update is called once per frame
    void Update() {
        if (!OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) && !OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger)) { return; }
        if (isPlaced) { Destroy(spatialAnchor); }
        Debug.Log("MapManager: PLACE NEW ANCHOR");
        spatialAnchor = new GameObject("EagleAnchor");
        spatialAnchor.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 0.5f;
        StartCoroutine(AddAnchor());

        spawnedPrefab.SetActive(true);
        spawnedPrefab.transform.position = spatialAnchor.transform.position;
        spawnedPrefab.transform.LookAt(Camera.main.transform.position);
        spawnedPrefab.transform.rotation = Quaternion.Euler(0, spawnedPrefab.transform.rotation.eulerAngles.y, 0);
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
        
        if(!firstStart) {
            resetEagle();
        } else {
            firstStart = false;
        }
    }

    private void resetEagle() {
        GameObject tutorialModal = spawnedPrefab.transform.Find("TutorialModal").gameObject;
        GameObject closeTutorialButton = tutorialModal.transform.Find("Visuals").gameObject.transform.Find("Close_PokeInteraction").gameObject;
        closeTutorialButton.SetActive(true);
        tutorialModal.SetActive(false);
        GameObject eagle = spawnedPrefab.transform.Find("EagleObject").gameObject;
        eagle.SetActive(true);
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
