using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalManager : MonoBehaviour
{

    public float RotationSpeed = 3;
    private bool active;
    // Start is called before the first frame update

    private void OnEnable()
    {
        foreach (Transform t in transform)
        {
            t.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * (RotationSpeed * Time.deltaTime));
        active = false;
        foreach (Transform t in gameObject.transform)
        {
            if (t.gameObject.activeSelf)
            {
                active = true;
            }
            
        }
        gameObject.SetActive(active);
    }
}
