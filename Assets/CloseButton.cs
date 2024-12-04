using UnityEngine;

public class CloseButton : MonoBehaviour
{
    [SerializeField]
    private GameObject itemToClose;
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
        Destroy(itemToClose);
    }
}
