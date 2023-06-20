using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TupObject : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            IInitialiser[] initialisers = FindObjectsOfType<MonoBehaviour>(true).OfType<IInitialiser>().ToArray();

            foreach (var item in initialisers)
            {
                item.Initialise();
            }
            
        }
    }

}
