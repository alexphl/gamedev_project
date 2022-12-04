using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    private AssetBundle myLoadedAssetBundle;
    private string[] scenePaths;
    public bool isFinalLevel = false;

    public HUD_Controller playerHUD;

    void Start() {
        if (!playerHUD) playerHUD = GameObject.Find("HUD Overlay").GetComponent<HUD_Controller>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Player>())
        {
            if (!isFinalLevel) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            else {
                playerHUD.showTextMessage("Congratulations! You beat the game.");
            }
        }
    }
}

