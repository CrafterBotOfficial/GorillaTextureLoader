/*
Todo:
remove verbose logging bc it is way overkill
*/


// Gonna do away with the Debug and Release build options soon to make room for LemonLoader, hense the verbose logging definition.
#if DEBUG
#define LOG_VERBOSE
#endif

using BepInEx;
using BepInEx.Logging;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace TextureLoader
{
    [BepInPlugin("crafterbot.dumbmonkegame.textureloader", "TextureLoader", "2.0.0")]
    public class Main : BaseUnityPlugin
    {
        public static ManualLogSource LogSource;

        private void Start()
        {
            LogSource = Logger;
        }

#if DEBUG
        private void OnGUI()
        {
            if (GUILayout.Button("Change all materials in resources"))
            {
                var resourceMaterials = Resources.FindObjectsOfTypeAll<Material>();
                Texture texture = resourceMaterials[0].mainTexture;
                foreach (Material mat in resourceMaterials)
                {
                    LogVerbose("Changing " + mat + " to " + texture.name);
                    mat.mainTexture = texture;
                }
            }
            if (GUILayout.Button("Dump materials in scene"))
            {
                LogSource.LogInfo("Dump mats pressed");
                StringBuilder materialDump = new StringBuilder("===Material dump===");
                var materials = GameObject.FindObjectsOfType<GameObject>().Where(obj => obj.GetComponent<MeshRenderer>() is object);
                foreach (GameObject obj in materials)
                {
                    string message = string.Format("{0} has materials {1}",
                        GorillaExtensions.GTExt.GetPath(obj),
                        string.Join(", ", obj.GetComponent<MeshRenderer>()?.materials.Select(mat => mat.name)))
                        ;
                    LogVerbose(message);
                    materialDump.AppendLine(message);
                }
                File.WriteAllText(Path.GetDirectoryName(typeof(Main).Assembly.Location) + "/MaterialDump.txt", materialDump.ToString());
            }
        }
#endif

        #region Logging
        public static void LogVerbose(params string[] contents)
        {
#if LOG_VERBOSE
            LogSource.LogDebug(" [VERBOSE LOG]: " + string.Join(" ", contents));
#endif
        }
        #endregion
    }
}
