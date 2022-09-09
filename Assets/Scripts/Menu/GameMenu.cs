using UnityEngine;
using UnityEngine.UI;

public class GameMenu : BaseMenu
{
    [SerializeField] private Text itemCountText;
    [SerializeField] private Button pauseButton;
    [SerializeField] private TutorialMenu tutorialMenu;

    public override void Initialise(ManagerMenu managerMenu)
    {
        base.Initialise(managerMenu);
        pauseButton.onClick.AddListener(OnPauseButton);
        tutorialMenu.Hide();
    }

    public void Initialise(IItemCount itemCount, ManagerMenu managerMenu)
    {
        Initialise(managerMenu);

        itemCount.ChangeItemCount += OnChangeSceneItemCount;
        OnChangeSceneItemCount(this, itemCount.CurrentItemCount());
        
    }

    private void OnChangeSceneItemCount(object sender, int value)
    {
        itemCountText.text = value.ToString();
    }

    private void OnPauseButton()
    {
        managerMenu.ShowPauseMenu();
    }

    public void ShowTutorialMemu()
    {
        tutorialMenu.Show();
    }

    public override void Show()
    {
        tutorialMenu.Hide();
    }

    public override void Hide()
    {
        tutorialMenu.Hide();
    }
}
