using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BlueCoin : MonoBehaviour
{
    [SerializeField] GameObject bludeCoinParent;
    public int GetCoin()
    {
        Destroy(bludeCoinParent);   
        return 1;
    }
}
