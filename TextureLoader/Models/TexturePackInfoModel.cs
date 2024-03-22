/*

The SupportedMaps key will be the scene name, the values will be a list of file paths (in the archive)
that will correspond with either a start of the name of a object in the scene or if its just a number it will be that index
of the main maps materials textures.
 
*/


using System.Collections.Generic;

namespace TextureLoader.Models
{

    public class TexturePackInfoModel
    {
        public string Name;
        public string Author;
        public string Description;

        public Dictionary<string, string[]> SupportedMaps; // map name, map textures files
    }
}