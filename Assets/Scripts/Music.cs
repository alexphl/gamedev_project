using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class Music : MonoBehaviour
    {
    public AudioSource audioData;
        private void Start()
        {
        audioData = GetComponent<AudioSource>();
        audioData.Play(0);
        //DontDestroyOnLoad(this);
        }
    }
