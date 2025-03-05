using UnityEngine;

public class ToggleMuseumLayoutButton : MonoBehaviour
{

    private GameObject museumLayoutOverlay;
    private Transform parentTransform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void Awake()
    {
        parentTransform = transform.parent;

        if (parentTransform)
        {
            museumLayoutOverlay = parentTransform.Find("OverlayModal")?.gameObject;

            if (museumLayoutOverlay)
            {
                Debug.Log("OverlayModal found");
            }
            else
            {
                Debug.LogWarning("OverlayModal not found");
            }
        }
    }

    public void ToggleVisibility()
    {
        museumLayoutOverlay.SetActive(!museumLayoutOverlay.activeSelf);
    }
}
