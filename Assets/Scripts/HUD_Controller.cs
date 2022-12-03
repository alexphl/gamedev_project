using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD_Controller : MonoBehaviour
{
    public HUD_BarController healthBar;
    public HUD_BarController shieldBar;
    public HUD_BarController bossBar;
    public PopUp deathScreen;
    public PopUp_Text textMessage;

    void Start()
    {
        bossBar.Hide();
    }

    public void SetHealth(float val) {
        healthBar.SetValue(val);
    }

    public void SetMaxHealth(float val) {
        healthBar.SetMax(val);
    }

    public void SetShield(float val) {
        shieldBar.SetValue(val);
    }

    public void SetMaxShield(float val) {
        shieldBar.SetMax(val);
    }

    public IEnumerator ShowDeathScreen(int timer) {
        deathScreen.Show();
        yield return new WaitForSeconds(timer);
        deathScreen.Hide();
    }

    public void showTextMessage() {
        textMessage.Show();
    }

    public void showTextMessage(string message) {
        textMessage.setText(message);
        textMessage.Show();
    }

    public void hideTextMessage() {
        textMessage.Hide();
    }

    public void showBossBar(float maxHealth) {
        bossBar.SetMax(maxHealth);
        bossBar.Show();
    }

    public void hideBossBar() {
        bossBar.Hide();
    }

    public void setBossHealth(float val) {
        bossBar.SetValue(val);
    }

}
