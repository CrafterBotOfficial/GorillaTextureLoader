using System.Collections.Generic;
using TextureLoader.MapTextureManagers;

namespace TextureLoader
{
    public static class TextureManager
    {
        public static readonly Dictionary<string, IMapTextureManager> MapManagers;

        static TextureManager()
        {
            MapManagers = new Dictionary<string, IMapTextureManager>()
            {
                { "forest", new ForestTextureManager() },
            };
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        }

        private static void SceneManager_sceneLoaded(UnityEngine.SceneManagement.Scene arg0, UnityEngine.SceneManagement.LoadSceneMode arg1)
        {
            throw new System.NotImplementedException();
        }
    }
}
