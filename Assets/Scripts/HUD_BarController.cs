using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD_BarController : MonoBehaviour
{
    public Slider slider;

    void Start() {
        slider = GetComponent<Slider>();
    }

    public void SetValue(float val) {
        slider.value = val;
    }

    public void SetMax(float val) {
        slider.maxValue = val;
        slider.value = val;
    }

    public void Show() {
        gameObject.SetActive(true);
    }

    public void Hide() {
        gameObject.SetActive(false);
    }
}
