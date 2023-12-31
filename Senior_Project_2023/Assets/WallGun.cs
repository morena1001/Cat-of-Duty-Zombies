using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallGun : MonoBehaviour
{
    // Checks if player is in range to buy gun from wall
    public bool buy = false;
    int price = 0;
    
    void Start()
    {
        string name = transform.GetChild(0).name;
        switch(name)
        {
            case "Sniper":
                price = 2000;
            break;

            case "Shotgun":
                price =  3000;
            break;

            case "Rifle":
                price = 3500;
            break;
        }
    }

    
    void Update()
    {
        
    }


    void OnTriggerStay2D(Collider2D col)
    {
        if (col.transform.name == "pawl") 
        {
            buy = true;
            Interact.turn_on(isAffordable());
            Cost_Popup.show_price(price, isAffordable());
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.transform.name == "pawl")   
        {
            buy = false;
            Interact.turn_off();
            Cost_Popup.hide_price();
        }
    }

    bool isAffordable() {
        return PointsManager.PointValue >= price;
    }
}
