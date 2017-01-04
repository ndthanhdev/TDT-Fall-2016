using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ToDoApi.Models;
using ToDoApi.Data;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ToDoApi.Controllers
{
    [Authorize("User")]
    [Route("[controller]")]
    public class EventController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Add an event
        /// </summary>
        /// <param name="eventInput"></param>
        /// <returns></returns>
        /// <response code="201">event was added</response>
        /// <response code="400">exception, can't add event</response>  
        [HttpPost("Add")]
        [ProducesResponseType(typeof(Event), 201)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<IActionResult> Add([FromBody, Required] Event eventInput)
        {
            try
            {
                var account = await GetAccountWithEventsById(ClaimTypes.NameIdentifier);
                account.Events.Add(eventInput);
                await _context.SaveChangesAsync();
                return Ok(eventInput);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get an event by id
        /// </summary>
        /// <param name="id">event id</param>
        /// <returns></returns>
        /// <response code="200">an event</response>
        /// <response code="400">exception, can't get event</response>  
        /// <response code="404">not found event</response>  
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Event), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<IActionResult> GetById([Required]string id)
        {
            try
            {
                var account = await GetAccountWithEventsById(ClaimTypes.NameIdentifier);
                var eventObject = account.Events.SingleOrDefault(e => e.EventId == id);
                if (eventObject == null)
                {
                    return NotFound();
                }
                return Ok(eventObject);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get all event of account
        /// </summary>
        /// <returns></returns>
        /// <response code="200">all event of account</response>
        /// <response code="400">exception, can't get event</response>  
        [HttpGet("GetAll")]
        [ProducesResponseType(typeof(Event[]), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var account = await GetAccountWithEventsById(ClaimTypes.NameIdentifier);
                return Ok(account.Events);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Update an event
        /// </summary>
        /// <param name="eventInput"></param>
        /// <returns></returns>
        /// <response code="200">event was updated</response>
        /// <response code="400">exception, can't update event</response>  
        /// <response code="404">not found event</response>  
        [HttpPost("Update")]
        //[ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<IActionResult> Update([FromBody, Required]Event eventInput)
        {
            try
            {
                // this code for no tracking event
                var aid = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var account = await _context.Accounts.AsNoTracking().Include(a => a.Events).SingleOrDefaultAsync(a => a.AccountId == aid);

                var eventObject = account.Events.SingleOrDefault(e => e.EventId == eventInput.EventId);
                if (eventObject == null)
                {
                    return NotFound();
                }
                _context.Update(eventInput);
                await _context.SaveChangesAsync();
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Delete an event
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">event was deleted</response>
        /// <response code="400">exception, can't update event</response>  
        /// <response code="404">not found event</response>  
        [HttpGet("Delete/{id}")]
        //[ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<IActionResult> Delete([Required]string id)
        {
            try
            {
                var account = await GetAccountWithEventsById(ClaimTypes.NameIdentifier);
                var eventObject = account.Events.SingleOrDefault(e => e.EventId == id);
                if (eventObject == null)
                {
                    return NotFound();
                }
                _context.Remove(eventObject);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private async Task<Account> GetAccountWithEventsById(string accountId)
        {
            var aid = User.FindFirst(accountId).Value;
            var account = await _context.Accounts.Include(a => a.Events).SingleOrDefaultAsync(a => a.AccountId == aid);
            return account;
        }
    }
}
