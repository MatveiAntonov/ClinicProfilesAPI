using Dapper;
using Profiles.Domain.Entities;
using Profiles.Domain.Entities.ForeignEntities;
using Profiles.Domain.Interfaces.Repositories;
using Profiles.Persistence.Contexts;
using System.Data;
using System.Numerics;

namespace Profiles.Persistence.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly ProfilesDbContext _profileDbContext;
        public DoctorRepository(ProfilesDbContext profileDbContext)
        {
            _profileDbContext = profileDbContext;
        }
        public async Task<IEnumerable<Doctor?>> GetAllDoctors(CancellationToken cancellationToken)
        {
            var query = "SELECT  * " +
                "FROM [Doctors] " +
                "JOIN Specializations ON Doctors.SpecializationId = Specializations.Id " +
                "JOIN Offices ON Doctors.OfficeId = Offices.Id " +
                "JOIN Accounts ON Doctors.AccountId = Accounts.Id " +
                "JOIN Photos ON Accounts.PhotoId = Photos.Id";


            using (var connection = _profileDbContext.CreateConnection())
            {
                var doctors = await connection.QueryAsync<Doctor, Specialization, Office, Account, Photo, Doctor>(query,
                    (doctor, specialization, office, account, photo) =>
                    {
                        doctor.Specialization = specialization;
                        doctor.Office = office;
                        account.Photo = photo;
                        doctor.Account = account;
                        return doctor;
                    });
                return doctors.ToList();
            }
        }
        public async Task<Doctor?> GetDoctor(int id, CancellationToken cancellationToken)
        {
            var query = "SELECT * " +
                "FROM [Doctors] " +
                "JOIN Specializations ON Doctors.SpecializationId = Specializations.Id " +
                "JOIN Offices ON Doctors.OfficeId = Offices.Id " +
                "JOIN Accounts ON Doctors.AccountId = Accounts.Id " +
                "JOIN Photos ON Accounts.PhotoId = Photos.Id " +
                "WHERE Doctors.Id = @Id";

            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);

            using (var connection = _profileDbContext.CreateConnection())
            {
                var doctors = await connection.QueryAsync<Doctor, Specialization, Office, Account, Photo, Doctor>(query,
                    (doctor, specialization, office, account, photo) =>
                    {
                        doctor.Specialization = specialization;
                        doctor.Office = office;
                        account.Photo = photo;
                        doctor.Account = account;
                        return doctor;
                    }, param: parameters);
                return doctors.ToList().FirstOrDefault();
            }
        }

        public async Task<Doctor?> CreateDoctor(Doctor doctor, CancellationToken cancellationToken)
        {
            var query = "INSERT INTO [Doctors] " +
                "(FirstName, LastName, MiddleName, DateOfBirth, " +
                "CareerStartYear, Status, OfficeId, SpecializationId, AccountId) " +
                "VALUES (@FirstName, @LastName, @MiddleName, @DateOfBirth, " +
                "@CareerStartYear, @Status, @OfficeId, @SpecializationId, @AccountId)";

            var parameters = new DynamicParameters();
            parameters.Add("FirstName", doctor.FirstName, DbType.String);
            parameters.Add("LastName", doctor.LastName, DbType.String);
            parameters.Add("MiddleName", doctor.MiddleName, DbType.String);
            parameters.Add("DateOfBirth", doctor.DateOfBirth, DbType.DateTime);
            parameters.Add("CareerStartYear", doctor.CareerStartYear, DbType.DateTime);
            parameters.Add("Status", doctor.Status, DbType.String);
            parameters.Add("OfficeId", doctor.OfficeId, DbType.Int32);
            parameters.Add("SpecializationId", doctor.SpecializationId, DbType.Int32);
            parameters.Add("AccountId", doctor.AccountId, DbType.Int32);

            using (var connection = _profileDbContext.CreateConnection())
            {
                var r = await connection.ExecuteAsync(query, parameters);
                return r == 0 ? null : doctor;
            }
        }
        public async Task<Doctor?> UpdateDoctor(Doctor doctor, CancellationToken cancellationToken)
        {
            var query = "UPDATE [Doctors] " +
                "SET FirstName = @FirstName, LastName = @LastName, MiddleName = @MiddleName, " +
                "DateOfBirth = @DateOfBirth, CareerStartYear = @CareerStartYear, Status = @Status, " +
                "OfficeId = @OfficeId, SpecializationId = @SpecializationId, AccountId = @AccountId " +
                "WHERE Id = @Id";

            var parameters = new DynamicParameters();
            parameters.Add("Id", doctor.Id, DbType.Int32);
            parameters.Add("FirstName", doctor.FirstName, DbType.String);
            parameters.Add("LastName", doctor.LastName, DbType.String);
            parameters.Add("MiddleName", doctor.MiddleName, DbType.String);
            parameters.Add("DateOfBirth", doctor.DateOfBirth, DbType.DateTime);
            parameters.Add("CareerStartYear", doctor.CareerStartYear, DbType.DateTime);
            parameters.Add("Status", doctor.Status, DbType.String);
            parameters.Add("OfficeId", doctor.OfficeId, DbType.Int32);
            parameters.Add("SpecializationId", doctor.SpecializationId, DbType.Int32);
            parameters.Add("AccountId", doctor.AccountId, DbType.Int32);

            using (var connection = _profileDbContext.CreateConnection())
            {
                var r = await connection.ExecuteAsync(query, parameters);
                return r == 0 ? null : doctor;
            }
        }

        public async Task<int> DeleteDoctor(int id, CancellationToken cancellationToken)
        {
            string query = "DELETE FROM [Doctors] WHERE Id = @Id";

            using (var connection = _profileDbContext.CreateConnection())
            {
                var r = await connection.ExecuteAsync(query, new { Id = id });
                return r;
            }
        }
    }
}
