using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ModalManager : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject modal;
    bool moving = false;
    void Start()
    {
        Debug.unityLogger.logEnabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 0) {return;}
        if (Input.GetTouch(0).phase == TouchPhase.Ended) { moving = false; }
        if (moving) {
            gameObject.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0.3f));
            gameObject.transform.LookAt(gameObject.transform.position - (Camera.main.transform.position - gameObject.transform.position));
            return;
        }

        if (Input.GetTouch(0).phase == TouchPhase.Began) {
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 10));
            if (Physics.Raycast(ray, out RaycastHit hit)) {
                if (hit.transform == gameObject.transform) {
                    gameObject.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0.3f));
                    gameObject.transform.LookAt(gameObject.transform.position - (Camera.main.transform.position - gameObject.transform.position));
                    moving = true;
                }
                
                if (hit.transform.parent.transform == gameObject.transform && hit.transform.name == "Close") {
                    gameObject.SetActive(false);
                }

            }
        }
    }
}
