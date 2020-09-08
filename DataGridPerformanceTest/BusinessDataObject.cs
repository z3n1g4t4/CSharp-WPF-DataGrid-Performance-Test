using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;

namespace DataGridPerformanceTest
{
    [DataContract]
    public class BusinessDataObject
    {
        [DataMember]
        public List<UsefulPoco> UsefulDataList { get; set; }
        private static readonly Random _random = new Random();
        [DataContract]
        public class UsefulPoco
        {
            [DataMember]
            public string Name { get; set; }
            [DataMember]
            public string Code1 { get; set; }
            [DataMember]
            public string Code2 { get; set; }
            [DataMember]
            public double SmallNumber { get; set; }
            [DataMember]
            public double BigNumber { get; set; }
            [DataMember]
            public string Classification1 { get; set; }
            [DataMember]
            public string Classification2 { get; set; }
            [DataMember]
            public DateTime? LastUpdate { get; set; }
        }
        public BusinessDataObject()
        {
            UsefulDataList = LoadBusinessData();
        }

        public static List<UsefulPoco> LoadBusinessData()
        {
            if (!File.Exists(Config.BusinessDataFullFileName))
                GenerateBusinessData();

            DataContractSerializer serializer = new DataContractSerializer(typeof(List<UsefulPoco>));
            using (XmlReader reader = XmlReader.Create(Config.BusinessDataFullFileName))
            {
                return serializer.ReadObject(reader) as List<UsefulPoco>;
            }
        }

        private static void SaveBusinessData(List<UsefulPoco> ListToSave)
        {
            if (ListToSave == null)
                return;

            DataContractSerializer serializer = new DataContractSerializer(typeof(List<UsefulPoco>));
            XmlWriterSettings settings = new XmlWriterSettings() { Indent = true };
            using (XmlWriter writer = XmlWriter.Create(Config.BusinessDataFullFileName, settings))
            {
                serializer.WriteObject(writer, ListToSave);
            }
        }

        public static void GenerateBusinessData()
        {
            List<UsefulPoco> tempList = new List<UsefulPoco>();
            for (int i = 0; i < 551; i++)
            {
                UsefulPoco tempPoco = new UsefulPoco();
                tempPoco.Name = RandomString(_random.Next(10, 26));
                string guid = Guid.NewGuid().ToString().ToUpper();
                tempPoco.Code1 = guid.Substring(0, 5);
                tempPoco.Code2 = guid.Substring(guid.Length - 9, 9);
                tempPoco.SmallNumber = _random.Next(1, 250);
                tempPoco.BigNumber = RandomDoubleBetween(50_000_000, 6_000_000_000);
                tempPoco.Classification1 = Classification1[_random.Next(0, Classification1.Count - 1)];
                tempPoco.Classification2 = Classification2[_random.Next(0, Classification2.Count - 1)];
                tempPoco.LastUpdate = DateTime.Now.AddDays(_random.Next(-500, 500));
                tempList.Add(tempPoco);
            }

            SaveBusinessData(tempList);
        }


        private static string RandomString(int length)
        {
            const string chars = "AAABCDEEEFGHIIIJKLMNOOOPQRSTUUUUVWXYZ";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[_random.Next(s.Length)]).ToArray());
        }

        private static double RandomDoubleBetween(double min, double max)
        {
            return _random.NextDouble() * (max - min) + min;

        }

        private readonly static Dictionary<int, string> Classification1 = new Dictionary<int, string>()
        {
            { 0, "C1 Smart Key" },
            { 1, "C1 Slower" },
            { 2, "C1 Increasing" },
            { 3, "C1 Static member" },
            { 4, "C1 Random" },
            { 5, "C1 Performance" },
            { 6, "C1 Regional" },
            { 7, "C1 Not smart" },
            { 8, "C1 Poco" },
        };

        private readonly static Dictionary<int, string> Classification2 = new Dictionary<int, string>()
        {
            { 0, "C2 Smart Key" },
            { 1, "C2 Slower" },
            { 2, "C2 Increasing" },
            { 3, "C2 Static member" },
            { 4, "C2 Random" },
            { 5, "C2 Performance" },
            { 6, "C2 Regional" },
            { 7, "C2 Not smart" },
            { 8, "C2 Poco" },
        };
    }
}
