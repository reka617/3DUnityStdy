using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    int _coin = 0;
  
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<BlueCoin>() != null)
        {
            _coin += other.gameObject.GetComponent<BlueCoin>().GetCoin();
            Debug.Log("My Coins : " + _coin);
        }
    }
}
