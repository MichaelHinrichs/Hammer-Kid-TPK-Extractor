//Written for Hammer Kid. https://store.steampowered.com/app/1839610/
using System.IO;

namespace Hammer_Kid_TPK_Extractor
{
    class Program
    {
        static BinaryReader br;
        static void Main(string[] args)
        {
            br = new(File.OpenRead(args[0]));

            if (new string(br.ReadChars(4)) != "TiPK")
                throw new System.Exception("Wrong file. Input the data.tpk file from Hammer Kid.");

            int fileCount = br.ReadInt32();
            System.Collections.Generic.List<FileData> data = new();

            for (int i = 0; i < fileCount; i++)
                data.Add(new());

            foreach (FileData file in data)
            {
                Directory.CreateDirectory(Path.GetDirectoryName(args[0]) + "//" + Path.GetFileNameWithoutExtension(args[0]) + "//" + Path.GetDirectoryName(file.name));
                using FileStream FS = File.Create(Path.GetDirectoryName(args[0]) + "//" + Path.GetFileNameWithoutExtension(args[0]) + "//" + file.name);
                BinaryWriter bw = new(FS);
                bw.Write(br.ReadBytes(file.size));
                bw.Close();
            }
        }

        class FileData
        {
            public string name = new(br.ReadChars(br.ReadInt32()));
            public int size = br.ReadInt32();
        }
    }
}
