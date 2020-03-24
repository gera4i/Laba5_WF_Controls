using Laba5_WF_Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json;

namespace Laba5_WF_Controls
{
    enum Option
    {
        Studio,
        SeparatedWC,
        Balkon,
        Podval
    }
    enum Material
    {
        Brick,
        Panel,
        Wood
    }
    
    class Flat
    {
        public int size;
        public int numOfRooms;
        public List <Option> options = new List<Option>();
        public int year;
        public Material typeOfMaterial;
        public int level;
        public Adress adress;
        public List<Room> rooms = new List<Room>();
        
           
        }
      
    
    class Adress
    {
        public string country;
        public string town;
        public string district;
        public string street;
        public int numberOfBuilding;
        public int? subNumberOfBuilding;
        public int numberOfFlat;
    }
    enum Side
    {
        North,
        South,
        West,
        East
    }
    class Room
    {
        public double area;
        public int numberOfWindows;
        public Side sideOfWindows;
    }
class FlatModel
{
    private FlatModel()
    {

    }
    public static FlatModel shared = new FlatModel();
    public List<Flat> flats = new List<Flat>();
    public async void Save()
    {

            using (FileStream fs = new FileStream("kvartir04ki.json", FileMode.OpenOrCreate))
            {
                string json = JsonConvert.SerializeObject(flats, Newtonsoft.Json.Formatting.Indented);
                // преобразуем строку в байты
                byte[] array = System.Text.Encoding.Default.GetBytes(json);
                // запись массива байтов в файл
                fs.Write(array, 0, array.Length);
                Console.WriteLine("Data has been saved to file");
            }
        }
    public void Load()
    {
            if (File.Exists("kvartir04ki.json"))
            {
                using (FileStream fstream = File.OpenRead("kvartir04ki.json"))
                {
                    // преобразуем строку в байты
                    byte[] array = new byte[fstream.Length];
                    // считываем данные
                    fstream.Read(array, 0, array.Length);
                    // декодируем байты в строку
                    string json = System.Text.Encoding.Default.GetString(array);
                    flats = JsonConvert.DeserializeObject<List<Flat>>(json);
                }
            }
    }


        public int Price(Flat flat)
        {
            int a = 0;
            if (flat.numOfRooms == 1)
            a = 100000;
            if (flat.numOfRooms == 2)
                a= 200000;
            if (flat.numOfRooms == 3)
                a= 300000;
            if (flat.numOfRooms == 4)
                a= 400000;
            foreach (var item in flat.options)
            {
                switch (item)
                {
                    case Option.Balkon:
                        a += 5000;
                        break;
                    case Option.Podval:
                        a += 2000;
                        break;
                    case Option.SeparatedWC:
                        a += 3500;
                        break;
                    case Option.Studio:
                        a += 1500;
                        break;
                    default:
                        break;
                }
            }
            if(flat.year < 2000)
            {
                a -= 15000;
            }
            if (flat.year < 2010 && flat.year>2000)
            {
                a += 2000;
            }
            if (flat.year > 2010)
            {
                a += 15000;
            }
            return a;
        }
    }
   
}

