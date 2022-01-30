using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RaveColour : MonoBehaviour
{
    private Color colour;

    // Update is called once per frame
    void Update()
    {
        GetComponent<TextMeshProUGUI>().color = Color.Lerp(Color.blue, Color.red, Mathf.PingPong(Time.time, 1));
    }
}
