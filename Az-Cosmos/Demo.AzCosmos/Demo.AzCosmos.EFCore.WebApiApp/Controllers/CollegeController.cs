
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
using Microsoft.AspNetCore.Http;

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
    
        [HttpPut("DeleteCollegeDatabase")]
        //[Route("DeleteCollegeDatabase")]
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

        [HttpPut("CreateSampleColleges")]
        //[Route("CreateSampleColleges")]
        public async Task<IActionResult>  CreateSampleColleges()
        {
              Log.Debug("Started: Create sampleColleges in cosmos DB for demo purpose");
              try
              {
                  
             await this.service.CreateSampleCollegesAsSync();
                Log.Debug("Finished: create colleges.");
               
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

        [HttpPut("UpdateAddress")]
        // [Route("UpdateAddress")]
         [ProducesResponseType(typeof(List<College>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateAddressforColleges()
        {
            Log.Debug("Started: Update Addresses to existing colleges for demo purpose");
            
            await this.service.UpdateAddressforCollegesAsync();
            Log.Debug("Finished: Update Addresses to existing colleges.");

                Log.Debug("started: Get colleges");
               var colleges= await  this.service.GetColleges();
                Log.Debug("Finished: Get colleges");

            if (colleges==null)
                    return NoContent();

            return Ok(colleges);
        }
         
         [HttpPut("UpdateDepartments")]
        //[Route("UpdateDepartments")]
          [ProducesResponseType(typeof(List<College>),StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateDepartmentsforColleges()
        {
              Log.Debug("Started: Update Departments to existing colleges. This is only for demo purpose");
             await   this.service.UpdateDepartmentsforCollegesAsync();
             Log.Debug("Finished: Update Departments to existing colleges. This is only for demo purpose");

                Log.Debug("started: Get colleges");
                var colleges= await  this.service.GetColleges();
                Log.Debug("Finished: Get colleges");

                if (colleges==null)
                     return Ok(HttpStatusCode.NotFound);
 
            return Ok(colleges);
            
        }
        //[Route("GetAll")]
        [HttpGet("colleges")]
        [ProducesResponseType(typeof(List<College>),StatusCodes.Status200OK)]
         public async Task<ActionResult>  GetColleges()
        { 
           List<College> colleges ; 
            Log.Debug("started: Get colleges. for demo");
            colleges= await this.service.GetColleges();
                
            if (colleges==null)
            {
                Log.Debug("colleges are not found.");
                return NotFound();
            }

           return  Ok(colleges);    
        }

        [HttpGet("{id}", Name = "GetCollege")]
        [HttpGet("With/{id}")]
        // [Route("GetById/{id}")]
        [ProducesResponseType(typeof(College),StatusCodes.Status200OK)]
        public async Task<ActionResult>  GetById(int collegeId)
        {
            Log.Debug($"started: Get college by id {collegeId} for demo");

           College college = await  this.service.GetByIdAsync(collegeId);

            if (college==null)
            {
                Log.Debug($"college id {collegeId} is not found");
                return NotFound();
            }
                return Ok(college);
            
        }

        [HttpGet("WithName/{name}", Name = "GetCollegeByName")]
        //[Route("GetByName/{name}")]
         [ProducesResponseType(typeof(College),StatusCodes.Status200OK)]
        public async Task<ActionResult> GetByName(string collegeName)
        {
             College college ; 
            
            Log.Debug("started:get college by id for demo ");
            college = await this.service.GetByNameAsync(collegeName);

            if (college==null)
             {
                Log.Debug($"college name {collegeName} is not found");
                   return NotFound();
             }
            
            return  Ok(college);
            
        }
}
}