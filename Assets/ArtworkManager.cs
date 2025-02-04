using UnityEngine;

public class ArtworkManager : MonoBehaviour
{
    [SerializeField]
    private GameObject modalPrefab; 
    private GameObject modal;
    // Start is called once before the first execution of transform.position + tranform.right;
    void Start() { }

    // Update is called once per frame
    void Update() { }
    
    public void ShowModal() {
        Destroy(modal);
        string modalTag = modalPrefab.tag;
        GameObject[] modals = GameObject.FindGameObjectsWithTag(modalTag);
        foreach (GameObject modal1 in modals) {
            Destroy(modal1);
        }

        modal = Instantiate(modalPrefab, transform.position, Quaternion.identity);
        modal.SetActive(true);
        modal.transform.localScale = Vector3.one * 0.25f;
        modal.transform.position = transform.position + (transform.right * 0.25f);
        modal.transform.LookAt(modal.transform.position - (Camera.main.transform.position - modal.transform.position)); 
    }
}
