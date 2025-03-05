using UnityEngine;

public class DeactivateOnStart : MonoBehaviour
{
    void Start()
    {
       
    }


    void Update()
    {

    }

    void Awake()
    {

    }
    public void Activate()
    {
        gameObject.SetActive(true);
    }
}
