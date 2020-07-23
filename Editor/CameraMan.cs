using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MARSViking
{
    
    public class CameraMan
    {
        public static void MoveTo()
        {
            var camera = GameObject.Find("MARS Session/Main Camera");
            camera.transform.position = Vector3.Lerp(camera.transform.position, new Vector3(0, 2, -0.01f), 1f);
        }
    }
}