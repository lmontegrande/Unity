using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerJumpScript : MonoBehaviour {

    public static PlayerJumpScript instance;

    private Rigidbody2D myBody;
    private Animator anim;

    [SerializeField]
    private float forceX, forceY;

    private float thresholdX = 7f;
    private float thresholdY = 14f;
    private float maxX = 6.5f;
    private float maxY = 13.5f;

    private bool setPower, didJump;

    private Slider powerBar;
    private float powerBarThreshold = 10f;
    private float powerBarValue = 0f;

    private GameObject currentPlatform;

    void Awake()
    {
        MakeInstance();
        Initialize();
    }

    void Update()
    {
        SetPower();
    }

    void Initialize()
    {
        powerBar = GameObject.Find("Power Bar").GetComponent<Slider>();
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        powerBar.minValue = 0f;
        powerBar.maxValue = 10f;
        powerBar.value = powerBarValue;
    }

    void MakeInstance()
    {
        if (PlayerJumpScript.instance == null)
            instance = this;
    }

    void SetPower()
    {
        if (setPower)
        {
            forceX += thresholdX * Time.deltaTime;
            forceY += thresholdY * Time.deltaTime;

            if (forceX > maxX)
                forceX = maxX;

            if (forceY > maxY)
                forceY = maxY;

            powerBarValue += powerBarThreshold * Time.deltaTime;
            powerBar.value = powerBarValue;
        }
    }

    public void SetPower(bool setPower)
    {
        this.setPower = setPower;

        if (!setPower)
        {
            Jump();
        }
    }

    void Jump()
    {
        myBody.velocity = new Vector2(forceX, forceY);
        forceX = forceY = 0;
        didJump = true;
        anim.SetBool("Jump", true);

        powerBarValue = 0f;
        powerBar.value = powerBarValue;
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        GameObject targetPlatform = target.gameObject;
        if (!currentPlatform)
            currentPlatform = targetPlatform;

        anim.SetBool("Jump", false);

        if (didJump && targetPlatform != currentPlatform)
        {
            currentPlatform = targetPlatform;
            didJump = false;
            if (target.tag == "Platform")
            {
                if (GameManager.instance != null)
                {
                    GameManager.instance.CreateNewPlatformAndLerp(target.transform.position.x);
                }

                if (ScoreManager.instance != null)
                {
                    ScoreManager.instance.IncrementScore();
                }
            }
        }

        if (target.tag == "Dead")
        {
            if (GameOverManager.instance != null)
            {
                GameOverManager.instance.GameOverShowPanel();
            }
            Destroy(gameObject);
        }
    }
}
