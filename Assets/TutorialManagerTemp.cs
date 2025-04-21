using UnityEngine;
using UnityEngine.Video;

public class TutorialManagerTemp : MonoBehaviour
{
    [SerializeField]
    private GameObject modal;

    // Start is called once before the first execution of transform.position + tranform.right;
    void Start() { }

    // Update is called once per frame
    void Update() { }
    
    public void ShowModal() {
        modal.SetActive(true);
    }
}
