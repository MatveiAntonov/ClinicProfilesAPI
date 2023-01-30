using Dapper;
using Profiles.Domain.Entities;
using Profiles.Domain.Entities.ForeignEntities;
using Profiles.Domain.Interfaces.Repositories;
using Profiles.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Profiles.Persistence.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly ProfilesDbContext _profileDbContext;

        public PatientRepository(ProfilesDbContext profilesDbContext)
        {
            _profileDbContext = profilesDbContext;
        }
        public async Task<IEnumerable<Patient?>> GetAllPatients(CancellationToken cancellationToken)
        {
            var query = "SELECT * " +
                "FROM [Patients] " +
                "JOIN Accounts ON Patients.AccountId = Accounts.Id " +
                "JOIN Photos ON Accounts.PhotoId = Photos.Id";

            using (var connection = _profileDbContext.CreateConnection())
            {
                var patients = await connection.QueryAsync<Patient, Account, Photo, Patient>(query,
                    (patient, account, photo) =>
                    {
                        account.Photo = photo;
                        patient.Account = account;
                        return patient;
                    });
                return patients.ToList();
            }
        }

        public async Task<Patient?> GetPatient(int id, CancellationToken cancellationToken)
        {
            var query = "SELECT * " +
                "FROM [Patients] " +
                "JOIN Accounts ON Patients.AccountId = Accounts.Id " +
                "JOIN Photos ON Accounts.PhotoId = Photos.Id " +
                "WHERE Patients.Id = @Id";

            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);

            using (var connection = _profileDbContext.CreateConnection())
            {
                var patients = await connection.QueryAsync<Patient, Account, Photo, Patient>(query,
                    (patient, account, photo) =>
                    {
                        account.Photo = photo;
                        patient.Account = account;
                        return patient;
                    }, param: parameters);
                return patients.ToList().FirstOrDefault();
            }
        }

        public async Task<Patient?> CreatePatient(Patient patient, CancellationToken cancellationToken)
        {
            var query = "INSERT into [Patients] " +
                "(FirstName, LastName, MiddleName, DateOfBirth, AccountId) " +
                "values (@FirstName, @LastName, @MiddleName, @DateOfBirth, @AccountId)";

            var parameters = new DynamicParameters();
            parameters.Add("FirstName", patient.FirstName, DbType.String);
            parameters.Add("LastName", patient.LastName, DbType.String);
            parameters.Add("MiddleName", patient.MiddleName, DbType.String);
            parameters.Add("DateOfBirth", patient.DateOfBirth, DbType.DateTime);
            parameters.Add("AccountId", patient.AccountId, DbType.Int32);

            using (var connection = _profileDbContext.CreateConnection())
            {
                var r = await connection.ExecuteAsync(query, parameters);
                return r == 0 ? null : patient;
            }
        }

        public async Task<Patient?> UpdatePatient(Patient patient, CancellationToken cancellationToken)
        {
            var query = "UPDATE [Patients] " +
                "SET FirstName = @FirstName, LastName = @LastName, MiddleName = @MiddleName, " +
                "DateOfBirth = @DateOfBirth, AccountId = @AccountId " +
                "WHERE Id = @Id";

            var parameters = new DynamicParameters();
            parameters.Add("Id", patient.Id, DbType.Int32);
            parameters.Add("FirstName", patient.FirstName, DbType.String);
            parameters.Add("LastName", patient.LastName, DbType.String);
            parameters.Add("MiddleName", patient.MiddleName, DbType.String);
            parameters.Add("DateOfBirth", patient.DateOfBirth, DbType.DateTime);
            parameters.Add("AccountId", patient.AccountId, DbType.Int32);

            using (var connection = _profileDbContext.CreateConnection())
            {
                var r = await connection.ExecuteAsync(query, parameters);
                return r == 0 ? null : patient;
            }
        }

        public async Task<int> DeletePatient(int id, CancellationToken cancellationToken)
        {
            string query = "DELETE FROM [Patients] WHERE Id = @Id";

            using (var connection = _profileDbContext.CreateConnection())
            {
                var r = await connection.ExecuteAsync(query, new { Id = id });
                return r;
            }
        }
    }
}
