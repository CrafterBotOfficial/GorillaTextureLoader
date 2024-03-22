namespace TextureLoader.MapTextureManagers
{
    public interface IMapTextureManager
    {
        public string SceneName { get; }

        public bool LoadTexture(TextureFileLoader.TexturePack info);
        public void UnLoadCurrent();
    }
}
