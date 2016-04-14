using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{

    public int InitialHealth;
    private int _health;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private bool _dead;

    // Use this for initialization

    void Start()
    {
        _health = InitialHealth;
        //We use this to flash it red and to fade it away.
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();

    }

    public void GotHit(int hitPoints)
    {
        if (_dead) return;

        _health -= hitPoints;
        StartCoroutine(FlashRed());

        if (_health <= 0)
        {
            _dead = true;
            //Kill me!
            //1. Disable EnemyController. We're not 'alive' anymore
            var enemyController = GetComponent<EnemyController>();
            enemyController.Died();
            enemyController.enabled = false;

            //3. Play death animation
            _animator.SetTrigger("Death");

            //4. Fade me out cause I'm dead
            StartCoroutine(FadeAway());
        }
    }

    IEnumerator FlashRed()
    {
        //Swap out the color for a damage color
        _spriteRenderer.material.color = Color.red;
        yield return new WaitForSeconds(.2f);
        //restore the color
        _spriteRenderer.material.color = Color.white;

    }

    IEnumerator FadeAway()
    {
        //Ensure any color flashing is complete and other animations (death) have finished
        yield return new WaitForSeconds(2);
        while (_spriteRenderer.color.a > 0)
        {
            var color = _spriteRenderer.color;
            //color.a is 0 to 1. So .5*time.deltaTime will take 2 seconds to fade out
            color.a -= (.5f * Time.deltaTime);

            _spriteRenderer.color = color;
            //wait for a frame
            yield return null;
        }
        Destroy(gameObject);
    }
}
