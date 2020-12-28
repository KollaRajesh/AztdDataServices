
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.AzCosmos.EFCore.WebApiApp.Repositories;
using Demo.AzCosmos.EFCore.WebApiApp.Services;
using Demo.AzCosmos.EFCore.WebApiApp.Models;
using Microsoft.AspNetCore.Mvc;

using Microsoft.Extensions.Logging;
using Serilog;
using System.Net;

namespace Demo.AzCosmos.EFCore.WebApiApp.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CollegeController:ControllerBase
    {
        private  ICollegeServiceAsync service { get; }

        public CollegeController(ICollegeServiceAsync service) 
        {
            this.service = service;
        }
    
        [HttpPost]
        [Route("DeleteCollegeDatabase")]
        public async Task<IActionResult>  DeleteCollegeDatabaseIfExists()
        {

            Log.Debug("stated:Delete College Database started and This is only for demo purpose");
             
             try
             {
             await this.service.DeleteCollegeDatabaseIfExists();
             Log.Debug("finished:Delete College Database started and This is only for demo purpose");
             return Ok();
             }catch (System.Exception ex)
            {
                    Log.Debug(ex.Message);
                    return Ok(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [Route("CreateSampleColleges")]
        public async Task<IActionResult>  CreateSampleColleges()
        {
              Log.Debug("Started: Create College Database. This is only for demo purpose");
              try
              {
                  
             await this.service.CreateSampleCollegesAsSync();
                Log.Debug("Finished: Update Addresses to existing colleges.");
               
               Log.Debug("started: Get colleges");
                var colleges= await  this.service.GetColleges();
                   Log.Debug("Finished: Get colleges");

                if (colleges!=null)
                     return Ok(colleges);
                else 
                     return Ok(HttpStatusCode.NotFound);
              }
             catch (System.Exception ex)
            {
                    Log.Debug(ex.Message);
                    return Ok(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [Route("UpdateAddress")]
        public async Task<IActionResult> UpdateAddressforColleges()
        {
               Log.Debug("Started: Update Addresses to existing colleges. This is only for demo purpose");
               try
               {

               await this.service.UpdateAddressforCollegesAsync();
               Log.Debug("Finished: Update Addresses to existing colleges.");

                 Log.Debug("started: Get colleges");
                var colleges= await  this.service.GetColleges();
                   Log.Debug("Finished: Get colleges");

                if (colleges!=null)
                     return Ok(colleges);
                else 
                     return Ok(HttpStatusCode.NotFound);
               }
                 catch (System.Exception ex)
                {
                    Log.Debug(ex.Message);
                    return Ok(HttpStatusCode.InternalServerError);
                }
        }
         
         [HttpPost]
        [Route("UpdateDepartments")]
        public async Task<IActionResult> UpdateDepartmentsforColleges()
        {
              Log.Debug("Started: Update Departments to existing colleges. This is only for demo purpose");
             try{
             await   this.service.UpdateDepartmentsforCollegesAsync();
             Log.Debug("Finished: Update Departments to existing colleges. This is only for demo purpose");

                Log.Debug("started: Get colleges");
                var colleges= await  this.service.GetColleges();
                   Log.Debug("Finished: Get colleges");

                if (colleges!=null)
                     return Ok(colleges);
                else 
                     return Ok(HttpStatusCode.NotFound);

             }  catch (System.Exception ex)
            {
                Log.Debug(ex.Message);
                 return Ok(HttpStatusCode.InternalServerError);
            }
        }
        [HttpGet]
        [Route("GetAll")]
         public async Task<ActionResult>  GetColleges()
        { 
           List<College> colleges ; 
            Log.Debug("started: Get colleges");
            try{

             colleges= await  this.service.GetColleges();

                if (colleges!=null)
                return Ok(colleges);
                else 
                return Ok(HttpStatusCode.NotFound);
        }
            catch (System.Exception ex)
            {
                Log.Debug(ex.Message);
                 return Ok(HttpStatusCode.InternalServerError);
            }
        }
        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<ActionResult>  GetById(int collegeId)
        {
               College college ; 
            Log.Debug("Get college by id  for demo started");
            try{

            college = await  this.service.GetByIdAsync(collegeId);
              if (college!=null)
              return Ok(college);
            else 
              return Ok(HttpStatusCode.NotFound);

            }
            catch (System.Exception ex)
            {
                Log.Debug(ex.Message);
                 return Ok(HttpStatusCode.InternalServerError);
            }
        }
        [HttpGet]
        [Route("GetByName/{name}")]
        public async Task<ActionResult> GetByName(string name)
        {
             College college ; 
            
            Log.Debug("Get college by id  for demo started");
            try
            {
            college = await  this.service.GetByNameAsync(name);

            if (college!=null)
                   return Ok(college);
            else 
              return Ok(HttpStatusCode.NotFound);
                
            }
            catch (System.Exception ex)
            {
                Log.Debug(ex.Message);
                 return Ok(HttpStatusCode.NotFound);
            }
            
            
        }
}
}