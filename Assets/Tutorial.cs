using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Unity.VisualScripting;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Video;

public class Tutorial : MonoBehaviour
{
    // [SerializeField] string filePath;
 
    [SerializeField] List<VideoClip> videos;
    [SerializeField] List<string> numberTexts;
    [SerializeField] List<string> descriptions;

    [SerializeField] GameObject Title;
    [SerializeField] GameObject Description;
    [SerializeField] HintModal modal;

    [SerializeField] GameObject nextButton;
    [SerializeField] GameObject previousButton;
    [SerializeField] GameObject closeButton;
    private int currentIndex = 0;

    void Awake()
    {
        string id = "tutorial" + currentIndex;
        modal.SetData(videos[currentIndex], id, numberTexts[currentIndex], descriptions[currentIndex]);

    }
    void Start() { }

    // Update is called once per frame
    void Update() { }

    public void GoToNext()
    {
        currentIndex++;
        if (currentIndex >= videos.Count - 1 ) {
            currentIndex = videos.Count - 1;
            nextButton.SetActive(false);
            if (closeButton.activeSelf == false) {
                closeButton.SetActive(true);
            }
        }
        string id = "tutorial" + currentIndex;
        if (currentIndex < videos.Count)
        {
            modal.SetData(videos[currentIndex], id, numberTexts[currentIndex], descriptions[currentIndex]);
            previousButton.SetActive(true);
        }

        // if previous button is disabled, enable it
        // if at the last index, disable next button
    }

    public void GoToPrevious()
    {
        currentIndex--;
        if (currentIndex <= 0) {
            currentIndex = 0;
            previousButton.SetActive(false);
        }
        string id = "tutorial" + currentIndex;
        if (currentIndex >= 0)
        {
            modal.SetData(videos[currentIndex], id, numberTexts[currentIndex], descriptions[currentIndex]);
            nextButton.SetActive(true);
        }
    }

    public void Reset()
    {
        currentIndex = 0;
        string id = "tutorial" + currentIndex;
        modal.SetData(videos[currentIndex], id, numberTexts[currentIndex], descriptions[currentIndex]);
        previousButton.SetActive(false);
        nextButton.SetActive(true);   
    }
}
