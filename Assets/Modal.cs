using UnityEngine;
using TextMeshPro = TMPro.TextMeshPro;
using System.Collections.Generic;
public class Modal : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private Sprite artworkImage;
    [SerializeField] SpriteRenderer spriteRenderer;
    private string title;
    [SerializeField] TextMeshPro titleObject;
    private string description;
    [SerializeField] TextMeshPro descriptionObject;
    private string id;

    private Logger logger;
    void Awake() {
        if (artworkImage) {
            spriteRenderer.sprite = artworkImage;
        }
        if (titleObject) {
            titleObject.text = title;
        }
        if (descriptionObject) {
            descriptionObject.text = description;
        }
    }
    void Start()
    {
        logger = FindObjectOfType<Logger>();
        logger.Log("modal_actions", id, "Opened");
        transform.hasChanged = false;
    }

    // Update is called once per frame
    void Update()
    {
        // check if transform is changed
        if (transform.hasChanged)
        {
            logger.Log("modal_transform", id, transform.position.x, transform.position.y, transform.position.z, transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w, transform.localScale.x, transform.localScale.y, transform.localScale.z);
            transform.hasChanged = false;
        }
        
    }

    void OnDestroy() {
        logger.Log("modal_actions", id, "Closed");
    }

    public void SetData(Sprite artworkImage, string id, string title, string description) {
        this.artworkImage = artworkImage;
        this.title = title;
        this.description = description;
        this.id = id;
        if (spriteRenderer) {
            spriteRenderer.sprite = artworkImage;
        }
        if (titleObject) {
            titleObject.text = title;
        }
        if (descriptionObject) {
            descriptionObject.text = description;
        }
    }

    public string GetId() {
        return id;
    }
}
