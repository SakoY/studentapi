using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using TestAPI.Models;

namespace TestAPI.Services
{
    public class StudentService
    {
        private readonly IMongoCollection<Student> _Students;

        public StudentService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("realtheoryuniversity"));
            var database = client.GetDatabase("realtheoryuniversity");
            _Students = database.GetCollection<Student>("students");
        }

        public async Task<List<Student>> Get(int index, int count, int sort_order)
        {

       
            if (sort_order == 1)
            {
                return await _Students.Find(Student => true).Skip(((index - 1) * count)).Limit(count).Sort(Builders<Student>.Sort.Descending("_id")).ToListAsync();
            }
            else if (sort_order == 0)
            {
                return await _Students.Find(Student => true).Skip(((index - 1) * count)).Limit(count).Sort(Builders<Student>.Sort.Ascending("_id")).ToListAsync();
            }
            else
                return await _Students.Find(Student => true).Skip(((index - 1) * count)).Limit(count).ToListAsync();

        }

        public double Size(int count)
        {
            return  System.Math.Ceiling((double)_Students.Find(Student => true).CountDocuments());
        }

        public Student Get(string _id)
        {
            return _Students.Find<Student>(Student => Student._id == _id).FirstOrDefault();
        }

        public Student Create(Student Student)
        {
            _Students.InsertOne(Student);
            return Student;
        }
          
        public void Update(string _id, Student StudentIn)
        {
            _Students.ReplaceOne(Student => Student._id == _id, StudentIn);
        }

        public void Remove(Student StudentIn)
        {
            _Students.DeleteOne(Student => Student._id== StudentIn._id);
        }

        public void Remove(string _id)
        {
            _Students.DeleteOne(Student => Student._id == _id);
        }

    }
}
