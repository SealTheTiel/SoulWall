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
    [SerializeField] SpriteRenderer positionSpriteObject;
    private Sprite positionSprite;

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
        if (positionSpriteObject)
        {
            positionSpriteObject.sprite = positionSprite;
        }

    }
    void Start()
    {
        logger = FindAnyObjectByType<Logger>();
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

    public void SetData(Sprite artworkImage, string id, string title, string description, Sprite positionSprite) {
        this.artworkImage = artworkImage;
        this.title = title;
        this.description = description;
        this.id = id;
        this.positionSprite = positionSprite;
        if (spriteRenderer) {
            spriteRenderer.sprite = artworkImage;
        }
        if (titleObject) {
            titleObject.text = title;
        }
        if (descriptionObject) {
            descriptionObject.text = description;
        }
        if (positionSpriteObject)
        {
            positionSpriteObject.sprite = positionSprite;
        }
    }

    public string GetId() {
        return id;
    }
}
