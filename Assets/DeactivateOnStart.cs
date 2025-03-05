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
        gameObject.SetActive(false);
    }
}
