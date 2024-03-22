using System.Collections.Generic;
using TextureLoader.MapTextureManagers;

namespace TextureLoader
{
    public static class TextureManager
    {
        public static Dictionary<string, IMapTextureManager> MapManagers = new Dictionary<string, IMapTextureManager>()
        {
            { "forest", new ForestTextureManager() }
        };
    }
}
