using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Acces
{
    Free,
    Blocked,
    Available,
}

[System.Serializable]
public class PresentPreference
{
    public string name;
    public Acces acces;
    public Ball ballPrefab;
    public UIPresentViewe viewePrefab;
    public bool current;

}
