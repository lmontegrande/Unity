using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    public float Speed = 1f;
    public int AttackStrength = 10;
    private Rigidbody2D _rigidBody;
    private Vector2 _destination;
    private Vector2 _direction;
    private Animator _animator;
    private Coroutine _navigate;
    private Coroutine _attack;
    private SpriteRenderer _spriteRenderer;
    private PlayerController _player;


    // Use this for initialization
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _navigate = StartCoroutine(CoMoveToNextPosition());
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    void SetNewDestination()
    {
        _destination = Random.insideUnitCircle;
        
        //Get the two positions (ours and destination) in x,y space and 
        //normalize it - make it one based, so its a max of (1,1) which makes it easy to use for calcs later.
        //Granted - it is already one based because Random.insideUnitCircle gives us a position with a max
        //of one unit away, but as a best practice in case we choose locations further away in the future,
        //lets normalize it.
        _direction = ((Vector2)(transform.position) - ((Vector2)transform.position + _destination)).normalized;

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Collision happened, time to find a new path.
        SetNewDestination();
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        //If we're stuck in a collision let's try something new
        SetNewDestination();
    }

    /// <summary>
    /// When the larger circle around our enemy is entered, this will be called
    /// </summary>
    /// <param name="collision"></param>
    public void OnTriggerEnter2D(Collider2D collision)
    {

        //If the player entered our space start attacking 
        if (collision.gameObject.tag == "Player")
        {

            StopCoroutine(_navigate);
            //flip right or left. We naturally look to the left
            //If player is to our right, flip to right.
            _spriteRenderer.flipX = collision.transform.position.x > transform.position.x;

            //Start the attack
            _attack = StartCoroutine(CoAttack());
        }
    }

    public void Died()
    {
        StopAllCoroutines();
        //Disable colliders & Disable physics so players can walk over us
        _rigidBody.isKinematic = true;
        var components = GetComponents<Collider2D>();
        for (int i = 0; i <= components.GetUpperBound(0); i++)
        {
            components[i].enabled = false;
        }
    }
    /// <summary>
    /// Our attack routine. Attack until we're told not to.
    /// </summary>
    /// <returns></returns>
    private IEnumerator CoAttack()
    {
        //Prevent us from being pushed while attacking.
        _rigidBody.isKinematic = true;

        //Wait for a few seconds before attacking
        _animator.SetBool("Walk", false);
        _animator.SetTrigger("Attack");
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1, 4));
            _animator.SetTrigger("Attack");

            //TODO: Deal damage (we'll be in range, if not this coroutine would have been shut down)
            _player.GotHit(AttackStrength);
        }
    }

    /// <summary>
    /// Called when someone leaves our outer circle area.
    /// </summary>
    /// <param name="collision"></param>
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Go back to navigating since we're no longer attacking.
            //Our orks are dumb.
            StopCoroutine(_attack);

            //Ok, we need to understand collision again.
            //Reenable the rigidbody. (We disabled it so the player couldn't push us around)
            _rigidBody.isKinematic = false;

            _navigate = StartCoroutine(CoMoveToNextPosition());
        }
    }

    /// <summary>
    /// Each physics frame move towards our target position.
    /// </summary>
    /// <returns></returns>
    IEnumerator CoMoveToNextPosition()
    {

        while (true)
        {
            if (_destination == Vector2.zero)
            {
                SetNewDestination();
                Debug.Log("Normalized:" + _direction);
            }

            _animator.SetBool("Walk", true);
            //blindly move until we hit something or get triggered

            _rigidBody.MovePosition((Vector2)transform.position + _direction * Time.deltaTime * Speed);

            //As a general rule,physics should really be done every FixedUpdate() not Update
            //FixedUpdate() is timed to the physics engine calcuations
            yield return new WaitForFixedUpdate();
        }
    }
}
