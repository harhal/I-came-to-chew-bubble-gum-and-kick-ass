using System.Collections.Generic;
using BubbleGumGuy;
using Core;
using UnityEngine;
using UnityEngine.UI;

public class BgUi : MonoBehaviour
{
    private Inventory _inventory;
    
    [SerializeField]
    Image BgMask;
    
    void Start()
    {
        _inventory = The.Me.GetComponent<Inventory>();
    }

    void Update()
    {
        float bgPrc = _inventory.GetBubbleGumPercentage();
        BgMask.fillAmount = bgPrc;
    }
}
