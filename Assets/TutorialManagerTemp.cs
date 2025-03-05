using UnityEngine;
using UnityEngine.Video;

public class TutorialManagerTemp : MonoBehaviour
{
    [SerializeField]
    private GameObject modalPrefab; 
    private GameObject modal;
    [SerializeField] private GameObject eagle;

    // Start is called once before the first execution of transform.position + tranform.right;
    void Start() { }

    // Update is called once per frame
    void Update() { }
    
    public void ShowModal() {
        Destroy(modal);
        modal = Instantiate(modalPrefab, transform.position, Quaternion.identity);
        
        modal.SetActive(true);
        modal.transform.localScale = Vector3.one * 0.35f;
        modal.transform.position = eagle.transform.position + (transform.forward * 0.1f);
        modal.transform.LookAt(eagle.transform.position);// - (eagle.transform.position - modal.transform.position)); 
    }
}
