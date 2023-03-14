using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    [SerializeField] Image _image;
    [SerializeField] TMP_Text _text;

    public void Init(Item _item)
    {
        _image.sprite = _item._sprite;
        _text.text = _item._coinCount.ToString();
    }
}
