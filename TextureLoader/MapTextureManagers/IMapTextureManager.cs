/*
Todo:
    add a dictionary for storing objects under the terrain root so it doesn't rely to heavily on constant gameobject.find
*/

using UnityEngine;

namespace TextureLoader.MapTextureManagers
{
    public interface IMapTextureManager
    {
        public string SceneName { get; }

        public void SaveGameMaterials(TextureFileLoader.TexturePack info);
        public void LoadTexture(TextureFileLoader.TexturePack info);
        public void UnLoadCurrent();

        public Transform GetTerrainRoot();
    }
}
