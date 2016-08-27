using System.Diagnostics;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;

namespace GLEED2D
{
    class TextureLoader
    {

        private static TextureLoader instance;
        public static TextureLoader Instance
        {
            get
            {
                if (instance == null) instance = new TextureLoader();
                return instance;
            }
        }

        public Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();

        public Texture2D FromFile(GraphicsDevice gd, string filename)
        {
            filename = filename.Replace("\\\\", "\\");
            if (!textures.ContainsKey(filename))
            {
                FileStream stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                textures[filename] = Texture2D.FromStream(gd, stream);
                stream.Close();
            }
            return textures[filename];
        }

        public void reload(string filename)
        {
            filename = filename.Replace("\\\\", "\\");
            if (textures.ContainsKey(filename))
            {
                FileStream stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                Texture2D newtext = Texture2D.FromStream(textures[filename].GraphicsDevice, stream);
                Color[] data = new Color[newtext.Width*newtext.Height];
                newtext.GetData<Color>(data);
                textures[filename].SetData<Color>(data);
                stream.Close();
            }
        }

        public void Clear()
        {
            textures.Clear();
        }

    }
}
