using Web.Repositories.Connection;
using Web.ViewModels.Home;
using Dapper;
using System.Collections.Generic;
using Web.Models.Db;
using Web.Models.Review;

namespace Web.Repositories
{
    public class SmartRepository : ISmartRepository
    {
        private IConnection Connection { get; }
        public SmartRepository(IConnection connection)
        {
            Connection = connection;
        }
        public ReviewModel GetReview()
        {
            var reviewViewModel = new ReviewModel();
            using (var connection = Connection.OpenConnection())
            {
                var rowCount = connection.RecordCount<RootNode>();

                reviewViewModel.CountOfClasses = rowCount;
            }

            return reviewViewModel;
        }

        public int? InsertClass(RootNode item)
        {
            using (var connection = Connection.OpenConnection())
            using (var transaction = connection.BeginTransaction())
            {
                var result = connection.Insert(item, transaction);
                transaction.Commit();
                return result;
            }

        }

        public IEnumerable<RootNode> GetClasses()
        {
            using (var connection = Connection.OpenConnection())
            { 
                var result = connection.GetList<RootNode>();
                return result;
            }
        }

        public decimal? InsertNewEnums(EnumsValue enums)
        {
            using (var connection = Connection.OpenConnection())
            using (var transaction = connection.BeginTransaction())
            {
                var result = connection.Insert(enums, transaction);
                transaction.Commit();
                return result;
            }
        }

        public IEnumerable<EnumsValue> GetAllEnums()
        {
            using (var connection = Connection.OpenConnection())
            {
                var result = connection.GetList<EnumsValue>();
                return result;
            }
        }

        public void SaveClasses(List<RootNode> nodes)
        {
            using (var connection = Connection.OpenConnection())
            using (var transaction = connection.BeginTransaction())
            {
                //TODO: переписать на BulkInsert
                foreach (var node in nodes)
                {
                    connection.Insert(node, transaction);
                }

                transaction.Commit();
            }
        }
    }
}
