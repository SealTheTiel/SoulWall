using UnityEngine;

public class ArtworkManager : MonoBehaviour
{
    [SerializeField]
    private GameObject modalPrefab; 
    private GameObject modal;

    [SerializeField] Sprite artworkImage;
    [SerializeField] string id;
    [SerializeField] string title;
    [SerializeField] string description;
    // Start is called once before the first execution of transform.position + tranform.right;
    void Start() { }

    // Update is called once per frame
    void Update() { }
    
    public void ShowModal() {
        Destroy(modal);
        string modalTag = modalPrefab.tag;
        Modal[] modals = GameObject.FindObjectsByType<Modal>(FindObjectsSortMode.None);
        foreach (Modal modal in modals) {
            if (modal.GetId() == id) {
                Destroy(modal.gameObject);
            }
        }
        modal = Instantiate(modalPrefab, transform.position, Quaternion.identity);
        modal.GetComponent<Modal>().SetData(artworkImage, id, title, description);
        
        modal.SetActive(true);
        modal.transform.localScale = Vector3.one * 0.25f;
        modal.transform.position = transform.position + (transform.right * 0.25f);
        modal.transform.LookAt(modal.transform.position - (Camera.main.transform.position - modal.transform.position)); 
    }
}
