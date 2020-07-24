using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MARSViking
{
    
    public class CameraMan
    {
        public static IEnumerator MoveTo()
        {
            var camera = GameObject.Find("MARS Session/Main Camera");
            var counter = 20;
            while (!FoundSurface())
            {
                camera.transform.position += camera.transform.forward * 1 * Time.deltaTime;
                yield return null;
                System.Threading.Thread.Sleep(500);
                counter--;
            }
           
        }

        private static bool FoundSurface()
        {
            var surfacesContainer = GameObject.Find("Spatial UI/Surfaces");
            return surfacesContainer.transform.childCount > 0;
        }
    }
}