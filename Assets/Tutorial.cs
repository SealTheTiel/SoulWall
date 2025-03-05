using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Unity.VisualScripting;
using TMPro;

public class Tutorial : MonoBehaviour
{
    // [SerializeField] string filePath;

    [SerializeField] List<Sprite> sprites;
    [SerializeField] List<string> titleTexts;
    [SerializeField] List<string> descriptions;

    [SerializeField] GameObject Title;
    [SerializeField] GameObject Description;
    [SerializeField] Modal modal;

    [SerializeField] GameObject nextButton;
    [SerializeField] GameObject previousButton;

    private int currentIndex = 0;

    void Awake()
    {
        string id = "tutorial" + currentIndex;
        modal.SetData(sprites[currentIndex], id, titleTexts[currentIndex], descriptions[currentIndex]);

    }
    void Start() { }

    // Update is called once per frame
    void Update() { }

    public void GoToNext()
    {
        currentIndex++;
        if (currentIndex >= sprites.Count - 1 ) {
            currentIndex = sprites.Count - 1;
            nextButton.SetActive(false);
        }
        string id = "tutorial" + currentIndex;
        if (currentIndex < sprites.Count)
        {
            modal.SetData(sprites[currentIndex], id, titleTexts[currentIndex], descriptions[currentIndex]);
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
            modal.SetData(sprites[currentIndex], id, titleTexts[currentIndex], descriptions[currentIndex]);
            nextButton.SetActive(true);
        }
    }
}
