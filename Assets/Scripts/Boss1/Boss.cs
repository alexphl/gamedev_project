using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private Color ogColor;

    public BossManager1 bossMan;
    public float RotationSpeed = 0;
    public bool isMainBoss;
    private bool hittable = true;

    private float health;
    public float totalHealth;
    private int currentPhase;
    private int totalPhases = 3; //CHANGE

    public HUD_Controller playerHUD;

    private void Start()
    {
        if (!playerHUD) playerHUD = GameObject.Find("HUD Overlay").GetComponent<HUD_Controller>();

        if(isMainBoss) currentPhase = bossMan.phaseFlag;
        health = totalHealth;
        if(isMainBoss) playerHUD.showBossBar(totalHealth);
        ogColor = this.GetComponent<Renderer>().material.color;
    }

    private void OnEnable()
    {
        health = totalHealth;
        if(isMainBoss) currentPhase = 1;
    }

    private void Update()
    {
        transform.Rotate(Vector3.up * (RotationSpeed * Time.deltaTime));
    }

    public void GetHit(float damage)
    {
        if (hittable)
        {
            health -= damage;
            if (health <= 0f)
            {
                if (isMainBoss) bossMan.gameObject.SetActive(false);
                else gameObject.SetActive(false);
                if(isMainBoss) playerHUD.hideBossBar();
            }
            else
            {
                StartCoroutine(flashRed(1 / 6f));
                float divider = currentPhase;
                if (health <= (totalHealth - totalHealth * divider / totalPhases) && isMainBoss)
                {
                    currentPhase++;
                    bossMan.phaseFlag++;
                }
            }
        }
        else
        {
            StartCoroutine(flashBlue(1 / 6f));
        }

        if(isMainBoss) playerHUD.setBossHealth(health);
    }

    private IEnumerator flashRed(float duration)
    {
        this.GetComponent<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(duration);
        this.GetComponent<Renderer>().material.color = ogColor;
    }

    private IEnumerator flashBlue(float duration)
    {
        this.GetComponent<Renderer>().material.color = Color.blue;
        yield return new WaitForSeconds(duration);
        this.GetComponent<Renderer>().material.color = ogColor;
    }

    public void SetHittable(bool t)
    {
        hittable = t;
    }
}
