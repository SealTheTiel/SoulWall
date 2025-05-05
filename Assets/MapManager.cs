using UnityEngine;
using System.Collections;
using UnityEngine.XR.ARFoundation;

public class MapManager : MonoBehaviour
{
    [SerializeField] private GameObject mapPrefab;
    [SerializeField] private float prefabScale = 1f;
    private GameObject mapInstance;
    private bool isPlaced = false;
    private GameObject spatialAnchor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    IEnumerator AddAnchor(GameObject go) {
        var anchor = go.AddComponent<OVRSpatialAnchor>();
        yield return new WaitUntil(() => anchor.Created);
    }
    void Awake()
    {
        Debug.Log("MapManager: ACTIVE");
        mapInstance = Instantiate(mapPrefab, Vector3.zero, Quaternion.identity);
        mapInstance.SetActive(false);
    }

    void OnEnable()
    {

    }

    // Update is called once per frame
    void Update() {
        Debug.Log("MapManager: PLACE NEW ANCHOR");

        if (!OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) && !OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger)) { return; }
        if (isPlaced) { Destroy(spatialAnchor); }
        spatialAnchor = new GameObject("EagleAnchor");
        spatialAnchor.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 0.5f;
        StartCoroutine(AddAnchor(spatialAnchor));

        mapInstance.SetActive(true);
        mapInstance.transform.position = spatialAnchor.transform.position;
        mapInstance.transform.LookAt(Camera.main.transform.position);
        mapInstance.transform.rotation = Quaternion.Euler(0, mapInstance.transform.rotation.eulerAngles.y, 0);
        mapInstance.transform.localScale = new Vector3(prefabScale, prefabScale, prefabScale);

        isPlaced = true;
    }
}
