using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAnimationController : MonoBehaviour
{
    // Start is called before the first frame update
    Animator _ani;

    float _moveValue = 0;


    void Start()
    {
        _ani = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        // HeroSetAnimation();
        DirectionAnimation();
        //behavierAnimation();
    }

    void HeroSetAnimation()
    {
        if (Input.GetKey(KeyCode.W))
        {
            _moveValue += Time.deltaTime;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                _moveValue += Time.deltaTime * 2;
            }
        }
        else
        {
            _moveValue -= Time.deltaTime;
        }
        if (_moveValue < 0) _moveValue = 0;
        if (_moveValue > 1) _moveValue = 1;
        _ani.SetFloat("MoveValue", _moveValue);
    }

    void DirectionAnimation()
    {
       
        float dirX = Input.GetAxis("Horizontal");
        float dirZ = Input.GetAxis("Vertical");

        GetComponent<Rigidbody>().velocity = new Vector3(dirX, 0, dirZ) * 3;

        int isSprint = 1;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isSprint = 2;
        }

        _ani.SetFloat("AxisX", dirX);
        _ani.SetFloat("AxisZ", dirZ * isSprint);
    }

    //void behavierAnimation()
    //{
    //    float timer = 0;
    //    int WaitTime = 5;
    //    timer += Time.deltaTime;

    //    if (timer > WaitTime)
    //    {
    //        _ani.SetLayerWeight(1, 0);
    //        Debug.Log("대기시간 초과");
    //    }
    //    if (Input.GetKey(KeyCode.A))
    //    {
            
    //        timer = 0;
    //        _ani.SetLayerWeight(1, 1);
           
    //    }

    //}
}

