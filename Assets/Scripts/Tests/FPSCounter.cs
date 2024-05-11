using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    private void OnGUI()
    {
        GUILayout.Label("FPS: " + 1.0f / Time.deltaTime);
    }
}
