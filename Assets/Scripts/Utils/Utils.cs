using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils 
{
    // Rewired Input
    public static string REWIRED_OPTION_ACTION = "OptionScreen";
    public static string REWIRED_MOVE_HORIZONTAL_ACTION = "MoveHorizontal";
    public static string REWIRED_MOVE_VERTICAL_ACTION = "MoveVertical";
    public static string LEFT_CLICK_ACTION = "LeftClick";
    public static string RIGHT_CLICK_ACTION = "RightClick";

    //Interaction possible
    public static string ROTATING_OBJECT_INTERACTION = "I should have a look at it.";
    public static string MOVABLE_OBJECT_INTERACTION = "I feel like I have to do something with it...";
    public static string PUT_INTERACTION = "Let’s put it here.";
    public static string UNLOCK_INTERACTION = "You did it! W.I.P feature";
    public static string OPEN_INTERACTION = "Let’s open it.";
    public static string CLOSE_INTERACTION = "Let’s close it.";
    public static string TYPEWRITTER_INTERACTION = "Let’s make this clear.";
    public static string OTHER_HOLDING_INTERACTION = "You already have an object in hand!";

    //Color light
    public static Color lightColor = new Color(0.509f, 0.509f, 0.509f);

    //SCENE NAME
    public static int MENU_SCENE = 0;
    public static int LOADING_SCENE = 1;
    public static int INGAME_SCENE = 2;

    //Layers
    public static int OBJECT_LAYER = 9;
    public static int DEFAULT_LAYER = 0;
}
