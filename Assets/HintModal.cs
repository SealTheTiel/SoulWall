using UnityEngine;
using UnityEngine.UI;
using TextMeshPro = TMPro.TextMeshPro;
using System.Collections.Generic;
using UnityEngine.Video;
public class HintModal : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private VideoClip video;
    [SerializeField] VideoPlayer videoPlayer;
    private string number;
    [SerializeField] TextMeshPro numberObject;
    private string description;
    [SerializeField] TextMeshPro descriptionObject;
    private string id;

    private Logger logger;
    void Awake() {
        if (video) {
            videoPlayer.clip = video;
        }
        if (numberObject) {
            numberObject.text = number;
        }
        if (descriptionObject) {
            descriptionObject.text = description;
        }
    }
    void Start() {}

    // Update is called once per frame
    void Update() {}

    public void SetData(VideoClip video, string id, string number, string description) {
        this.video = video;
        this.number = number;
        this.description = description;
        this.id = id;
        if (video) {
            videoPlayer.clip = video;
        }
        if (numberObject) {
            numberObject.text = number;
        }
        if (descriptionObject) {
            descriptionObject.text = description;
        }
    }

    public string GetId() {
        return id;
    }
}
