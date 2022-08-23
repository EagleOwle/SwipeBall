using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeCalculate : MonoBehaviour
{
    #region SINGLETON
    private static SwipeCalculate _instance;
    public static SwipeCalculate Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<SwipeCalculate>();
            }
            return _instance;
        }
    }
    #endregion

    public bool OnSwipe => onSwipe;
    public Vector2 SwipeScreenDirection => swipeDirection;
    public Vector2 TupScreenPosition => tupPosition;

    [SerializeField] private float deadZone = 10;
    [SerializeField] private Vector2 tupPosition;
    [SerializeField] private Vector2 swipeDirection;

    private bool onSwipe = false;
    private bool isMobilePlatform;
    private bool isDraggin = false;

    private void Awake()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        isMobilePlatform = false;
#else 
        isMobilePlatform = true;
#endif
    }

    private void Start()
    {
        isDraggin = false;
        ResetSwipe();
    }

    private void Update()
    {
        ControlInput();
        Swipe();
    }

    private void ControlInput()
    {
        if (!isMobilePlatform)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isDraggin = true;
                tupPosition = Input.mousePosition;
            }

            if (Input.GetMouseButtonUp(0))
            {
                isDraggin = false;
                ResetSwipe();
            }
        }
        else
        {
            if (Input.touchCount > 0)
            {
                if (Input.touches[0].phase == TouchPhase.Began)
                {
                    isDraggin = true;
                    tupPosition = Input.touches[0].position;
                }

                if (Input.touches[0].phase == TouchPhase.Canceled || Input.touches[0].phase == TouchPhase.Ended)
                {
                    isDraggin = false;
                    ResetSwipe();
                }
            }
            else
            {
                isDraggin = false;
            }
        }
    }

    private void Swipe()
    {
        if (isDraggin == false) return;

        swipeDirection =  (Vector2)Input.mousePosition - tupPosition;

        if (swipeDirection.magnitude > deadZone)
        {
            onSwipe = true;
        }
        else
        {
            onSwipe = false;
        }
    }

    public void ResetSwipe()
    {
        onSwipe = false;
        isDraggin = false;
        tupPosition = Vector2.zero;
        swipeDirection = Vector2.zero;
    }

}
