using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Inventory : MonoBehaviour
{
    [SerializeField] GameObject _inventoryPanel;
    [SerializeField] Sprite[] _coins;
    [SerializeField] Transform _content;
    [SerializeField] GameObject _itemPreb;

    List<Item> lstitems = new List<Item>();

    public void OpenOrClose()
    {
        _inventoryPanel.SetActive(!_inventoryPanel.activeSelf);
    }

    public void AddItem(Item _item)
    {

        foreach (Sprite sp in _coins)
        {
            if (_item._eType.ToString().Equals(sp.name))
            {
                _item._sprite = sp;
            }
        }

        //foreach (Sprite sp in _coins) 
        //{
        //    foreach(EItemType type in Enum.GetValues(typeof(EItemType))) 
        //    {
        //        if(type.ToString().Equals(sp.name))
        //        {
        //            _item._sprite = sp;
        //        }
        //    }
        //}
        
        lstitems.Add(_item);
        GameObject temp = Instantiate(_itemPreb, _content);
        temp.GetComponent<ItemUI>().Init(_item);
    }

}
public enum EItemType
{
    none,
    Blue,
    Green,
    Gold,
    Brown,
    Purple,
    Max
}

public class Item
{
    public Sprite _sprite;
    public int _coinCount;
    public EItemType _eType;
}
