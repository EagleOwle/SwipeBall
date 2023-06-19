using UnityEngine;

[CreateAssetMenu]
public class StaticData : ScriptableObject
{
    public GameObject playerPrefab;
    public float playerSpeed;
    public float followSpeed;

    public bool IsMobilePlatform
    {
        get
        {
#if UNITY_EDITOR || UNITY_STANDALONE
            isMobilePlatform = false;
#else
            isMobilePlatform = true;
#endif

            return isMobilePlatform;
        }
    }
    private bool isMobilePlatform;
    public float DeadZone;
}
