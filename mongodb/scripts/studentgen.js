const MongoClient = require("mongodb").MongoClient;
const assert = require("assert").strict;
const faker = require("faker/locale/en_US");

const url = "mongodb://admin:abc123@mongo:27017";
const dbName = "realtheoryuniversity";

// connect to the mongo server container
MongoClient.connect(url, { useNewUrlParser: true }, (err, client) => {
  assert.strictEqual(err, null);
  console.log("Connected successfully to server");

  const db = client.db(dbName);
  const collection = db.collection("students");

  // populate 'students' collection if it is empty
  collection.countDocuments((err, result) => {
    assert.strictEqual(err, null);

    if (result === 0) {
      // generate 5000 random students
      let students = [];
      for (let i = 0; i < 5000; i++) {
        const gender = Math.round(Math.random());
        const firstName = faker.name.firstName(gender);
        const lastName = faker.name.lastName(gender);
        let student = {
          name: {
            first: firstName,
            last: lastName
          },
          email: faker.internet.email(firstName, lastName, "realtheory.edu"),
          address: {
            street: faker.address.streetAddress(true),
            city: faker.address.city(),
            state: faker.address.state(true),
            zip: faker.address.zipCode()
          }
        };
        students.push(student);
      }

      // insert the records into the 'students' collection
      collection.insertMany(students, (err, result) => {
        assert.strictEqual(err, null);
        assert.strictEqual(result.result.n, 5000);
        assert.strictEqual(result.ops.length, 5000);
        console.log("Generated 5000 student records successfully");
        client.close();
      });
    }
  });
});
