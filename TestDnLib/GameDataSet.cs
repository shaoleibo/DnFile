using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace TestDnLib
{
    class GameDataSet
    {

        private Dictionary<UInt64, String> _mapResMotion = new Dictionary<UInt64,string>();
        private Dictionary<UInt64, String> _mapResTexture = new Dictionary<UInt64, string>();
        private Dictionary<UInt64, String> _mapResMesh = new Dictionary<UInt64, String>();

        private void Init()
        {
            LoadRes(Resource.INI_PATH + "/3dmotion.ini", _mapResMotion);
            LoadRes(Resource.INI_PATH + "/3dobj.ini", _mapResMesh);
            LoadRes(Resource.INI_PATH + "/3dtexture.ini", _mapResTexture);
        }

        private void LoadRes( string path, Dictionary<UInt64, String> mapRes )
        {
            try
            {
                mapRes.Clear();
                FileStream fs = File.OpenRead(path);
                StreamReader sr = new StreamReader(fs);
                Console.WriteLine(UInt64.MaxValue);
                while (true)
                {

                    string line = sr.ReadLine();
                    if (line == null)
                    {
                        break;
                    }
                    //注释或是空
                    if (line.Trim() == "" || line.Trim().StartsWith("//") )
                    {
                        continue;
                    }
                    int seperate = line.IndexOf('=');
                    if (seperate < 0)
                    {
                        continue;
                    }
                    UInt64 id = Convert.ToUInt64(line.Substring( 0, seperate ));
                    string resPath = line.Substring(seperate + 1);
                    mapRes[id] = resPath;
                    Console.WriteLine(id+":"+resPath);
                }
                sr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
