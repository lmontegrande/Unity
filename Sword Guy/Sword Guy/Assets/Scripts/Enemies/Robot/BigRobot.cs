using UnityEngine;
using System.Collections;

public class BigRobot : Robot {

    public string sceneToLoad = "Cutscene 0 - 1";

    public override void DestroySelf()
    {
        StartCoroutine(LoadNext());
    }

    IEnumerator LoadNext()
    {
        base.dead = true;
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(2f);
        GameManager.instance.levelOneUnlocked = true;
        GameManager.instance.LoadLevel(sceneToLoad);
    }
}
