using UnityEngine;
using TMPro;

public class KeyInteractGenerator : MonoBehaviour, IInteractable
{
    public TextMeshPro timerText;
    public float timer;
    bool activated = false;

    public void Interact() {
        activated = true;
    }
    // Start is called before the first frame update
    void Start() {
        timerText.text = timer.ToString();
    }

    // Update is called once per frame
    void Update() {
        if (activated) {
            timer -= Time.deltaTime;
            timerText.text = Mathf.CeilToInt(timer).ToString();
            activated = timer > 0;
        }
    }
}
