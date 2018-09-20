using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DriverExpansesTracker.API.Filters;
using DriverExpansesTracker.Repository.Entities;
using DriverExpansesTracker.Services.Helpers;
using DriverExpansesTracker.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DriverExpansesTracker.API.Controllers
{
    [Route("api/users/{userId}")]
    [EnableCors("MyPolicy")]
    [Authorize(Policy ="User")]
    [ValidateAuthorizedUserFilter]
    public class PaymentsController : BaseController
    {
        private IUserService _userService;
        private IPaymentService _paymentService;
        private IJourneyService _journeyService;

        public PaymentsController(IUserService userService, 
                                  IPaymentService paymentService,
                                  IJourneyService journeyService,
                                  IUrlHelper urlHelper):base(urlHelper)
        {
            _userService = userService;
            _paymentService = paymentService;
            _journeyService = journeyService;
        }

        [HttpGet]
        [Route("payments",Name = "GetPayments")]
        [Route("journeys/{journeyId}/payments")]
        public IActionResult GetPayments(string userId,int ?journeyId,ResourceParameters resourceParameters)
        {
            //Apply paging too!
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
                var pagedPayments = _paymentService.GetPagedPayments(userId,resourceParameters);

                pagedPayments.Header.PreviousPageLink = pagedPayments.HasPrevious ? CreateResourceUri("GetPayments", resourceParameters, ResourceUriType.PreviousPage) : null;
                pagedPayments.Header.NextPageLink = pagedPayments.HasNext ? CreateResourceUri("GetPayments", resourceParameters, ResourceUriType.NextPage) : null;

                Response.Headers.Add("X-Pagination", pagedPayments.Header.ToJson());

                return Ok(pagedPayments.ToList());
            }
        }

        [HttpGet]
        [Route("payments/{id}")]
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
