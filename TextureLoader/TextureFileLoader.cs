// TODO: Check that zipArchive.GetEntry() returns null if the entry doesnt exists

using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using TextureLoader.Models;
using UnityEngine;

namespace TextureLoader
{
    public static class TextureFileLoader
    {
        public static Dictionary<string, TexturePack> TexturePacks;

        public static void LoadAll()
        {
            TexturePacks = new Dictionary<string, TexturePack>();
            foreach (string file in Directory.GetFiles(Path.Combine("./", "TexturePacks")))
            {
                if (LoadFromFile(file) is not TexturePack info)
                {
                    Main.LogSource.LogWarning($"Failed to load " + file);
                    continue;
                }
                TexturePacks.Add(info.Id, info);
            }
        }

        private static TexturePack? LoadFromFile(string path)
        {
            Main.LogSource.LogInfo("Loading file " + Path.GetFileName(path));

            using Stream reader = File.OpenRead(path);
            using ZipArchive zipArchive = new ZipArchive(reader);

            if (zipArchive.GetEntry("packinfo.json") is not ZipArchiveEntry packInfoEntry)
            {
                Main.LogSource.LogError($"{path} does not contain a packinfo file.");
                return null;
            }

            if (ParsePackInfo(packInfoEntry) is not TexturePackInfoModel packInfo)
            {
                Main.LogSource.LogError($"Failed to deerialize {path} packinfo.json file");
                return null;
            }

            Dictionary<string, List<Texture>> mapTextures = new Dictionary<string, List<Texture>>();
            for (int mapFolderIndex = 0; mapFolderIndex < packInfo.SupportedMaps.Count; mapFolderIndex++)
            {
                var dictEntry = packInfo.SupportedMaps.ElementAt(mapFolderIndex);
                string mapName = dictEntry.Key;
                string[] mapTextureFiles = dictEntry.Value;

                foreach (var mapTextureFile in mapTextureFiles)
                {
                    if (!zipArchive.Entries.Any(entry => entry.FullName == mapTextureFile))
                    {
                        Main.LogSource.LogWarning("Failed to find texture file " + mapTextureFile + " in " + path);
                        continue;
                    }
                    using Stream mapTextureStream = zipArchive.GetEntry(mapTextureFile).Open();
                    byte[] mapBuffer = new byte[mapTextureStream.Length];
                    mapTextureStream.Read(mapBuffer, 0x0, mapBuffer.Length);

                    Texture2D mapTexture = new Texture2D(0, 0);
                    mapTexture.LoadImage(mapBuffer);
                    if (mapTextures.TryGetValue(mapName, out List<Texture> textures)) textures.Add(mapTexture);
                    else mapTextures.Add(mapName, new() { mapTexture });
                    Main.LogSource.LogInfo($"Loaded {mapTextureFile} for {mapName}");
                }
            }

            return new TexturePack()
            {
                PackInfo = packInfo,
                Id = packInfo.Author + packInfo.Name,
                MapTextures = mapTextures
            };
        }

        private static TexturePackInfoModel ParsePackInfo(ZipArchiveEntry entry)
        {
            using Stream stream = entry.Open();
            using StreamReader reader = new StreamReader(stream);

            string jsonText = reader.ReadToEnd();
            if (Newtonsoft.Json.JsonConvert.DeserializeObject<TexturePackInfoModel>(jsonText) is TexturePackInfoModel model)
                return model;
            return null;
        }

        // TODO: See how long each instance will exist for | struct vs class
        public class TexturePack
        {
            public TexturePackInfoModel PackInfo;
            public string Id;
            public Dictionary<string, List<Texture>> MapTextures;
        }
    }
}