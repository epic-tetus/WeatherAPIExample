using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAPIExample
{
    public class AddressElement
    {
        public async static Task<AddressObject> GetAddress(double Latitude, double Longitude)
        {
            var Client = new HttpClient();
            var JsonResponse = await Client.GetAsync("https://apis.daum.net/local/geo/coord2addr?apikey=0e7e5f413decd06b57472aa92f0818a5&longitude="+Longitude+"&latitude="+Latitude+"&output=json");
            var Result = await JsonResponse.Content.ReadAsStringAsync();
            var Serializer = new DataContractJsonSerializer(typeof(AddressObject));

            var ms = new MemoryStream(Encoding.UTF8.GetBytes(Result));

            var data = (AddressObject)Serializer.ReadObject(ms);

            return data;

        }
    }

    [DataContract]
    public class AddressObject
    {
        [DataMember]
        public string type { get; set; }
        [DataMember]
        public string code { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string fullName { get; set; }
        [DataMember]
        public string regionId { get; set; }
        [DataMember]
        public string name0 { get; set; }
        [DataMember]
        public string code1 { get; set; }
        [DataMember]
        public string name1 { get; set; }
        [DataMember]
        public string code2 { get; set; }
        [DataMember]
        public string name2 { get; set; }
        [DataMember]
        public string code3 { get; set; }
        [DataMember]
        public string name3 { get; set; }
        [DataMember]
        public double x { get; set; }
        [DataMember]
        public double y { get; set; }
    }
}
