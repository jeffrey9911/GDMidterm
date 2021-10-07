using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_inter : MonoBehaviour
{
    public GameObject _player;
    public GameObject _door0;
    public GameObject _door1;
    public GameObject _door2;
    public GameObject _door3;
    public GameObject _door4;
    public GameObject _door5;

    private RaycastHit rayCout;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.F))
        {
            float transZ;
            float transX;
            switch (isNearDoor(false))
            {
                
                case 0:
                    transZ = (_player.transform.position.z < midPoint(_door0, _door1).z) ? 2.0f : -2.0f;
                    _player.transform.position += new Vector3(0.0f, 0.0f, transZ);
                    break;

                case 1:
                    transX = (_player.transform.position.x < midPoint(_door2, _door3).x) ? 2.0f : -2.0f;
                    _player.transform.position += new Vector3(transX, 0.0f, 0.0f);
                    break;

                case 2:
                    transX = (_player.transform.position.x < midPoint(_door4, _door5).x) ? 2.0f : -2.0f;
                    _player.transform.position += new Vector3(transX, 0.0f, 0.0f);
                    break;
                default:
                    break;
            }
        }
    }

    Vector3 midPoint(GameObject obj0, GameObject obj1)
    {
        return (obj1.transform.position + obj0.transform.position) / 2;
    }

    float calcDistance(Vector3 vec1, Vector3 vec2)
    {
        return (vec2 - vec1).sqrMagnitude;
    }

    int isNearDoor(bool isDebugging)
    {
        int doorSetInd = -1;
        if (calcDistance(_player.transform.position, midPoint(_door0, _door1)) <= 1f)
            doorSetInd = 0;
        if (calcDistance(_player.transform.position, midPoint(_door2, _door3)) <= 1f)
            doorSetInd = 1;
        if (calcDistance(_player.transform.position, midPoint(_door4, _door5)) <= 1f)
            doorSetInd = 2;

        if(isDebugging)
            Debug.Log("doorSetInd: " + doorSetInd);
        return doorSetInd;
    }
}
