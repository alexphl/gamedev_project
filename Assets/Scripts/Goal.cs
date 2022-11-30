using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    private AssetBundle myLoadedAssetBundle;
    private string[] scenePaths;
    private void Start()
    {
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("change");
        if (other.gameObject.GetComponent<Player>())
        {
            
            SceneManager.LoadScene("Survival");
            
        }
    }
}

