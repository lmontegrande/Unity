using UnityEngine;
using System.Collections;

public class Frame : MonoBehaviour {

    [SerializeField]
    GameObject top;

    private int frameNumber = 1;

	public GameObject GetTop()
    {
        return top;
    }
}
