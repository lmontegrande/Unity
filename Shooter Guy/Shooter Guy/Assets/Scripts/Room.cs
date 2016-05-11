using UnityEngine;
using System.Collections;

public class Room : MonoBehaviour {

    public GameObject up;
    public GameObject down;
    public GameObject left;
    public GameObject right;

    [SerializeField]
    private GameObject roomObjects;

    [SerializeField]
    private GameObject[] entryways;

    void Start()
    {
        SetUpDoors();
        Deactivate();
    }

	void SetUpDoors()
    {
        if (entryways == null)
        {
            return;
        }

        foreach (GameObject entry in entryways)
        {
            Unlock(entry);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameManager.instance.ChangeRoom(gameObject);
        }
    }

    public void Deactivate()
    {
        roomObjects.SetActive(false);
    }

    public void Activate()
    {
        roomObjects.SetActive(true);
    }

    public void Unlock(GameObject entry)
    {
        entry.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, .2f);
        entry.gameObject.layer = 10;
    }

    public void Lock(GameObject entry)
    {
        entry.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        entry.gameObject.layer = 0;
    }

    public void AddObjects(GameObject objects)
    {
        objects.transform.parent = roomObjects.transform;
    }
}
