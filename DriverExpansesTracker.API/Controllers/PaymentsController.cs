using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DriverExpansesTracker.Services.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DriverExpansesTracker.API.Controllers
{
    [Route("api/users/{userId}")]
    public class PaymentsController : Controller
    {
        private IUserService _userService;
        private IPaymentService _paymentService;

        public PaymentsController(IUserService userService,IPaymentService paymentService)
        {
            _userService = userService;
            _paymentService = paymentService;
        }

        [HttpGet]
        [Route("payments")]
        [Route("journeys/{journeyId}/payments")]
        public IActionResult GetPayments(string userId,int?journeyId)
        {
            if(!_userService.UserExists(userId))
            {
                return NotFound();
            }

            if(journeyId!=null)
            {
                var payments = _paymentService.GetPayments(userId, (int)journeyId);

                return Ok(payments);
            }
            else
            {
                var payments = _paymentService.GetPayments(userId);

                return Ok(payments);
            }
            
        }

        //[HttpGet]
        //[Route("payments/{id}")]
        //[Route("journeys/{journeyId}/payments/{id}")]
        //public IActionResult GetPayment(string userId,int id ,int? journeyId)
        //{

        //}

        //// GET api/<controller>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<controller>
        //[HttpPost]
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/<controller>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/<controller>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
