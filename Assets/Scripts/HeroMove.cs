using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMove : MonoBehaviour
{

    [SerializeField] Transform _cam;
    Animator _ani;

    private void Awake()
    {
        _ani = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        Move();   
    }

   void Move()
   {
        transform.rotation = Quaternion.LookRotation(new Vector3(_cam.transform.forward.x, 0, _cam.transform.forward.z));
        float vX = Input.GetAxisRaw("Horizontal");
        float vZ= Input.GetAxisRaw("Vertical");
        float vY = GetComponent<Rigidbody>().velocity.y;

        Vector3 forward = transform.forward;
        Vector3 right = transform.right;
        Vector3 v3 = forward * vZ + right * vX;
        float speed = 4.5f;
        if (Input.GetKey(KeyCode.LeftShift)) speed = 9f;
        Vector3 vYz = v3.normalized * speed;
        vYz.y += vY;

        _ani.SetFloat("AxisX", vX);
        _ani.SetFloat("AxisZ", vZ);
        _ani.SetFloat("MoveValue", speed > 4.5f ? 2f : 1f);

        if (Input.GetMouseButton(0)) _ani.SetTrigger("Attack");

        GetComponent<Rigidbody>().velocity = vYz;

        GetComponent<Rigidbody>().velocity = vYz;


    }
}
