using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD_Controller : MonoBehaviour
{
    public HUD_BarController healthBar;
    public HUD_BarController shieldBar;
    public PopUp deathScreen;

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

}
