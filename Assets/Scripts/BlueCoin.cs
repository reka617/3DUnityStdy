using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BlueCoin : MonoBehaviour
{
    [SerializeField] GameObject bludeCoinParent;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Hero"))
        {
            other.GetComponent<HeroMove>().AddCoin();
            Destroy(bludeCoinParent);
        }
    }
}
