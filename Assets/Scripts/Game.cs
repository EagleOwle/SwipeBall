using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private GameMenu gameMenu;
    [SerializeField] private Environment environment;

    private void Start()
    {
        environment.Initialise();
        gameMenu.Initialise();
    }

}
