using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMove : MonoBehaviour
{
    [SerializeField] GameObject _gameObjectUI;
    [SerializeField] Collider _sword;
    [SerializeField] Transform _cam;
    Animator _ani;

    public int HP = 5;
    int _coin = 0;

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

        if (Input.GetMouseButton(0))
        {
            _sword.enabled = true;
            _ani.SetLayerWeight(1, 0.5f);
            _ani.SetTrigger("isAttack");
        }
        else _ani.SetLayerWeight(1, 0.5f);  
            

        GetComponent<Rigidbody>().velocity = vYz;

        GetComponent<Rigidbody>().velocity = vYz;


    }

    public void hitted()
    {
        if (!canHitted) return;
        HP--;
        if (HP <= 0)
        {
            _gameObjectUI.SetActive(true);
            _ani.Play("Die");
            Time.timeScale = 0;

        }
        else
        {
            _ani.SetLayerWeight(2, 1);
            _ani.SetTrigger("isHitted");
            _ani.SetLayerWeight(2, 0);
        }
        canHitted = false;
        StartCoroutine(CoHittedCoolTime());

    }

    bool canHitted = true;

    IEnumerator CoHittedCoolTime()
    {
        yield return new WaitForSeconds(1f);
        canHitted = true;
    }


   
    void EndAttack()
    {
        _sword.enabled = false;
        Debug.Log("input end Attack");
    }
    
    public void AddCoin()
    {
        _coin++;
    }
}
