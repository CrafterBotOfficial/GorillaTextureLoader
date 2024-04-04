using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TextureLoader.MapTextureManagers
{
    public class ForestTextureManager : IMapTextureManager
    {
        public string SceneName => "GorillaTag";


        void IMapTextureManager.LoadTexture(TextureFileLoader.TexturePack info)
        {
            throw new NotImplementedException();
        }

        void IMapTextureManager.SaveGameMaterials(TextureFileLoader.TexturePack info)
        {
            throw new NotImplementedException();
        }

        void IMapTextureManager.UnLoadCurrent()
        {
            throw new NotImplementedException();
        }

        public Transform GetTerrainRoot()
        {
            throw new NotImplementedException();
        }
    }
}
