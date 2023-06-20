using UnityEngine;
using UnityEngine.UI;

public class GameMenu : BaseMenu
{
    [SerializeField] private Text itemCountText;
    [SerializeField] private Button pauseButton;

    private ItemHandler itemHandler;

    public override void Initialise(ManagerMenu managerMenu)
    {
        base.Initialise(managerMenu);
        pauseButton.onClick.AddListener(OnPauseButton);
    }

    public void Initialise(ItemHandler itemHandler, ManagerMenu managerMenu)
    {
        Initialise(managerMenu);
        this.itemHandler = itemHandler;
        itemHandler.EventChangeItemCount += OnChangeSceneItemCount;
        OnChangeSceneItemCount(itemHandler.CurrentItemCount());
        
    }

    private void OnChangeSceneItemCount(int value)
    {
        itemCountText.text = value.ToString();
    }

    private void OnPauseButton()
    {
        managerMenu.ShowPauseMenu();
    }
    private void OnDestroy()
    {
        if(itemHandler != null)
        {
            itemHandler.EventChangeItemCount -= OnChangeSceneItemCount;
        }
    }

}
