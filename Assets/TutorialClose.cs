using UnityEngine;

public class TutorialClose : MonoBehaviour
{
    [SerializeField]
    private GameObject itemToClose;

    [SerializeField] private GameObject eagle;

    [SerializeField] private Tutorial tutorialManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Close()
    {
        itemToClose.SetActive(false);
        tutorialManager.Reset();
        eagle.SetActive(true);
    }
}
