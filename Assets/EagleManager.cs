using UnityEngine;

public class EagleManager : MonoBehaviour
{
    [SerializeField] private GameObject eagleOutline;
    [SerializeField] private GameObject eaglePrefab;
    [SerializeField] private Material selectedTexture;
    [SerializeField] private Material unselectedTexture;
    [SerializeField] private GameObject[] artworks;

    private MeshCollider eagleCollider;
    private Renderer eagleRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        eagleCollider = eaglePrefab.GetComponent<MeshCollider>();
        eagleRenderer = eaglePrefab.GetComponent<Renderer>();
        eagleRenderer.material = unselectedTexture;
        ToggleArtworks(false);   
    }

    // Update is called once per frame
    void Update() {
        if  (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, 100.0f))
        {
            if (hit.collider == eagleCollider)
            {
                eagleRenderer.material = selectedTexture;
                eagleOutline.SetActive(true);
                ToggleArtworks(true);
                return;
            }
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
