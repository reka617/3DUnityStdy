using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BlueCoin : MonoBehaviour
{
    [SerializeField] GameObject blueCoinParent;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Hero"))
        {
            other.GetComponent<HeroMove>().AddCoin();
            Destroy(blueCoinParent);
        }
    }
}
