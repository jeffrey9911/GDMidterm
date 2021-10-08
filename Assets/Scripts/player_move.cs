using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_move : MonoBehaviour
{
// pbcV
    public GameObject _gameObj;
    public CharacterController chraController;
    public Animator _animator;
    public float _gravity = -9.8f;
    public float dtMultiplier = 1.0f;
    public float _jumpForce = 0.5f;

// prtV
    private RaycastHit rayCout;
    private float veloY;
    private int rotAng;

    // Start is called before the first frame update
    void Start()
    {
        chraController = gameObject.GetComponent<CharacterController>();
        veloY = _gravity;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        calcVeloY(false);
        isCollidingVertically(false);
        calcRotAng(false);

        _animator.SetInteger("AnimatorState", rotAng);
        if (rotAng != -1)
        {
            _gameObj.transform.rotation = Quaternion.Euler(0, Camera.main.transform.rotation.eulerAngles.y + rotAng, 0);
        }

        if (Input.GetKey(KeyCode.Space) && isCollidingVertically(false))
        {
            veloY += _jumpForce;
        }

        

        _gameObj.transform.position += new Vector3(0f, veloY, 0f) * Time.deltaTime * dtMultiplier;
    }

    bool isCollidingVertically(bool isDebugging)
    {
        if (isDebugging)
            Debug.Log("rayCout.distance: " + rayCout.distance);

        if (!Physics.Raycast(_gameObj.transform.position, Vector3.down, out rayCout))
        {
            return false;
        }
        
        if (rayCout.distance <= 0.155)
        {
            veloY = 0f;
            return true;
        }


        return false;
    }

    void calcVeloY(bool isDebugging)
    {
        veloY -= (veloY <= _gravity) ? 0 : Time.deltaTime;
        if (isDebugging)
            Debug.Log("veloY: " + veloY);
    }

    void calcRotAng(bool isDebugging)
    {
        rotAng = -1;

        if (Input.GetKey(KeyCode.A))
            rotAng = 270;
        if (Input.GetKey(KeyCode.D))
            rotAng = 90;

        if (Input.GetKey(KeyCode.W))
        {
            rotAng = 0;
            rotAng += (Input.GetKey(KeyCode.D)) ? 45 : 0;
            rotAng += (Input.GetKey(KeyCode.A)) ? -45 : 0;
        }
        if(Input.GetKey(KeyCode.S))
        {
            rotAng = 180;
            rotAng += (Input.GetKey(KeyCode.D)) ? -45 : 0;
            rotAng += (Input.GetKey(KeyCode.A)) ? 45 : 0;
        }

        if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.A))
            rotAng = 0;
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S))
            rotAng = 0;

        if (isDebugging)
            Debug.Log("rotAng: " + rotAng);
    }
}
