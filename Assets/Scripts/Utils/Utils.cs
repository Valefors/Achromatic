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
    public static string TURN_ON_INTERACTION = "Too quiet, let's turn on this TV.";
    public static string TURN_OFF_INTERACTION = "Too noisy, let's turn off.";

    //Color light
    public static Color lightColor = new Color(0.509f, 0.509f, 0.509f);
    public static Color lightOff = new Color(0f, 0f, 0f);
    public static Color lightOn = new Color(118, 118, 118);


    //SCENE NAME
    public static int MENU_SCENE = 0;
    public static int LOADING_SCENE = 1;
    public static int INGAME_SCENE = 2;

    //Layers
    public static int OBJECT_LAYER = 9;
    public static int DEFAULT_LAYER = 0;

    //Lights
    public static int TURN_ON_LIGHT_DELAY = 15;
    public static int TURN_OFF_LIGHT_DELAY = 25;

    //Skip Delay
    public static int SKIP_DELAY = 4;

    //Options Value
    public static float MOUSE_SENSIBILITY = 250;
    public static Enums.ELanguage LANGUAGE = Enums.ELanguage.FRENCH;
    public static int QUALITY_LEVEL = 2;
    public static bool FULLSCREEN = true;

    //Detection Player
    public static int PLAYER_DETECTION = 4;
}
