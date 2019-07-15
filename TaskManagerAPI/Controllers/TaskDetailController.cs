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
    public class TaskDetailController : ApiController
    {
        private DBModel db = new DBModel();

        // GET: api/TaskDetail
        public IQueryable<TaskDetail> GetTaskDetails()
        {
            return db.TaskDetails;
        }

        // GET: api/TaskDetail
        public IQueryable<TaskDetail> GetTaskDetails(string strTaskName)
        {
            if (!string.IsNullOrEmpty(strTaskName) && strTaskName !="SD" && strTaskName != "ED" && strTaskName != "P")
            {
                return db.TaskDetails.Where(x => x.Task_Name == strTaskName);
            }          
            else if (strTaskName == "SD")
            {
                return db.TaskDetails.OrderBy(a => a.Start_date);
            }
            else if (strTaskName == "ED")
            {
                return db.TaskDetails.OrderBy(a => a.End_Date);
            }
            else if (strTaskName == "P")
            {
                return db.TaskDetails.OrderBy(a => a.Task_Priority);
            }
            else
            {
                return db.TaskDetails;
            }
        }


        // GET: api/TaskDetail/5
        [ResponseType(typeof(TaskDetail))]
        public IHttpActionResult GetTaskDetail(int id)
        {
            TaskDetail taskDetail = db.TaskDetails.Find(id);
            if (taskDetail == null)
            {
                return NotFound();
            }

            return Ok(taskDetail);
        }

        // PUT: api/TaskDetail/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTaskDetail(int id, TaskDetail taskDetail)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            if (id != taskDetail.Task_ID)
            {
                return BadRequest();
            }

            db.Entry(taskDetail).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskDetailExists(id))
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

        // POST: api/TaskDetail
        [ResponseType(typeof(TaskDetail))]
        public IHttpActionResult PostTaskDetail(TaskDetail taskDetail)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            taskDetail.ISTaskEnded = "N";
            db.TaskDetails.Add(taskDetail);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = taskDetail.Task_ID }, taskDetail);
        }

        // DELETE: api/TaskDetail/5
        [ResponseType(typeof(TaskDetail))]
        public IHttpActionResult DeleteTaskDetail(int id)
        {
            TaskDetail taskDetail = db.TaskDetails.Find(id);
            if (taskDetail == null)
            {
                return NotFound();
            }

            db.TaskDetails.Remove(taskDetail);
            db.SaveChanges();

            return Ok(taskDetail);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TaskDetailExists(int id)
        {
            return db.TaskDetails.Count(e => e.Task_ID == id) > 0;
        }

    }
}