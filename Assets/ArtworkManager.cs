using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class ArtworkManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 0 || Input.GetTouch(0).phase != TouchPhase.Began) { return; }
        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        if (Physics.Raycast(ray, out RaycastHit hit)) {
            if (hit.transform.tag == "artwork")
            {
                GameObject.Find("Canvas").transform.Find("ArtworkInfoModal").gameObject.SetActive(true);
            }
        }

    }
}
