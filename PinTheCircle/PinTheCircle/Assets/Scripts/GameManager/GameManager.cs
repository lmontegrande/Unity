using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance;
    public bool infiniteMode;
    private Button shootBtn;

    [SerializeField]
    private GameObject needle;

    private GameObject[] gameNeedles;

    [SerializeField]
    private int howManyNeedles;

    private float needleDistance = 0.6f; 
    private int needleIndex;
    private bool canPressButton = true;

    private GameObject currentNeedle;

	void Awake () {
	    if (instance == null)
        {
            instance = this;
        }
        GetButton();
	}
	
	void Start () {
        if (infiniteMode)
            InstantiateNeedle();
        else
            CreateNeedles();
	}

    void GetButton()
    {
        shootBtn = GameObject.Find("Shoot Button").GetComponent<Button>();
        if (infiniteMode)
            shootBtn.onClick.AddListener(() => ShootTheNeedle2());
        else
            shootBtn.onClick.AddListener(() => ShootTheNeedle());
    }

    public void ShootTheNeedle()
    {
        if (!canPressButton)
            return;

        gameNeedles[needleIndex].GetComponent<NeedleMovementScript>().FireTheNeedle();
        needleIndex++;

        if (needleIndex == gameNeedles.Length)
        {
            Debug.Log("Game Is Finished");
            shootBtn.onClick.RemoveAllListeners();
        }
    }

    public void ShootTheNeedle2()
    {
        if (!canPressButton)
            return;

        currentNeedle.GetComponent<NeedleMovementScript>().FireTheNeedle();
    }

    void CreateNeedles()
    {
        gameNeedles = new GameObject[howManyNeedles];
        Vector3 temp = transform.position;

        for (int i=0; i < gameNeedles.Length; i++)
        {
            gameNeedles[i] = Instantiate(needle, temp, Quaternion.identity) as GameObject;
            temp.y -= needleDistance;
        }
    }

    public void InstantiateNeedle()
    {
        currentNeedle = Instantiate(needle, transform.position, Quaternion.identity) as GameObject;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void setCanPressButton(bool val)
    {
        canPressButton = val;
    }
}
