using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace MARSViking
{
    public class Init : MonoBehaviour
    {
        [InitializeOnLoadMethod]
        public static void SetupNewProject()
        {
            Debug.Log("Init Viking");
            
            var projectRootPath = Directory.GetCurrentDirectory();
            var projectManifestPath = Path.Combine(projectRootPath, "Packages", "manifest.json");
            var lines = File.ReadAllLines(projectManifestPath).ToList();
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

            File.WriteAllLines(projectManifestPath, lines);
            
        }
    }
}
