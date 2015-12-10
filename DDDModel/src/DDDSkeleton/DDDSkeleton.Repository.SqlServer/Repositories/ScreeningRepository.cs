using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using DDDSkeleton.Domain;
using Dapper;

namespace DDDSkeleton.Repository.SqlServer.Repositories
{
    public class ScreeningRepository:IScreeningRepository
    {
        private string connectionString = "?";
        private const string Fields = "Id,Candidate,Recruiter,Date,Location,Remark,GlobalEvaluation";

        public Screening FindBy(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
               var screeningDb = connection.QueryFirst(
                    "select Id,Candidate,Recruiter,Date,Location,Remark,GlobalEvaluation from Screening where Id= @Id",
                    id);

                if (screeningDb == null)
                {
                    return null;
                }
                 
                return ScreeningBuilder.CreateScreening((string) screeningDb.Candidate)
                    .ByRecruiter((string) screeningDb.Recruiter)
                    .OnLocation((string) screeningDb.Location)
                    .Build();
            }
        }

        public void Delete(Screening aggregate)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Execute("delete from Screening where Id = @Id", aggregate.Id);
            }
        }

        public void Insert(Screening aggregate)
        {
            using (var connection = new SqlConnection(connectionString))
            {
               var NewId = connection.Query<int>("insert into Screening (Candidate,Recruiter,Date,Location,Remark,GlobalEvaluation) values (@Candidate,@Recruiter,@Date,@Location,@Remark,@GlobalEvaluation);SELECT @ID = SCOPE_IDENTITY()", 
                    aggregate);
            }
        }

        public void Update(Screening aggregate)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Execute("update Screening set Candidate = @Candidate,Recruiter=@Recruiter,Date=@Date,Location=@Location,Remark=@Remark,GlobalEvaluation=@GlobalEvaluation where Id = @Id",
                    aggregate);
            }
        }

        public IEnumerable<Screening> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}
