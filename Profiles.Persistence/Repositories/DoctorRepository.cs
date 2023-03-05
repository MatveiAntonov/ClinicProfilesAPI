using Dapper;
using Events;
using MassTransit;
using Profiles.Domain.Entities;
using Profiles.Domain.Entities.ForeignEntities;
using Profiles.Domain.Interfaces.Repositories;
using Profiles.Persistence.Contexts;
using System.Data;
using static Dapper.SqlMapper;

namespace Profiles.Persistence.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly ProfilesDbContext _profileDbContext;
        private readonly IPublishEndpoint _publishEndpoint;
        public DoctorRepository(ProfilesDbContext profileDbContext, IPublishEndpoint publishEndpoint)
        {
            _profileDbContext = profileDbContext;
            _publishEndpoint = publishEndpoint;
        }
        public async Task<IEnumerable<Doctor?>> GetAllDoctors(CancellationToken cancellationToken)
        {
            var query = "SELECT  * " +
                "FROM [Doctors] " +
                "JOIN Specializations ON Doctors.SpecializationId = Specializations.Id " +
                "JOIN Offices ON Doctors.OfficeId = Offices.Id " +
                "JOIN Accounts ON Doctors.AccountId = Accounts.Id";


            using (var connection = _profileDbContext.CreateConnection())
            {
                var doctors = await connection.QueryAsync<Doctor, Specialization, Office, Account, Doctor>(query,
                    (doctor, specialization, office, account) =>
                    {
                        doctor.Specialization = specialization;
                        doctor.Office = office;
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
                "WHERE Doctors.Id = @Id";

            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);

            using (var connection = _profileDbContext.CreateConnection())
            {
                var doctors = await connection.QueryAsync<Doctor, Specialization, Office, Account, Doctor>(query,
                    (doctor, specialization, office, account) =>
                    {
                        doctor.Specialization = specialization;
                        doctor.Office = office;
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
                "OUTPUT INSERTED.Id " +
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
                var r = connection.ExecuteScalar<int>(query, parameters);

                var insertedDoctor = await GetDoctor(r, default(CancellationToken));

                await _publishEndpoint.Publish<DoctorCreated>(new
                {
                    Id = r,
                    FirstName = insertedDoctor.FirstName,
                    LastName = insertedDoctor.LastName,
                    MiddleName = insertedDoctor.MiddleName,
                    SpecializationName = insertedDoctor.Specialization.SpecializationName,
                    PhotoUrl = insertedDoctor.Account.PhotoUrl,
                    OfficeName = insertedDoctor.Office.OfficeName,
                    OfficeCity = insertedDoctor.Office.City,
                    OfficeRegion = insertedDoctor.Office.Region,
                    OfficeStreet = insertedDoctor.Office.Street,
                    OfficePostalCode = insertedDoctor.Office.PostalCode,
                    OfficeHouseNumber = insertedDoctor.Office.HouseNumber
                });

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

                if (r != 0)
                {
                    var updatedDoctor = await GetDoctor(doctor.Id, default(CancellationToken));

                    await _publishEndpoint.Publish<DoctorUpdated>(new
                    {
                        Id = updatedDoctor.Id,
                        FirstName = updatedDoctor.FirstName,
                        LastName = updatedDoctor.LastName,
                        MiddleName = updatedDoctor.MiddleName,
                        SpecializationName = updatedDoctor.Specialization.SpecializationName,
                        PhotoUrl = updatedDoctor.Account.PhotoUrl,
                        OfficeName = updatedDoctor.Office.OfficeName,
                        OfficeCity = updatedDoctor.Office.City,
                        OfficeRegion = updatedDoctor.Office.Region,
                        OfficeStreet = updatedDoctor.Office.Street,
                        OfficePostalCode = updatedDoctor.Office.PostalCode,
                        OfficeHouseNumber = updatedDoctor.Office.HouseNumber
                    });
                }

                return r == 0 ? null : doctor;
            }
        }

        public async Task<int> DeleteDoctor(int id, CancellationToken cancellationToken)
        {
            var doctor = await GetDoctor(id, default(CancellationToken));

            if (doctor is not null)
            {
                string query = "DELETE FROM [Doctors] WHERE Id = @Id";

                using (var connection = _profileDbContext.CreateConnection())
                {
                    var r = await connection.ExecuteAsync(query, new { Id = id });

                    if (r != 0)
                    {

                        await _publishEndpoint.Publish<DoctorDeleted>(new
                        {
                            Id = doctor.Id,
                            FirstName = doctor.FirstName,
                            LastName = doctor.LastName,
                            MiddleName = doctor.MiddleName,
                            SpecializationName = doctor.Specialization.SpecializationName,
                            PhotoUrl = doctor.Account.PhotoUrl,
                            OfficeName = doctor.Office.OfficeName,
                            OfficeCity = doctor.Office.City,
                            OfficeRegion = doctor.Office.Region,
                            OfficeStreet = doctor.Office.Street,
                            OfficePostalCode = doctor.Office.PostalCode,
                            OfficeHouseNumber = doctor.Office.HouseNumber
                        });
                    }
                    return r;
                }
            }
            return 0;
        }
    }
}
