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
    private int frameCounter = 0;
    private int nFrames = 10; 
    private Transform parentTransform;
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
        if (transform.hasChanged) {
            frameCounter %= nFrames;
            if (frameCounter == 0) {
                LogPosition();
            }
            frameCounter++;
        }
        transform.hasChanged = false;
    }

    void LogPosition() {
        Vector3 relativePosition = transform.position - parentTransform.position;
        Vector3 cameraPosition = Camera.main.transform.position;
        Vector3 cameraLook = Camera.main.transform.forward;
        logger.Log("modal_transform", id,   transform.position.x, transform.position.y, transform.position.z,
                                            transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w,
                                            transform.localScale.x, transform.localScale.y, transform.localScale.z,
                                            relativePosition.x, relativePosition.y, relativePosition.z,
                                            cameraPosition.x, cameraPosition.y, cameraPosition.z,
                                            cameraLook.x, cameraLook.y, cameraLook.z);
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
    public void SetParentTransform(Transform parentTransform) {
        this.parentTransform = parentTransform;
    }
    public string GetId() {
        return id;
    }
}
