using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public Image HealthBar;
    public Text CoinText;
    public Text OrkText;

    [SerializeField]
    private GameObject _enemy;

    [SerializeField]
    private float _spawnSpeed = 4f;

    private GameObject[] _spawnPoints;
    private Coroutine _spawnOrks;
    private bool isSpawning = true;

    public void Start()
    {
        _spawnOrks = StartCoroutine(CoSpawnOrks());
    }

    IEnumerator CoSpawnOrks()
    {
        _spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        if (_spawnPoints.Length <= 0)
        {
            isSpawning = false;
        }

        while (isSpawning)
        {
            yield return new WaitForSeconds(Random.Range(_spawnSpeed, _spawnSpeed + 1f));

            var index = Random.Range(0, _spawnPoints.Length);
            var spawnPoint = _spawnPoints[index];

            Instantiate(_enemy, spawnPoint.transform.position, Quaternion.identity);
        }
    }

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

    public void UpdatePlayerOrk(int orkScore)
    {
        OrkText.text = orkScore.ToString();
    }

    public void ReloadScene()
    {
        int sceneNumber = SceneManager.GetActiveScene().buildIndex;
        GameObject.FindObjectOfType<ScreenFader>().EndScene(sceneNumber);
    }
}
