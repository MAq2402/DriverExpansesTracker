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
        private ICarService _carService;

        public PaymentsController(IUserService userService, 
                                  IPaymentService paymentService,
                                  IJourneyService journeyService,
                                  IUrlHelper urlHelper,
                                  ICarService carService):base(urlHelper)
        {
            _userService = userService;
            _paymentService = paymentService;
            _journeyService = journeyService;
            _carService = carService;
        }

        [HttpGet]
        [Route("payments",Name = Constants.RouteNames.GetPayments)]
        [Route("journeys/{journeyId}/payments" , Name = Constants.RouteNames.GetPaymentsByJourney)]
        public IActionResult GetPayments(string userId,int ?journeyId, int?carId,ResourceParameters resourceParameters)
        {
            if (journeyId.HasValue)
            {
                if(!_journeyService.JourneyExists(userId,journeyId.Value))
                {
                    return NotFound();
                }
                var pagedPayments = _paymentService.GetPagedPaymentsByJourneys(userId, journeyId.Value, resourceParameters);

                pagedPayments.Header.PreviousPageLink = pagedPayments.HasPrevious ? CreateResourceUri(Constants.RouteNames.GetPaymentsByJourney, resourceParameters,ResourceUriType.PreviousPage) : null;
                pagedPayments.Header.PreviousPageLink = pagedPayments.HasNext ? CreateResourceUri(Constants.RouteNames.GetPaymentsByJourney, resourceParameters, ResourceUriType.NextPage) : null;

                Response.Headers.Add(Constants.Headers.XPagination, pagedPayments.Header.ToJson());

                return Ok(pagedPayments.ToList());
            }
            else
            {
                if (!_userService.UserExists(userId))
                {
                    return NotFound();
                }
                var pagedPayments = _paymentService.GetPagedPayments(userId, resourceParameters);

                pagedPayments.Header.PreviousPageLink = pagedPayments.HasPrevious ? CreateResourceUri(Constants.RouteNames.GetPayments, resourceParameters, ResourceUriType.PreviousPage) : null;
                pagedPayments.Header.NextPageLink = pagedPayments.HasNext ? CreateResourceUri(Constants.RouteNames.GetPayments, resourceParameters, ResourceUriType.NextPage) : null;

                Response.Headers.Add(Constants.Headers.XPagination, pagedPayments.Header.ToJson());

                return Ok(pagedPayments.ToList());
            }
        }

        [HttpGet]
        [Route("payments/{id}")]
        [Route("journeys/{journeyId}/payments/{id}")]
        public IActionResult GetPayment(string userId, int id,int? journeyId)
        {

            if (journeyId.HasValue)
            {
                if (!_journeyService.JourneyExists(userId, journeyId.Value))
                {
                    return NotFound();
                }

                var payment = _paymentService.GetPayment(userId, journeyId.Value,id);

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
