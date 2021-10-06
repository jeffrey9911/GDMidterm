using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Scene : MonoBehaviour
{
    public Rigidbody _rigidbody;
    public Animator _animator;
    public float _gravity = -9.8f;
    private float _impG;
    bool bot_hit = false;





    // Start is called before the first frame update
    void Start()
    {
        _impG = _gravity;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        
        float veloX = Input.GetAxis("Horizontal");
        float veloZ = Input.GetAxis("Vertical");
        Vector3 veloInput = new Vector3(veloX, 0, veloZ);
        
        veloInput = Quaternion.Euler(0, Camera.main.transform.rotation.eulerAngles.y, 0) * veloInput;
        //_rigidbody.MovePosition(transform.position + veloInput * 10 * Time.deltaTime);
        //_rigidbody.velocity = veloInput;

        

        if(Input.GetKey(KeyCode.W))
        {
            _animator.SetInteger("AnimatorState", 1);
            _animator.SetFloat("isMovingForward", 1.0f);
            _rigidbody.rotation = Quaternion.Euler(0, Camera.main.transform.rotation.eulerAngles.y, 0);
        } 
        else if(Input.GetKey(KeyCode.S))
        {
            _animator.SetInteger("AnimatorState", 1);
            _animator.SetFloat("isMovingForward", -1.0f);
            _rigidbody.rotation = Quaternion.Euler(0, Camera.main.transform.rotation.eulerAngles.y, 0);
        }
        else if (Input.GetKey(KeyCode.Space) && bot_hit)
        {
            _rigidbody.velocity += (new Vector3(0, 10.0f, 0));
        }
        else
        {
            _animator.SetInteger("AnimatorState", 0);
        }
        
        _rigidbody.velocity += (new Vector3(0, _impG, 0) * Time.deltaTime);
        
    }

   

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.contacts[0].normal.y == 1)
        {
            _impG = 0.0f;
            bot_hit = true;
        }
        
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.contacts[0].normal.y == 1)
        {
            _impG = 0.0f;
            bot_hit = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.contactCount != 0)
        {
            Debug.Log(collision.contacts[0].normal);
            
            
        }
        _impG = _gravity;
        bot_hit = false;

    }
}
