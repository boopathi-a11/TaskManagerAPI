using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using TaskManagerAPI.Models;

namespace TaskManagerAPI.Controllers
{
    public class ProjectDetailController : ApiController
    {
        private DBModel db = new DBModel();

        // GET: api/ProjectDetail
        public IQueryable<ProjectDetail> GetProjectDetails()
        {
            return db.ProjectDetails;
        }

        public IQueryable<ProjectDetail> GetProjectUsers(string strSortBy)
        {
            if (strSortBy == "SD")
            {
                return db.ProjectDetails.OrderBy(a => a.Start_date);
            }
            else if (strSortBy == "ED")
            {
                return db.ProjectDetails.OrderBy(a => a.End_date);
            }
            else
            {
                return db.ProjectDetails.OrderBy(a => a.Project_Priority);
            }
        }

        // GET: api/ProjectDetail/5
        [ResponseType(typeof(ProjectDetail))]
        public IHttpActionResult GetProjectDetail(int id)
        {
            ProjectDetail projectDetail = db.ProjectDetails.Find(id);           

            return Ok(projectDetail);
        }

        // PUT: api/ProjectDetail/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProjectDetail(int id, ProjectDetail projectDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != projectDetail.Project_ID)
            {
                return BadRequest();
            }

            db.Entry(projectDetail).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectDetailExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/ProjectDetail
        [ResponseType(typeof(ProjectDetail))]
        public IHttpActionResult PostProjectDetail(ProjectDetail projectDetail)
        {
            db.ProjectDetails.Add(projectDetail);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = projectDetail.Project_ID }, projectDetail);
        }

        // DELETE: api/ProjectDetail/5
        [ResponseType(typeof(ProjectDetail))]
        public IHttpActionResult DeleteProjectDetail(int id)
        {
            ProjectDetail projectDetail = db.ProjectDetails.Find(id);
            if (projectDetail == null)
            {
                return NotFound();
            }

            db.ProjectDetails.Remove(projectDetail);
            db.SaveChanges();

            return Ok(projectDetail);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProjectDetailExists(int id)
        {
            return db.ProjectDetails.Count(e => e.Project_ID == id) > 0;
        }
    }
}