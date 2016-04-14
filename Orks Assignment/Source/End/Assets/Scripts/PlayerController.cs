using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public int Speed = 1;
    [Tooltip("How much damage we deliver on a hit to others")]
    public int HitStrength = 10;

    //Components on this game object we need to work with
    private Animator _animator;
    private Rigidbody2D _rigidBody;
    private SpriteRenderer _spriteRenderer;
    private GameController _gameController;


    private int Health = 100;
    private int _coinScore;
    private bool _dead;

    //The read in values from the keyboard
    private float _horizontal;
    private float _vertical;


    // Use this for initialization
    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidBody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    public void GotHit(int hitPoints)
    {
        if (_dead)
        {
            return;
        }
        Health -= hitPoints;
        if (Health < 0) Health = 0;

        //Update the UI
        _gameController.UpdatePlayerHealth(Health);

        if (Health <= 0)
        {
            HesDeadJim();
        }
    }

    private void HesDeadJim()
    {
        _dead = true;

        //Play death.
        _animator.SetTrigger("Death");
        //Wait, restart scene.
        StartCoroutine(Reload());
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(2);
        _gameController.ReloadScene();
    }

    //Add health
    private void PowerUp(int points)
    {
        Health += points;
        if (Health > 100)
        {
            Health = 100;
        }
        _gameController.UpdatePlayerHealth(Health);
    }


    //Add coin score
    private void CoinUp(int coins)
    {
        _coinScore += coins;
        if (_coinScore > 100)
        {
            _coinScore = 100;
        }
        _gameController.UpdatePlayerCoin(_coinScore);
    }

    //Called when we run over another object with a trigger
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Coin")
        {
            //Get its coin score
            //TODO
            var pickupProperties = collision.gameObject.GetComponent<PickupProperties>();
            CoinUp(pickupProperties.CoinAmount);

            //TODO - get rid of the game object we just picked up
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Health")
        {
            //Get its health
            //TODO
            var pickupProperties = collision.gameObject.GetComponent<PickupProperties>();
            PowerUp(pickupProperties.HealthAmount);

            //TODO - get rid of the game object we just picked up
            Destroy(collision.gameObject);
        }
    }

    void Update()
    {
        //TODO: Need to read player input here for Fire1
        if (Input.GetButtonDown("Fire1"))
        {
            _animator.SetTrigger("Attack");
            AttackLocalEnemies();
        }
        //Read the left/right input
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");

        //Flips the character left if the input is < 0 and 'normal right facing' if > 0 
        if (_horizontal > 0)
        {
            _spriteRenderer.flipX = true;
        }
        else if (_horizontal < 0)
        {
            _spriteRenderer.flipX = false;
        }

        //TODO: Play Walk animation if we've read input on the horizontal
        _animator.SetBool("Walk", _horizontal != 0 || _vertical != 0);
    }

    void AttackLocalEnemies()
    {
        //Enemies are on the Enemy Layer, which is layer 8. So shift left by 8 bits
        //Draw a circle 1.5 meters out to see what we hit.
        var collider = Physics2D.OverlapCircle(transform.position, 1.5f, (1 << 8));
        if (collider != null)
        {
            //Get the component for EnemyHealth
            var health = collider.gameObject.GetComponent<EnemyHealth>();
            health.GotHit(HitStrength);

        }

        //If we want to hit mulitple enemies at once we could:
        /*
        var colliders = Physics2D.OverlapCircleAll(transform.position, 1f, (1 << 8));
        if (colliders.Length > 0)
        {
            // enemies within 1m
        }
        */
    }


    //Fixed update is for the physics system. 
    void FixedUpdate()
    {
        if (_dead)
        {
            return;
        }

        //Move the actual object by setting its velocity
        _rigidBody.velocity = new Vector2(_horizontal * Speed, _vertical * Speed);
    }
}
