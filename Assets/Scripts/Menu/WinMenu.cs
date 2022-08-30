using UnityEngine;
using UnityEngine.UI;

public class WinMenu : BaseMenu
{
    [SerializeField] private Button restartButton;
    [SerializeField] private Button exitButton;

    public override void Initialise(ManagerMenu managerMenu)
    {
        base.Initialise(managerMenu);
        restartButton.onClick.AddListener(RestartScene);
        exitButton.onClick.AddListener(ExitMenu);
    }

    private void RestartScene()
    {
        managerMenu.RestartScene();
    }

    private void ExitMenu()
    {
        managerMenu.ExitScene();
    }

}
