using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public Image HealthBar;
    public Text CoinText;


    public void UpdatePlayerHealth(int health)
    {
        HealthBar.fillAmount = health / 100f;
        if (health < 50)
        {
            HealthBar.color = Color.red;
        }
    }

    public void UpdatePlayerCoin(int coinScore)
    {
        CoinText.text = coinScore.ToString();
    }

    public void ReloadScene()
    {
        int sceneNumber = SceneManager.GetActiveScene().buildIndex;
        GameObject.FindObjectOfType<ScreenFader>().EndScene(sceneNumber);
    }
}
