using Assets.SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class ReadJSON
{
    public static string fileName = "JSON/Difficulty.json";
    public static SimpleJSON.JSONNode json;

    public static void StartReadingJSON()
    {
        string str = Read();

        json = SimpleJSON.JSON.Parse(str);
    }

    private static string Read()
    {
        StreamReader sr = new StreamReader(Application.dataPath + "/StreamingAssets/" + fileName);
        string lContent = sr.ReadToEnd();
        sr.Close();
        Debug.Log(lContent);
        return lContent;
    }

    public static string GetCorrectTextInteraction(string pLabel)
    {
        if (json[pLabel] == null)
        {
            Debug.LogError(pLabel + " doesn't exist");
            return "LABEL NOT DEFINED";
        }

        return json[pLabel][Utils.DIFFICULTY_MODE].Value;
        //print(json[pLabel]["easy"].Value);
    }

}
