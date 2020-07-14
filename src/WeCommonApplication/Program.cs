using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Linq;
using WeCommon;
namespace WeCommonApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            MongoClient dbClient = new MongoClient("mongodb://localhost:27017");
            var db=dbClient.GetDatabase("db-test", new MongoDatabaseSettings() { });
            db.DropCollection("dynamic");
            var col = db.GetCollection<PropertyType>("dynamic", new MongoCollectionSettings() { });

            dynamic nbre = new Property<int>();
            nbre.fr = 10;
            var serialized = JsonConvert.SerializeObject(nbre);
            Console.WriteLine(serialized);
            

            Property<int> nbre1 = JsonConvert.DeserializeObject<Property<int>>(serialized);
            Console.WriteLine(JsonConvert.SerializeObject(nbre1));

            dynamic propertyType = new Property<string>();
            propertyType.fr = "Hotel";
            propertyType.en = "Hostel";

            Console.WriteLine(propertyType.en);
            propertyType.it = null;
            //propertyType.gb = 11;
            serialized = JsonConvert.SerializeObject(propertyType);
            Console.WriteLine(serialized);

            Property<string> anotherPropType = JsonConvert.DeserializeAnonymousType<Property<string>>(serialized,new Property<string>());
            Console.WriteLine(JsonConvert.SerializeObject(anotherPropType));


            PropertyType pt = new PropertyType();
            pt.Id = Guid.NewGuid();
            pt.Description.fr = "Appartement";
            pt.Description.es = "apartamento";
            pt.Location.fr = "Chez mois";
            pt.Location.es = "en mi casa";
            pt.Location.it = "Nona";
            pt.Test = "Test Text";
            pt.Prices.fr = 100;
            pt.Prices.es = 85;
            pt.Taxes.fr = 1.2;
            pt.Taxes.es = 1.05;
            pt.GPS = new Location( 48.507998, 6.988586);

            serialized = JsonConvert.SerializeObject(pt);
            Console.WriteLine(serialized);
            col.InsertOne(pt);

            var res = col.Find(_=>true).ToList();
            Console.WriteLine("From DB");
            Console.WriteLine(JsonConvert.SerializeObject(res.First()));

            Console.WriteLine("COPY");
            PropertyType pt1 = JsonConvert.DeserializeObject<PropertyType>(serialized);
            Console.WriteLine(JsonConvert.SerializeObject(pt1));




            Date _date = new Date();
            Periode _periode = new Periode(_date, _date + 3);
            Console.WriteLine(_date.ToString());
            Console.WriteLine(_periode.GetDays());
            _periode.GetDates().ToList().ForEach(d => Console.WriteLine(d));
            Console.ReadLine();
        }
    }
}
