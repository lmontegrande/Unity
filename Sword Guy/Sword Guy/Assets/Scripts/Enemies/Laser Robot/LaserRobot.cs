﻿using UnityEngine;
using System.Collections;

public class LaserRobot : Enemy {

    [SerializeField]
    private GameObject cannon, laser;

    [SerializeField, Range(0f, 1f)]
    private float soundVolume = .5f;

    [SerializeField]
    private AudioClip hitSound, laserSound;

    [SerializeField]
    private int health = 20, damage = 10;

    private AudioSource audioSource;
    private SpriteRenderer spriteRenderer;
    private Animator anim;
    private bool facingLeft;


	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (transform.rotation.y != 180)
            facingLeft = true;

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
            other.gameObject.GetComponent<SwordGuy>().GetHit(damage);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag != "Player")
            return;

        Transform otherTransform = other.gameObject.transform;

        if (otherTransform.position.x < transform.position.x) // If player on the left
        {
            if (!facingLeft)
                Flip();

            Attack();
        }
        else
        {
            if (facingLeft)
                Flip();

            Attack();
        }
    }

    void Attack()
    {
        anim.SetTrigger("attack");
    }

    void Flip ()
    {
        facingLeft = !facingLeft;
        transform.Rotate(new Vector3(0, 180, 0));
    }

    public override void GetHit(int damage)
    {
        audioSource.PlayOneShot(hitSound, soundVolume);
        anim.SetTrigger("gotHit");
        health -= damage;
        if (health <= 0)
        {
            DestroySelf();
        }
    }

    public void Fire()
    {
        audioSource.PlayOneShot(laserSound, soundVolume);
        GameObject temp = (GameObject)Instantiate(laser, cannon.transform.position, Quaternion.identity);
        temp.GetComponent<Laser>().FireLaser(facingLeft, damage);
    }

    public override void DestroySelf()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        Destroy(gameObject, 1f);
    }
}