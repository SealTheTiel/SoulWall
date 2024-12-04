using System.Collections;
using UnityEngine;

public class EagleTransition : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {}

    // Update is called once per frame
    void Update() {}

    private IEnumerator Animate() {
        float duration = 3;
        float counter = 0;
        Renderer renderer = gameObject.GetComponent<Renderer>();
        Color color = renderer.material.color;
        while (counter < duration) {
            counter += Time.deltaTime;
            float alpha = Mathf.Lerp(1, 0, counter/duration);
            renderer.material.color = new Color(color.r, color.g, color.b, alpha);
            yield return 1;
        }
        InitialEagleManager initialEagleManager = FindFirstObjectByType<InitialEagleManager>();
        initialEagleManager.enableNext();
        gameObject.SetActive(false);
        enabled = false;
    }

    public void Proceed() {
        StartCoroutine(Animate());
    }
}
