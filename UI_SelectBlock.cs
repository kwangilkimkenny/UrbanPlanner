using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_SelectBlock : MonoBehaviour
{
    public Dropdown BlockList;
    //public var  selectedValue;

    private void Start()
    {
        SelectBlock();
    }

    public void SelectBlock()
    {
        BlockList.options.Clear();
        for (int i = 0; i < 82; i++)
        {
            Dropdown.OptionData option = new Dropdown.OptionData();
            option.text = "Block " + i.ToString();
            BlockList.options.Add(option);
        }
    }

    public void SButton()
        {
        Debug.Log("Selected Block : " + BlockList.value + ", List Selected :" +  (BlockList.value + 1));
        //selectedValue = BlockList.value;

        }



}

