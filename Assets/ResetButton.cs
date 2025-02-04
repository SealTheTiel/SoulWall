using UnityEngine;

public class ResetButton : MonoBehaviour
{
    private GameObject initialEagleManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake() {
        initialEagleManager = GameObject.FindFirstObjectByType<InitialEagleManager>().gameObject;
    }

    public void Reset() {
        initialEagleManager.GetComponent<InitialEagleManager>().reset();
        Modal[] modals = GameObject.FindObjectsByType<Modal>(FindObjectsSortMode.None);
        foreach (Modal modal in modals) {
            Destroy(modal.gameObject);
        }
    }
}
