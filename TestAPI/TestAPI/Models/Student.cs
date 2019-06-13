using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//Binary encoding of JSON-like documents that MongoDB
using MongoDB.Bson;

//Bson serialization/deserialization
using MongoDB.Bson.Serialization.Attributes;

namespace TestAPI.Models
{
    public class Student
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }

        [BsonElement("name")]
        public Name name { get; set; }

        [BsonElement("address")]
        public Address address { get; set; }


        [BsonElement("email")]
        public string email { get; set; }

    }

    public class Name
    {
        [BsonElement("first")]
        public string first { get; set; }
        [BsonElement("last")]
        public string last { get; set; }
    }

    public class Address
    {
        [BsonElement("street")]
        public string street { get; set; }
        [BsonElement("city")]
        public string city { get; set; }
        [BsonElement("state")]
        public string state { get; set; }
        [BsonElement("zip")]
        public string zip { get; set; }
    }
}

