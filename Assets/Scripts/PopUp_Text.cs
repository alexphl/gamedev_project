using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopUp_Text : MonoBehaviour
{
    public TMP_Text text;

    void Start()
    {
        gameObject.SetActive(false);
    }

    public void Show() {
        gameObject.SetActive(true);
    }

    public void Hide() {
        gameObject.SetActive(false);
    }

    public void setText(string message)
    {
        this.text.text = message;
    }
}
