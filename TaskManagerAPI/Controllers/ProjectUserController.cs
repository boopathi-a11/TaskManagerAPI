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
    public class ProjectUserController : ApiController
    {
        private DBModel db = new DBModel();

        // GET: api/ProjectUser
        public IQueryable<ProjectUser> GetProjectUsers()
        {
            return db.ProjectUsers;
        }

        public IQueryable<ProjectUser> GetProjectUsers(string strSortBy)
        {
            if (strSortBy == "FN") {
                return db.ProjectUsers.OrderBy(a => a.First_Name);
            }
            else if (strSortBy == "LN")
            {
                return db.ProjectUsers.OrderBy(a => a.Last_Name);
            }
            else
            {
                return db.ProjectUsers.OrderBy(a => a.Employee_ID);
            }
        }
         
            
        // GET: api/ProjectUser/5
        [ResponseType(typeof(ProjectUser))]
        public IHttpActionResult GetProjectUser(int id)
        {
            ProjectUser projectUser = db.ProjectUsers.Find(id);
            return Ok(projectUser);
        }

        // PUT: api/ProjectUser/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProjectUser(int id, ProjectUser projectUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != projectUser.User_ID)
            {
                return BadRequest();
            }

            db.Entry(projectUser).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectUserExists(id))
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

        // POST: api/ProjectUser
        [ResponseType(typeof(ProjectUser))]
        public IHttpActionResult PostProjectUser(ProjectUser projectUser)
        {          

            db.ProjectUsers.Add(projectUser);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = projectUser.User_ID }, projectUser);
        }

        // DELETE: api/ProjectUser/5
        [ResponseType(typeof(ProjectUser))]
        public IHttpActionResult DeleteProjectUser(int id)
        {
            ProjectUser projectUser = db.ProjectUsers.Find(id);
            if (projectUser == null)
            {
                return NotFound();
            }

            db.ProjectUsers.Remove(projectUser);
            db.SaveChanges();

            return Ok(projectUser);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProjectUserExists(int id)
        {
            return db.ProjectUsers.Count(e => e.User_ID == id) > 0;
        }
    }
}