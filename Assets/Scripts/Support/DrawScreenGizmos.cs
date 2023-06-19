using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawScreenGizmos : MonoBehaviour
{
    private static DrawScreenGizmos instance;
    public static DrawScreenGizmos Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<DrawScreenGizmos>();
            }

            return instance;
        }
    }

    [SerializeField] private Camera camera;
    [SerializeField] private Canvas canvas;

    private Vector3 startPixelPos;
    private Vector3 endPixelPos;
    private bool draw = false;

    public void DrawLine(Vector3 startPixelPos, Vector3 endPixelPos)
    {
        this.startPixelPos = startPixelPos;
        this.endPixelPos = endPixelPos;
        draw = true;
    }

    private void OnDrawGizmos()
    {
        if (draw)
        {
            ScreenGizmos.DrawLine(canvas, camera, startPixelPos, endPixelPos);
        }
    }

}
