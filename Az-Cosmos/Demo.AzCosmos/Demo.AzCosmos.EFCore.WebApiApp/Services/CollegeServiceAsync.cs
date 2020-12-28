using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Azure.Cosmos;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Demo.AzCosmos.EFCore.WebApiApp.Config;
using System.Linq;
using System.Collections.Generic;
using Demo.AzCosmos.EFCore.WebApiApp.Models;
using Demo.AzCosmos.EFCore.WebApiApp.Repositories.Infrastructure;
using System.Net;

namespace Demo.AzCosmos.EFCore.WebApiApp.Services
{

    /// <summary>
    ///  this is helper claas to fill data in entities for demo purpose 
    /// </summary>
    public class CollegeServiceAsync : ICollegeServiceAsync
    {
        private IRepositoryAsync<College> _repository { get; }

        public CollegeServiceAsync(IRepositoryAsync<College> repository)
        {
            _repository = repository;
        }

        public async Task DeleteCollegeDatabaseIfExists()
        {
            await _repository.DeleteDatabaseAsync();

        }

        // this only for demo 
        public async Task CreateSampleCollegesAsSync()
        {

            // await DeleteCollegeDatabaseIfExists(context);
            await _repository.CreateDatabaseIfNotExistsAsync();

            int collegeId = 1;

            if (!(await _repository.IsExists(collegeId)))
            {

                var chicagoUniversity = CollegeServiceHelper.CreateChicagoUniversity();
                await _repository.SaveChangesAsync(chicagoUniversity);
            }

            collegeId = 2;
            if (!(await _repository.IsExists(collegeId)))
            {
                var princetonUniversity = CollegeServiceHelper.CreatePrincetonUniversity();
                await _repository.SaveChangesAsync(princetonUniversity);
            }

            collegeId = 3;
            if (!(await _repository.IsExists(collegeId)))
            {
                var harvardUniversity = CollegeServiceHelper.CreateHarvardUniversity();
                await _repository.SaveChangesAsync(harvardUniversity);
            }

            collegeId = 4;
            if (!(await _repository.IsExists(collegeId)))
            {

                var columbiauniversity = CollegeServiceHelper.CreateColumbiaUniversity();
                await _repository.SaveChangesAsync(columbiauniversity);
            }


            collegeId = 5;

            if (!(await _repository.IsExists(collegeId)))
            {
                var yaleUniversity = CollegeServiceHelper.CreateYaleUniversity();
                await _repository.SaveChangesAsync(yaleUniversity);
            }

            collegeId = 6;

            if (!(await _repository.IsExists(collegeId)))
            {
                var UniversityOfPennsylvania = CollegeServiceHelper.CreateUniversityOfPennsylvania();
                await _repository.SaveChangesAsync(UniversityOfPennsylvania);
            }
        }
        public async Task UpdateAddressforCollegesAsync()
        {
            // await DeleteCollegeDatabaseIfExists(context);
            await _repository.CreateDatabaseIfNotExistsAsync();
            //var Colleges =_repository.GetAll();

            int collegeId = 1;

            if ((await _repository.IsExists(collegeId)))
            {
                var chicagoUniversity = await _repository.GetByIdAsync(collegeId);
                chicagoUniversity.Address = CollegeServiceHelper.CreateChicagoUniversityAddress();
                await _repository.SaveChangesAsync(chicagoUniversity);
            }

            collegeId = 2;
            if ((await _repository.IsExists(collegeId)))
            {
                var princetonUniversity = await _repository.GetByIdAsync(collegeId);
                princetonUniversity.Address = CollegeServiceHelper.CreatePrincetonUniversityAddress();
                await _repository.SaveChangesAsync(princetonUniversity);
            }

            collegeId = 3;
            if ((await _repository.IsExists(collegeId)))
            {
                var harvardUniversity = await _repository.GetByIdAsync(collegeId);
                harvardUniversity.Address = CollegeServiceHelper.CreateHarvardUniversityAddress();
                await _repository.SaveChangesAsync(harvardUniversity);
            }

            collegeId = 4;
            if ((await _repository.IsExists(collegeId)))
            {
                var columbiauniversity = await _repository.GetByIdAsync(collegeId);
                columbiauniversity.Address = CollegeServiceHelper.CreateColumbiaUniversityAddress();

                await _repository.SaveChangesAsync(columbiauniversity);
            }
            collegeId = 5;
            if ((await _repository.IsExists(collegeId)))
            {
                var yaleUniversity = await _repository.GetByIdAsync(collegeId);
                yaleUniversity.Address = CollegeServiceHelper.CreateYaleUniversityAddress();

                await _repository.SaveChangesAsync(yaleUniversity);
            }

            collegeId = 6;
            if (!(await _repository.IsExists(collegeId)))
            {
                var UniversityOfPennsylvania = await _repository.GetByIdAsync(collegeId);
                UniversityOfPennsylvania.Address = CollegeServiceHelper.CreateUniversityOfPennsylvaniaAddress();

                await _repository.SaveChangesAsync(UniversityOfPennsylvania);
            }
        }
        public async Task UpdateDepartmentsforCollegesAsync()
        {

            //  DeleteCollegeDatabaseIfExists(context);
            await _repository.CreateDatabaseIfNotExistsAsync();
            int collegeId = 1;

            if (await (_repository.IsExists(collegeId)))
            {
                var chicagoUniversity = await _repository.GetByIdAsync(collegeId);
                chicagoUniversity.Departments = CollegeServiceHelper.CreateDepartmentsForChicagoUniversity();
                await _repository.SaveChangesAsync(chicagoUniversity);
            }

            collegeId = 2;
            if (await (_repository.IsExists(collegeId)))
            {
                var princetonUniversity = await _repository.GetByIdAsync(collegeId);
                princetonUniversity.Departments = CollegeServiceHelper.CreateDepartmentsForPrincetonUniversity();

                await _repository.SaveChangesAsync(princetonUniversity);
            }

            collegeId = 3;
            if (await (_repository.IsExists(collegeId)))
            {
                var harvardUniversity = await _repository.GetByIdAsync(collegeId);
                harvardUniversity.Departments = CollegeServiceHelper.CreateDepartmentsForHarvardUniversity();
                await _repository.SaveChangesAsync(harvardUniversity);
            }

            collegeId = 4;
            if (await (_repository.IsExists(collegeId)))
            {
                var columbiauniversity = await _repository.GetByIdAsync(collegeId);
                columbiauniversity.Departments = CollegeServiceHelper.CreateDepartmentsForColumbiaUniversity();

                await _repository.SaveChangesAsync(columbiauniversity);
            }
            collegeId = 5;

            if (await (_repository.IsExists(collegeId)))
            {
                var yaleUniversity = await _repository.GetByIdAsync(collegeId);
                yaleUniversity.Departments = CollegeServiceHelper.CreateDepartmentsForYaleUniversity();

                await _repository.SaveChangesAsync(yaleUniversity);
            }
            collegeId = 6;

            if (await (_repository.IsExists(collegeId)))
            {
                var UniversityOfPennsylvania = await _repository.GetByIdAsync(collegeId);
                UniversityOfPennsylvania.Departments = CollegeServiceHelper.CreateDepartmentsForUniversityOfPennsylvania();

                await _repository.SaveChangesAsync(UniversityOfPennsylvania);
            }
        }

        public async Task<List<College>> GetColleges()
        {
            return (await _repository.GetAllAsync()).ToList();
        }

        public async Task<College> GetByIdAsync(int collegeId)
        {
            return await _repository.GetByIdAsync(collegeId);
        }

        public async Task<College> GetByNameAsync(string collegeName)
        {

            if (!string.IsNullOrWhiteSpace(collegeName))
                return await _repository.GetByNameAsync(collegeName);
            else
                throw new Exception($"{nameof(collegeName)} should not be null.");
        }

        public async Task WriteItemsToConsoleAsync()
        {
            CollegeServiceHelper.WriteItemsToConsole(await GetColleges());
        }

    }
}