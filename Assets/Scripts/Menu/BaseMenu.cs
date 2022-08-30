using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseMenu : MonoBehaviour
{
    protected ManagerMenu managerMenu;

    public virtual void Initialise(ManagerMenu managerMenu)
    {
        this.managerMenu = managerMenu;
    }

    public virtual void Show()
    {
        gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }
}
