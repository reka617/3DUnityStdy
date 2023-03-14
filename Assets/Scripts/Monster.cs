using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;

public enum EMonState
{
    None,
    Idle,
    Move,
    Attack,
    Die,
}

public class Monster : MonoBehaviour
{
    [SerializeField] GameObject _mon;
    [SerializeField] Transform _target;
    [SerializeField] GameObject _coin;
    [SerializeField] GameObject _attackSpace;
    [SerializeField] Sprite[] _coins;
    [SerializeField] Inventory _inven;


    //일정 랜덤한 시간 이 지나면 스폰
    EMonState _eState = EMonState.None;

    Animator _ani;

    float _searchDis = 10;
    float _attackDis = 2;
    float _speed = 2;

    int _hp =1;

    // Start is called before the first frame update
    void Start()
    {
        _ani = _mon.GetComponent<Animator>();
        StartCoroutine(CoSpawn());
    }
    private void Update()
    {
        if (_eState == EMonState.Idle) moveAndSearch();
        if (_eState == EMonState.Attack) followAndAttack();
        if (_eState == EMonState.Die) DieEnd();
        _attackSpace.SetActive(_eState== EMonState.Attack);
    }
    void followAndAttack()
    {
        if (_ani.GetCurrentAnimatorStateInfo(0).IsName("Hitted") == false || _ani.GetCurrentAnimatorStateInfo(0).normalizedTime >=1)
        {
            _ani.Play("Attack");
        }
        float dis = Vector3.Distance(_target.position, transform.position);
        if(dis > _attackDis)
        {
            if(dis > _searchDis)
            {
                _eState = EMonState.Idle;
            }
            else
            {
                Vector3 lookDir = _target.position - transform.position;
                transform.rotation = Quaternion.LookRotation(new Vector3(lookDir.x, 0, lookDir.z));
                transform.position = Vector3.MoveTowards(transform.position, _target.position, Time.deltaTime * _speed);
            }
        }
    }
    IEnumerator CoSpawn()
    {
        int rand = Random.Range(2,5);
        yield return new WaitForSeconds(rand);
        Spawn();
    }

    void Spawn()
    {
        _mon.SetActive(true);
        _eState = EMonState.Idle;
    }


    void moveAndSearch()
    {
        //근처에 영웅이 있나 탐색
        // 있으면 영웅쪽으로 이동하고 공격
        // 없으면 상태를 move 로 바꾼다음에 랜덤한 위치를 정해서 이동

        if (_ani.GetCurrentAnimatorStateInfo(0).IsName("Hitted") == false)
        {
            _ani.Play("Idle");
        }
        float dis = Vector3.Distance(_target.position, transform.position);
        if(dis  < _searchDis) // 영웅 찾았을때
        {
            if(dis < _attackDis)
            {
                _eState = EMonState.Attack;
            }
            else
            {
                Vector3 lookDir = _target.position - transform.position;
                transform.rotation = Quaternion.LookRotation(new Vector3(lookDir.x, 0, lookDir.z));
                transform.position = Vector3.MoveTowards(transform.position, _target.position, Time.deltaTime * _speed);
                // transform.translate((target.position - transform.position) * time.deltatime * _speed);
                //transform.position += (target.position - transform.position) * time.deltatime * _speed;
            }
        }
        else
        {
            _eState = EMonState.Move;
            StartCoroutine(CoRandomMove());
        }
    }

    IEnumerator CoRandomMove()
    {
        //랜덤 방향을 쳐다본다.
        //랜덤 방향으로 이동할 수 있는지 본다.
        //이동할 수 없으면 아이들로 전환
        //이동할 수 있으면 해당 위치로 천천히 이동한다.
        Vector3 randomDir = new Vector3(Random.Range(-1f,1f), 0 , Random.Range(-1f,1f));
        transform.rotation = Quaternion.LookRotation(randomDir);

        yield return new WaitForSeconds(1f);


        // 앞에 벽이 있는지 체크해서 벽이 있으면 방향바꿈

        Vector3 front = transform.position + transform.forward + new Vector3(0, 0.5f, 0);
        RaycastHit hit;
        Vector3 targetDir = transform.position + transform.forward * 2;//movedir
        bool canMove = false;
        if (!Physics.Raycast(front, transform.forward * 2, out hit, 2))
        {
            canMove = true;
        }


        if (canMove)
        {
            Debug.DrawRay(targetDir + new Vector3(0, 0.5f, 0), new Vector3(0, -2f, 0), Color.red, 5);
            if (Physics.Raycast(targetDir + new Vector3(0, 0.5f, 0), new Vector3(0, -2f, 0), out hit, 2))
            {
                while (Vector3.Distance(transform.position, targetDir) > 0.1)
                {
                    _ani.Play("Move");
                    transform.position = Vector3.MoveTowards(transform.position, targetDir, Time.deltaTime * _speed);
                    yield return null;
                }
            }
        }
        yield return new WaitForSeconds(1f);
        _eState = EMonState.Idle;
    }

    public void hitted()
    {
        if (!canHitted) return;
        _hp--;
        if(_hp <=0)
        {
            _ani.Play("Die");
            _eState = EMonState.Die;
        }
        else
        {
            _ani.Play("Hitted");
        }
        canHitted= false;
        StartCoroutine(CoHittedCoolTime());

    }

    bool canHitted = true;

    IEnumerator CoHittedCoolTime()
    {
        yield return new WaitForSeconds(1f);
        canHitted = true;
    }
    public void DieEnd()
    {
        Debug.Log("die");
        gameObject.SetActive(false);
        GameObject tmp = Instantiate(_coin);
        tmp.GetComponentInChildren<SpriteRenderer>().sprite = _coins[Random.Range(0,4)];
        tmp.transform.position= transform.position;
        _inven.getDropItemData(tmp);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Hero"))
        {
            other.GetComponent<HeroMove>().hitted();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Hero"))
        {
            other.GetComponent<HeroMove>().hitted();
        }
    }

}
