using UnityEngine;
using TextMeshPro = TMPro.TextMeshPro;
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
