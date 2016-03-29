using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {

    public AudioClip backgroundMusic;

    private static MusicManager instance;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(backgroundMusic);
    }

    void Awake()
    {
        if (MusicManager.instance == null)
        {
            instance = this;
        }
        else if (MusicManager.instance != this)
        {
            Destroy(gameObject);
        }
    }

}
