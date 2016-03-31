using UnityEngine;
using System.Collections;

public class UnlockLevel : MonoBehaviour {

	public void Start()
    {
        GameManager.instance.levelTwoUnlocked = true;
    }
}
