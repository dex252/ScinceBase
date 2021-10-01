using System;
using Web.Repositories.Connection;
using Web.ViewModels.Home;
using Dapper;
using Web.Models.Db;

namespace Web.Repositories
{
    public class SmartRepository : ISmartRepository
    {
        private IConnection Connection {get;}
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
    }
}
