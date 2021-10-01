using Web.Repositories.Connection;
using Web.ViewModels.Home;
using Dapper;
using Web.Models.Db;
using System.Collections.Generic;

namespace Web.Repositories
{
    public class SmartRepository : ISmartRepository
    {
        private IConnection Connection { get; }
        public SmartRepository(IConnection connection)
        {
            Connection = connection;
        }
        public ReviewViewModel GetReview()
        {
            var reviewViewModel = new ReviewViewModel();
            using (var connection = Connection.OpenConnection())
            {
                var rowCount = connection.RecordCount<Classes>();

                reviewViewModel.CountOfClasses = rowCount;
            }

            return reviewViewModel;
        }

        public int? InsertClass(Classes item)
        {
            using (var connection = Connection.OpenConnection())
            using (var transaction = connection.BeginTransaction())
            {
                var result = connection.Insert(item, transaction);
                transaction.Commit();
                return result;
            }

        }

        public IEnumerable<Classes> GetClasses()
        {
            using (var connection = Connection.OpenConnection())
            { 
                var result = connection.GetList<Classes>();
                return result;
            }
        }
    }
}
