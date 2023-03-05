using Dapper;
using Events;
using MassTransit;
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
        private readonly IPublishEndpoint _publishEndpoint;

        public PatientRepository(ProfilesDbContext profilesDbContext, IPublishEndpoint publishEndpoint)
        {
            _profileDbContext = profilesDbContext;
            _publishEndpoint = publishEndpoint;
        }
        public async Task<IEnumerable<Patient?>> GetAllPatients(CancellationToken cancellationToken)
        {
            var query = "SELECT * " +
                "FROM [Patients] " +
                "JOIN Accounts ON Patients.AccountId = Accounts.Id";

            using (var connection = _profileDbContext.CreateConnection())
            {
                var patients = await connection.QueryAsync<Patient, Account, Patient>(query,
                    (patient, account) =>
                    {
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
                "WHERE Patients.Id = @Id";

            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);

            using (var connection = _profileDbContext.CreateConnection())
            {
                var patients = await connection.QueryAsync<Patient, Account, Patient>(query,
                    (patient, account) =>
                    {
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
                "OUTPUT INSERTED.Id " +
                "values (@FirstName, @LastName, @MiddleName, @DateOfBirth, @AccountId)";

            var parameters = new DynamicParameters();
            parameters.Add("FirstName", patient.FirstName, DbType.String);
            parameters.Add("LastName", patient.LastName, DbType.String);
            parameters.Add("MiddleName", patient.MiddleName, DbType.String);
            parameters.Add("DateOfBirth", patient.DateOfBirth, DbType.DateTime);
            parameters.Add("AccountId", patient.AccountId, DbType.Int32);

            using (var connection = _profileDbContext.CreateConnection())
            {
                var r = connection.ExecuteScalar<int>(query, parameters);

                var insertedPatient = await GetPatient(r, default(CancellationToken));

                await _publishEndpoint.Publish<PatientCreated>(new
                {
                    Id = r,
                    FirstName = insertedPatient.FirstName,
                    LastName = insertedPatient.LastName,
                    MiddleName = insertedPatient.MiddleName,
                    PhotoUrl = insertedPatient.Account.PhotoUrl,
                    DateOfBirth = insertedPatient.DateOfBirth
                });

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

                if (r != 0)
                {
                    var updatedPatient = await GetPatient(patient.Id, default(CancellationToken));

                    await _publishEndpoint.Publish<PatientUpdated>(new
                    {
                        Id = updatedPatient.Id,
                        FirstName = updatedPatient.FirstName,
                        LastName = updatedPatient.LastName,
                        MiddleName = updatedPatient.MiddleName,
                        PhotoUrl = updatedPatient.Account.PhotoUrl,
                        DateOfBirth = updatedPatient.DateOfBirth
                    });
                }

                return r == 0 ? null : patient;
            }
        }

        public async Task<int> DeletePatient(int id, CancellationToken cancellationToken)
        {
            var patient = await GetPatient(id, default(CancellationToken));
            if (patient != null)
            {
                string query = "DELETE FROM [Patients] WHERE Id = @Id";

                using (var connection = _profileDbContext.CreateConnection())
                {
                    var r = await connection.ExecuteAsync(query, new { Id = id });

                    if (r != 0)
                    {
                        await _publishEndpoint.Publish<PatientDeleted>(new
                        {
                            Id = patient.Id,
                            FirstName = patient.FirstName,
                            LastName = patient.LastName,
                            MiddleName = patient.MiddleName,
                            PhotoUrl = patient.Account.PhotoUrl,
                            DateOfBirth = patient.DateOfBirth
                        });
                    }
                    return r;
                }
            }
            return 0;
        }
    }
}
