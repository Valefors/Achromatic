using Rewired;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionScreen : MonoBehaviour
{
    public static float MOUSE_SENSIBILITY = 120;

    public void OnClickSwitchControllersEn()
    {
        ReInput.players.GetPlayer(0).controllers.maps.SetMapsEnabled(false, 0);
        ReInput.players.GetPlayer(0).controllers.maps.SetMapsEnabled(true, 1);
    }

    public void OnClickSwitchControllersFr()
    {
        ReInput.players.GetPlayer(0).controllers.maps.SetMapsEnabled(false, 1);
        ReInput.players.GetPlayer(0).controllers.maps.SetMapsEnabled(true, 0);
    }
}
