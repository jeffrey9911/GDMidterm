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

// prtV
    private RaycastHit rayCout;
    private float veloY;

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

    void FixedUpdate()
    {
        calcVeloY(false);
        isCollidingVertically(false);

        float veloX = Input.GetAxis("Horizontal");
        float veloZ = Input.GetAxis("Vertical");
        Vector3 veloInput = new Vector3(veloX, 0, veloZ);
        veloInput = Quaternion.Euler(0, Camera.main.transform.rotation.eulerAngles.y, 0) * veloInput;

        _animator.SetInteger("AnimatorState", 0);

        if (Input.GetKey(KeyCode.W))
        {
            _animator.SetInteger("AnimatorState", 1);
            _animator.SetFloat("isMovingForward", 1.0f);
            _gameObj.transform.rotation = Quaternion.Euler(0, Camera.main.transform.rotation.eulerAngles.y, 0);
            
        } 
        if(Input.GetKey(KeyCode.S))
        {
            _animator.SetInteger("AnimatorState", 1);
            _animator.SetFloat("isMovingForward", -1.0f);
            _gameObj.transform.rotation = Quaternion.Euler(0, Camera.main.transform.rotation.eulerAngles.y, 0);
        }
        if (Input.GetKey(KeyCode.Space) && isCollidingVertically(false))
        {
            veloY += 0.5f;
        }

        _gameObj.transform.position += new Vector3(0f, veloY, 0f) * Time.deltaTime * dtMultiplier;
    }

    bool isCollidingVertically(bool isDebugging)
    {
        if(isDebugging)
            Debug.Log("rayCout.distance: " + rayCout.distance);

        if(!Physics.Raycast(_gameObj.transform.position, Vector3.down, out rayCout))
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
}
