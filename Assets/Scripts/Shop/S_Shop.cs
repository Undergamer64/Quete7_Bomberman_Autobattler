using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Shop : MonoBehaviour
{
    public void ActivateButtonsEvent()
    {
        S_ShopManager.Instance.ChangeButtons(true);
    }

    public void DeactivateButtonsEvent()
    {
        S_ShopManager.Instance.ChangeButtons(false);
    }

    public void CloseShopEvent()
    {
        S_ShopManager.Instance.ShopClosed();
    }
}
