using System.Collections.Generic;
using BubbleGumGuy;
using Core;
using UnityEngine;
using UnityEngine.UI;

public class SgUi : MonoBehaviour
{
    private Inventory _inventory;
    
    [SerializeField]
    Image SgImage;
    
    [SerializeField]
    List<Image> AmmoImages;
    
    void Start()
    {
        _inventory = The.Me.GetComponent<Inventory>();
    }

    void Update()
    {
        int ammo = _inventory.GetAmmo(); 
        SgImage.enabled = ammo > 0;
        AmmoImages[0].enabled = ammo > 0;
        AmmoImages[1].enabled = ammo > 1;
        AmmoImages[2].enabled = ammo > 2;
    }
}
