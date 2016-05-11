using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public GameObject currentRoom;
    public GameObject previousRoom;

    [SerializeField, Range(0, 5)]
    private float playerMoveAdjust = .2f;

    [SerializeField]
    private float
        xGenerateOffset = 20.5f,
        yGenerateOffset = 11.5f;

    [SerializeField]
    private bool autoGenerateMap = true;

    [SerializeField]
    private int mapSize = 10;

    [SerializeField]
    Vector3 startingPoint = new Vector3(0, 0, 0);

    [SerializeField]
    GameObject room;

    [SerializeField]
    GameObject[] roomObjects;

    private GameObject _player;
    private Camera _mainCamera;
    private float cameraMoveSpeed = 1f;

    private enum direction {up, down, left, right, none};

    void Awake()
    {
        GameManager.instance = this;

        _player = GameObject.FindGameObjectWithTag("Player");
        _mainCamera = Camera.main;

        if (autoGenerateMap)
        {
            MapGenerate();
        }
    }

    void MapGenerate()
    {
        direction previousDirection = direction.none;
        Vector3 currentLocation = startingPoint;
        GameObject currentRoom = null;
        GameObject previousRoom = null;

        for (int x=0; x < mapSize; x++)
        {
            currentRoom = Instantiate(room, currentLocation, Quaternion.identity) as GameObject;
            Room tempRoom = currentRoom.GetComponent<Room>();
            tempRoom.Unlock(tempRoom.up);
            tempRoom.Unlock(tempRoom.down);

            GameObject roomStuff = Instantiate(roomObjects[Random.Range(0, roomObjects.Length - 1)], currentLocation, Quaternion.identity) as GameObject;
            tempRoom.AddObjects(roomStuff);

            currentLocation += new Vector3(0, yGenerateOffset, 0);

            if (x==0)
            {
                tempRoom.Lock(tempRoom.down);
                Destroy(roomStuff);
            }
            if (x==mapSize-1)
            {
                tempRoom.Lock(tempRoom.up);
                Destroy(roomStuff);
            }

            //TODO: PROCEDURALLY GENERATE
            //
            //previousRoom = currentRoom;
            //switch (previousDirection)
            //{
            //    case direction.none:
            //        currentRoom = Instantiate(room, startingPoint, Quaternion.identity) as GameObject;
            //        break;
            //    case direction.up:
            //        break;
            //    case direction.down:
            //        break;
            //    case direction.left:
            //        break;
            //    case direction.right:
            //        break;
            //}
        }
    }

    public void ChangeRoom(GameObject room)
    {
        previousRoom = currentRoom;
        currentRoom = room;

        if (previousRoom != null)
            previousRoom.GetComponent<Room>().Deactivate();
     
        currentRoom.GetComponent<Room>().Activate();

        _player.transform.position += (currentRoom.transform.position - _player.transform.position).normalized * playerMoveAdjust;

        StartCoroutine(moveCamera());
    }

    private IEnumerator moveCamera()
    {
        Vector3 target = new Vector3(currentRoom.transform.position.x, currentRoom.transform.position.y, _mainCamera.transform.position.z);
        Time.timeScale = 0;
        while (_mainCamera.transform.position != target)
        {
            yield return null;
            Vector3 tempLoc = Vector3.MoveTowards(_mainCamera.transform.position, target, cameraMoveSpeed);
            _mainCamera.transform.position = tempLoc;
        }
        Time.timeScale = 1;
    }
}
