using UnityEngine;
using Oculus.Interaction;
using Unity.VisualScripting;
public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuPrefab;
    [SerializeField] private InitialEagleManager initialEagleManager;
    [SerializeField] private MapManager mapManager;
    [SerializeField] private float prefabScale = 0.2f;
    private GameObject muralButton;
    private GameObject mapButton;
    private GameObject mainMenuInstance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainMenuInstance = Instantiate(mainMenuPrefab, Vector3.zero, Quaternion.identity);   
        mainMenuInstance.SetActive(true);
        muralButton = mainMenuInstance.transform.Find("Mural").gameObject;
        mapButton = mainMenuInstance.transform.Find("Map").gameObject;
        mainMenuInstance.transform.localScale =  new Vector3(prefabScale, prefabScale, prefabScale);
        connectButtons();
    }

    // Update is called once per frame
    void Update()
    {
        mainMenuInstance.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 0.5f;
        mainMenuInstance.transform.LookAt(Camera.main.transform.position);
    }

    private void connectButtons() {
        muralButton.GetComponent<InteractableUnityEventWrapper>().WhenUnselect.AddListener(StartMural);
        mapButton.GetComponent<InteractableUnityEventWrapper>().WhenUnselect.AddListener(StartMap);
    }

    private void disconnectButtons() {
        muralButton.GetComponent<InteractableUnityEventWrapper>().WhenUnselect.RemoveListener(StartMural);
        mapButton.GetComponent<InteractableUnityEventWrapper>().WhenUnselect.RemoveListener(StartMap);
    }


    public void StartMural() {
        disconnectButtons();
        mainMenuInstance.SetActive(false);
        initialEagleManager.enabled = true;
        Debug.Log("MAIN: Mural SELECTED");
    }

    public void StartMap() {
        disconnectButtons();
        mainMenuInstance.SetActive(false);
        mapManager.enabled = true;
        Debug.Log("MAIN: MAP SELECTED");
    }
}
