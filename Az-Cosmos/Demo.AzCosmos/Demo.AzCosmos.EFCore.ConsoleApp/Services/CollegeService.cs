using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Azure.Cosmos;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Demo.AzCosmos.EFCore.ConsoleApp.Config;
using System.Linq;
using System.Collections.Generic;
using Demo.AzCosmos.EFCore.ConsoleApp.Models;
using Demo.AzCosmos.EFCore.ConsoleApp.Repositories.Infrastructure;

namespace Demo.AzCosmos.EFCore.ConsoleApp.Services
{
  /// <summary>
  ///  this is helper claas to fill data in entities for demo purpose 
  /// </summary>
    public class CollegeService
    {
         private readonly IRepository<College> _repository ;

        public CollegeService(IRepository<College> repository) 
        {
            _repository = repository;
        }
    
      public void  DeleteCollegeDatabaseIfExists()
      {
                _repository.DeleteDatabase();
      }
      public void CreateSampleColleges()
    {
              //_repository.DeleteDatabase();
               _repository.CreateDatabaseIfNotExists();
             
                int collegeId = 1;

                if (!( _repository.IsExists(collegeId)))
                {
                    var chicagoUniversity = CollegeServiceHelper.CreateChicagoUniversity();
                     _repository.SaveChanges(chicagoUniversity);
                }

                collegeId = 2;
               if (!( _repository.IsExists(collegeId)))
                {
                   var  princetonUniversity = CollegeServiceHelper.CreatePrincetonUniversity();
                        _repository.SaveChanges(princetonUniversity);
                }

                collegeId = 3;
                 if (!( _repository.IsExists(collegeId)))
                {
                    var harvardUniversity =CollegeServiceHelper.CreateHarvardUniversity();
                   _repository.SaveChanges(harvardUniversity);
                }

                   collegeId = 4;
                if (!( _repository.IsExists(collegeId)))
                {

                  var columbiauniversity = CollegeServiceHelper.CreateColumbiaUniversity();
                   _repository.SaveChanges(columbiauniversity);
                }

                 collegeId = 5;

                if (!( _repository.IsExists(collegeId)))
                {
                    var yaleUniversity = CollegeServiceHelper.CreateYaleUniversity();
                      _repository.SaveChanges(yaleUniversity);
                }

                 collegeId = 6;
                if (!( _repository.IsExists(collegeId)))
                {
                    var UniversityOfPennsylvania = CollegeServiceHelper.CreateUniversityOfPennsylvania();
                    _repository.SaveChanges(UniversityOfPennsylvania);
                }
        }

        public void UpdateAddressforColleges()
        {

                //  DeleteCollegeDatabaseIfExists(context);
                 _repository.CreateDatabaseIfNotExists();
               int collegeId = 1;

                if (( _repository.IsExists(collegeId)))
                {
                 var  chicagoUniversity= _repository.GetById(collegeId);
                       chicagoUniversity.Address = CollegeServiceHelper.CreateChicagoUniversityAddress();
                   _repository.SaveChanges(chicagoUniversity);
                }

                collegeId = 2;
               if (( _repository.IsExists(collegeId)))
                {
                 var  princetonUniversity=  _repository.GetById(collegeId);
                      princetonUniversity.Address = CollegeServiceHelper.CreatePrincetonUniversityAddress();

                    _repository.SaveChanges(princetonUniversity);
                }

                collegeId = 3;
                 if (( _repository.IsExists(collegeId)))
                {
                 var  harvardUniversity= _repository.GetById(collegeId);
                      harvardUniversity.Address = CollegeServiceHelper.CreateHarvardUniversityAddress();
                    _repository.SaveChanges(harvardUniversity);
                }

                   collegeId = 4;
                if (( _repository.IsExists(collegeId)))
                {
                  var  columbiauniversity= _repository.GetById(collegeId);
                   columbiauniversity.Address= CollegeServiceHelper.CreateColumbiaUniversityAddress();

                    _repository.SaveChanges(columbiauniversity);
                }
                 collegeId = 5;

                if (( _repository.IsExists(collegeId)))
                {
                 var  yaleUniversity= _repository.GetById(collegeId);
                 yaleUniversity.Address= CollegeServiceHelper.CreateYaleUniversityAddress();

                     _repository.SaveChanges(yaleUniversity);
                }
                 collegeId = 6;

                if (( _repository.IsExists(collegeId)))
                {
                 var  UniversityOfPennsylvania= _repository.GetById(collegeId);
                 UniversityOfPennsylvania.Address= CollegeServiceHelper.CreateUniversityOfPennsylvaniaAddress();

                     _repository.SaveChanges(UniversityOfPennsylvania);
                }

        }

        public void UpdateDepartmentsforColleges()
        {
                //  DeleteCollegeDatabaseIfExists(context);
                 _repository.CreateDatabaseIfNotExists();
               int collegeId = 1;

                if (( _repository.IsExists(collegeId)))
                {
                 var  chicagoUniversity= _repository.GetById(collegeId);
                       chicagoUniversity.Departments= CollegeServiceHelper.CreateDepartmentsForChicagoUniversity();
                   _repository.SaveChanges(chicagoUniversity);
                }

                collegeId = 2;
               if (( _repository.IsExists(collegeId)))
                {
                 var  princetonUniversity=  _repository.GetById(collegeId);
                      princetonUniversity.Departments=CollegeServiceHelper.CreateDepartmentsForPrincetonUniversity();

                    _repository.SaveChanges(princetonUniversity);
                }

                collegeId = 3;
                 if (( _repository.IsExists(collegeId)))
                {
                 var  harvardUniversity= _repository.GetById(collegeId);
                      harvardUniversity.Departments=CollegeServiceHelper.CreateDepartmentsForHarvardUniversity();
                    _repository.SaveChanges(harvardUniversity);
                }

                   collegeId = 4;
                if (( _repository.IsExists(collegeId)))
                {
                  var  columbiauniversity= _repository.GetById(collegeId);
                   columbiauniversity.Departments=CollegeServiceHelper.CreateDepartmentsForColumbiaUniversity();

                    _repository.SaveChanges(columbiauniversity);
                }
                 collegeId = 5;

                if (( _repository.IsExists(collegeId)))
                {
                 var  yaleUniversity= _repository.GetById(collegeId);
                 yaleUniversity.Departments=CollegeServiceHelper.CreateDepartmentsForYaleUniversity();

                     _repository.SaveChanges(yaleUniversity);
                }
                 collegeId = 6;

                if (( _repository.IsExists(collegeId)))
                {
                 var  UniversityOfPennsylvania= _repository.GetById(collegeId);
                 UniversityOfPennsylvania.Departments=CollegeServiceHelper.CreateDepartmentsForUniversityOfPennsylvania();

                     _repository.SaveChanges(UniversityOfPennsylvania);
                }
        }
       public List<College> GetColleges()
       {
         return _repository.GetAll().ToList();
      }
       public void WriteItemsToConsole()
       {
            CollegeServiceHelper.WriteItemsToConsole(GetColleges());
       }
    }
}