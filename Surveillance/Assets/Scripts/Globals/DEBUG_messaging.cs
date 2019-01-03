using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public static class DEBUG_messaging {

    public static void warningPopup(string message)   {

        Text debugText = GameObject.Find("DEBUG_Text").GetComponent<Text>();

        if(debugText) debugText.text = message;

    }
}
