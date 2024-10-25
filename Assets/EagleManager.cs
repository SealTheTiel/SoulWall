using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Interfaces;
using Unity.VisualScripting;
using UnityEngine;

public class EagleManager : MonoBehaviour
{
    [SerializeField] private GameObject eaglePrefab;
    [SerializeField] private Material selectedTexture;
    [SerializeField] private Material unselectedTexture;
    [SerializeField] private GameObject[] artworks;
    private MeshCollider eagleCollider;

    void ToggleArtworks(bool state)
    {
        foreach (GameObject artwork in artworks)
        {
            artwork.SetActive(state);
        }
    }

    void Start()
    {
        eagleCollider = eaglePrefab.GetComponent<MeshCollider>();
        eaglePrefab.GetComponent<Renderer>().material = unselectedTexture;
        ToggleArtworks(false);
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider == eagleCollider || hit.transform.tag == "artwork")
            {
                eaglePrefab.GetComponent<Renderer>().material = selectedTexture;
                ToggleArtworks(true);
            }
            else
            {
                eaglePrefab.GetComponent<Renderer>().material = unselectedTexture;
                ToggleArtworks(false);
            }
        }
    }
}
