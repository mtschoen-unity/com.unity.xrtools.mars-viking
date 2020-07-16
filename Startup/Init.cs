﻿﻿using System.IO;
  using System.Linq;
  using UnityEditor;
  using UnityEngine;

namespace MARSViking
{
    public class Init : MonoBehaviour
    {
        [InitializeOnLoadMethod]
        public static void SetupNewProject()
        {
            Debug.Log("Init Viking");
            
            string ProjectRootPath = Directory.GetCurrentDirectory();
            string ProjectManifestPath = Path.Combine(ProjectRootPath, "Packages", "manifest.json");
            var lines = File.ReadAllLines(ProjectManifestPath).ToList();
            var viking = lines.Find(l => l.Contains("com.unity.xrtools.mars-viking") && !l.Contains(":"));
            
            if (viking != null)
            {
                return;
            }
            var testables  = lines.Find(l => l.Contains("testables"));

            if (testables != null && !testables.Contains("com.unity.xrtools.mars-viking"))
            {
                var testablesIndex = lines.IndexOf(testables);
                var iterate = true;
                while (iterate)
                {
                    if (lines[testablesIndex].Contains("]"))
                    {
                        lines[testablesIndex] = lines[testablesIndex].Replace("]", ",\"com.unity.xrtools.mars-viking\"]");
                        iterate = false;
                    }
                    else
                    {
                        testablesIndex++;
                    }
                }

                
            }
            
            if (testables == null)
            {
                lines[lines.Count - 2] = "},";
                lines.Insert(lines.Count - 1, "\"testables\": [\"com.unity.xrtools.mars-viking\"]");
            }

            File.WriteAllLines(ProjectManifestPath, lines);
            
        }
    }
}
