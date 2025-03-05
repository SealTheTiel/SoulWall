using UnityEngine;
using System.Collections;

public class EagleManager : MonoBehaviour
{
    [SerializeField] private GameObject eagleOutline;
    [SerializeField] private GameObject eaglePrefab;
    [SerializeField] private Material selectedTexture;
    [SerializeField] private Material unselectedTexture;
    [SerializeField] private GameObject[] artworks;
    private bool focused = false;
    private Coroutine focusCoroutine;

    private MeshCollider eagleCollider;
    private Renderer eagleRenderer;
    private Logger logger;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        logger = FindAnyObjectByType<Logger>();
        eagleCollider = eaglePrefab.GetComponent<MeshCollider>();
        eagleRenderer = eaglePrefab.GetComponent<Renderer>();
        eagleRenderer.material = unselectedTexture;
        ToggleArtworks(false);   
    }

    IEnumerator FocusCheck() {
        yield return new WaitForSeconds(2f);
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, 100.0f)) {
            if (!focused && hit.collider == eagleCollider) {
                logger.Log("eagle_focus", $"{gameObject.name}", "Focused");
                focused = true;
            }
        }

    }

    // Update is called once per frame
    void Update() {
        if  (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, 100.0f)) {
            if (hit.collider == eagleCollider) {
                eagleRenderer.material = selectedTexture;
                eagleOutline.SetActive(true);
                ToggleArtworks(true);
                if (focusCoroutine == null) { focusCoroutine = StartCoroutine(FocusCheck()); }
                return;
            }
        } 
        if (focusCoroutine != null) {
            StopCoroutine(focusCoroutine);
            focusCoroutine = null;
        }
        if (focused) {
            logger.Log("eagle_focus", $"{gameObject.name}", "Unfocused");
            focused = false;
        }
        eagleRenderer.material = unselectedTexture;
        eagleOutline.SetActive(false);
        ToggleArtworks(false);  
    }

    void ToggleArtworks(bool state)
    {
        foreach (GameObject artwork in artworks)
        {
            artwork.SetActive(state);
        }
    }
}
