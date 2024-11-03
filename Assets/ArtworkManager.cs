using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ArtworkManager : MonoBehaviour
{
    ManomotionManager manomotion;
    [SerializeField] private GameObject artwork;
    private GameObject modal;
    // Start is called before the first frame update
    void Start() {
        manomotion = GameObject.Find("ManomotionManager").GetComponent<ManomotionManager>();
        modal = Instantiate(artwork, Vector3.zero, Quaternion.identity);
        modal.transform.localPosition = new Vector3(0, 0, 0.5f);
        modal.transform.localScale = new Vector3(0.0001f, 0.0001f, 0.0001f);

    }

    // Update is called once per frame

    void Update() {
        // GESTURES
        // HandInfo handInfo = manomotion.Hand_infos[0].hand_info;
        // GestureInfo gestureInfo = handInfo.gesture_info;
        // TrackingInfo trackingInfo = handInfo.tracking_info;
        // if (gestureInfo.mano_gesture_trigger == ManoGestureTrigger.NO_GESTURE) {return;}

        if (Input.touchCount == 0) {return;}

        //if (gestureInfo.mano_gesture_trigger == ManoGestureTrigger.CLICK) {
            // Ray ray = Camera.main.ScreenPointToRay(trackingInfo.skeleton.joints[8]); //8 is the index of the tip of the index finger
        if (Input.GetTouch(0).phase == TouchPhase.Began) {
            // Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 10));
            if (Physics.Raycast(ray, out RaycastHit hit)) {
                if (hit.transform.parent.transform.name == gameObject.transform.name) {
                    modal.SetActive(true);
                    // modal.transform.parent = hit.transform.parent.transform.parent.transform;
                    modal.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 4, Screen.height / 2, 0.3f));
                    //modal.transform.localScale = new Vector3(0.0001f, 0.0001f, 0.0001f);
                    modal.transform.LookAt(modal.transform.position - (Camera.main.transform.position - modal.transform.position));
                    //modal.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
                    //modal.transform.localPosition = new Vector3(0, 0, 0.5f);
                    // modal.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0.5f));
                    // modal.transform.LookAt(Camera.main.transform);

                    // modal.transform.localRotation = Quaternion.Euler(0, 180, 0);
                    return;
                }
            } 
        }
    }
}