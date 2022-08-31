using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colorize : MonoBehaviour
{
    [SerializeField] private Color[] colors;
    [SerializeField] private Renderer render;

    private void OnEnable()
    {
        int rnd = Random.Range(0, colors.Length);
        Color color = colors[rnd];
        render.material.color = color;
    }

}
