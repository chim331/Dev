using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevelopmentProject.DAL;
using Raven.Client;
using Raven.Client.Document;

namespace DevelopmentProject
{
    class Program
    {
        private static IDocumentStore _documentStore;

        static void Main(string[] args)
        {
            _documentStore = new DocumentStore { Url = "http://localhost:8080" }.Initialize();

            IRepository<SampleEntity>  _repository = new Repository<SampleEntity>(_documentStore);

            for (int i = 0; i < 10; i++)
            {
                _repository.Add(new SampleEntity(){Id = Guid.NewGuid(), Date = DateTime.Now.AddDays(i), Name = string.Format("Nome {0}", i), Version = i});

            }

            _repository.SaveChanges();

            Console.WriteLine(string.Format("Inseriti {0} records!", _repository.All<SampleEntity>().Count()));

            var a = _repository.Find(x => x.Version == 3);

            Console.ReadKey();
        }
    }
}
