using System;
using System.Collections.Generic;
using Google.Cloud.Firestore;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace SpiritAstro.BusinessTier.Repositories
{
    public class Base
    {
        public string Id { get; set; }
    }

    public class FirestoreRepository
    {
        private readonly string _collectionName;
        private readonly FirestoreDb _fireStoreDb;

        public FirestoreRepository(string collectionName)
        {
            string filepath = "Resources/private_key.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", filepath);
            _fireStoreDb = FirestoreDb.Create("ndtradingsystem");
            _collectionName = collectionName;
        }

        public T Add<T>(T record) where T : Base
        {
            CollectionReference colRef = _fireStoreDb.Collection(_collectionName);
            DocumentReference doc = colRef.AddAsync(record).GetAwaiter().GetResult();
            record.Id = doc.Id;
            return record;
        }

        public bool Update<T>(T record) where T : Base
        {
            DocumentReference recordRef = _fireStoreDb.Collection(_collectionName).Document(record.Id);
            recordRef.SetAsync(record, SetOptions.MergeAll).GetAwaiter().GetResult();
            return true;
        }

        public bool Delete<T>(T record) where T : Base
        {
            DocumentReference recordRef = _fireStoreDb.Collection(_collectionName).Document(record.Id);
            recordRef.DeleteAsync().GetAwaiter().GetResult();
            return true;
        }

        public T Get<T>(T record) where T : Base
        {
            DocumentReference docRef = _fireStoreDb.Collection(_collectionName).Document(record.Id);
            DocumentSnapshot snapshot = docRef.GetSnapshotAsync().GetAwaiter().GetResult();
            if (snapshot.Exists)
            {
                T usr = snapshot.ConvertTo<T>();
                usr.Id = snapshot.Id;
                return usr;
            }

            return null;
        }

        public List<T> GetAll<T>() where T : Base
        {
            Query query = _fireStoreDb.Collection(_collectionName);
            QuerySnapshot querySnapshot = query.GetSnapshotAsync().GetAwaiter().GetResult();
            List<T> list = new List<T>();
            foreach (DocumentSnapshot documentSnapshot in querySnapshot.Documents)
            {
                if (documentSnapshot.Exists)
                {
                    Dictionary<string, object> city = documentSnapshot.ToDictionary();
                    string json = JsonConvert.SerializeObject(city);
                    T newItem = JsonConvert.DeserializeObject<T>(json);
                    newItem.Id = documentSnapshot.Id;
                    list.Add(newItem);
                }
            }

            return list;
        }

        public List<T> QueryRecords<T>(Query query) where T : Base
        {
            QuerySnapshot querySnapshot = query.GetSnapshotAsync().GetAwaiter().GetResult();
            List<T> list = new List<T>();
            foreach (DocumentSnapshot documentSnapshot in querySnapshot.Documents)
            {
                if (documentSnapshot.Exists)
                {
                    Dictionary<string, object> city = documentSnapshot.ToDictionary();
                    string json = JsonConvert.SerializeObject(city);
                    T newItem = JsonConvert.DeserializeObject<T>(json);
                    newItem.Id = documentSnapshot.Id;
                    list.Add(newItem);
                }
            }

            return list;
        }
    }
}