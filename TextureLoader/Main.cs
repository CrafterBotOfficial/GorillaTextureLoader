using BepInEx;
using BepInEx.Logging;

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
    }
}
