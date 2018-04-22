using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DriverExpansesTracker.API.Filters;
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
        private IJourneyService _journeyService;

        public PaymentsController(IUserService userService, IPaymentService paymentService,IJourneyService journeyService)
        {
            _userService = userService;
            _paymentService = paymentService;
            _journeyService = journeyService;
        }

        [HttpGet]
        [Route("payments")]
        [Route("journeys/{journeyId}/payments")]
        public IActionResult GetPayments(string userId,int ?journeyId)
        {
            
            if (journeyId != null)
            {
                if(!_journeyService.JourneyExists(userId,(int)journeyId))
                {
                    return NotFound();
                }
                var payments = _paymentService.GetPayments(userId,(int)journeyId);

                return Ok(payments);
            }
            else
            {
                if (!_userService.UserExists(userId))
                {
                    return NotFound();
                }
                var payments = _paymentService.GetPayments(userId);

                return Ok(payments);

            }
            

        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetPayment(string userId, int id,int? journeyId)
        {
            if (!_userService.UserExists(userId))
            {
                return NotFound();
            }


            if (journeyId != null)
            {
                if (!_journeyService.JourneyExists(userId, (int)journeyId))
                {
                    return NotFound();
                }

                var payment = _paymentService.GetPayment(userId, (int)journeyId,id);

                if (payment == null)
                {
                    return NotFound();
                }

                return Ok(payment);
            }
            else
            {
                if (!_userService.UserExists(userId))
                {
                    return NotFound();
                }

                var payment = _paymentService.GetPayment(userId, id);

                if (payment == null)
                {
                    return NotFound();
                }

                return Ok(payment);
            }
            
        }

    }
}
