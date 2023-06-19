using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class FpsCalculate : MonoBehaviour
{
    [SerializeField] private Text text;

    const string display = "{0}";
    const float fpsMeasurePeriod = 1;

    private int fpsAccumulator = 0;
    private float fpsNextPeriod = 0;
    private int currentFps;

    private IEnumerator Start()
    {
        fpsNextPeriod = Time.realtimeSinceStartup + fpsMeasurePeriod;

        while (true)
        {
            fpsAccumulator++;

            yield return null;

            if (Time.realtimeSinceStartup > fpsNextPeriod)
            {
                currentFps = (int)(fpsAccumulator / fpsMeasurePeriod);
                fpsAccumulator = 0;
                fpsNextPeriod += fpsMeasurePeriod;
                text.text = string.Format(display, currentFps);
            }

#if UNITY_EDITOR
            if (EditorApplication.isPlaying == false)
            {
                break;
            }
#endif
        }
    }

//    public async void Start()
//    {
//        fpsNextPeriod = Time.realtimeSinceStartup + fpsMeasurePeriod;

//        while (true)
//        {
//            fpsAccumulator++;

//            await System.Threading.Tasks.Task.Yield();

//            if (Time.realtimeSinceStartup > fpsNextPeriod)
//            {
//                currentFps = (int)(fpsAccumulator / fpsMeasurePeriod);
//                fpsAccumulator = 0;
//                fpsNextPeriod += fpsMeasurePeriod;
//                text.text = string.Format(display, currentFps);
//            }


//#if UNITY_EDITOR
//            if (EditorApplication.isPlaying == false)
//            {
//                break;
//            }
//#endif
//        }
//    }

}
