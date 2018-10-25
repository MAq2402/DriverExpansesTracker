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
using RiskFirst.Hateoas;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DriverExpansesTracker.API.Controllers
{
    [Route("api/users/{userId}")]
    [EnableCors("MyPolicy")]
    [Authorize(Policy = "User")]
    [ValidateAuthorizedUserFilter]
    [ValidateIfUserIsNotLoggedOut]
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
                                  ICarService carService,
                                  ILinksService linksService) : base(urlHelper, linksService)
        {
            _userService = userService;
            _paymentService = paymentService;
            _journeyService = journeyService;
            _carService = carService;
        }

        [HttpGet("payments", Name = Constants.RouteNames.GetPayments)]

        public async Task<IActionResult> GetPayments(string userId, ResourceParameters resourceParameters)
        {
            if (!_userService.UserExists(userId))
            {
                return NotFound();
            }
            var pagedPayments = _paymentService.GetPagedPayments(userId, resourceParameters);

            AddPaginationHeader(pagedPayments, Constants.RouteNames.GetPayments, resourceParameters);

            await AddLinksToCollectionAsync(pagedPayments);

            return Ok(pagedPayments);
        }

        [HttpGet("journeys/{journeyId}/payments", Name = Constants.RouteNames.GetPaymentsByJourney)]
        public async Task<IActionResult> GetPaymentsByJourney(string userId, int journeyId, ResourceParameters resourceParameters)
        {
            if (!_journeyService.JourneyExists(userId, journeyId))
            {
                return NotFound();
            }
            var pagedPayments = _paymentService.GetPagedPaymentsByJourneys(userId, journeyId, resourceParameters);

            AddPaginationHeader(pagedPayments, Constants.RouteNames.GetPaymentsByJourney, resourceParameters);

            await AddLinksToCollectionAsync(pagedPayments);

            return Ok(pagedPayments);
        }


        [HttpGet("payments/{id}", Name = Constants.RouteNames.GetPayment)]

        public async  Task<IActionResult> GetPayment(string userId, int id)
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

            await _linksService.AddLinksAsync(payment);

            return Ok(payment);
        }

        [HttpGet("journeys/{journeyId}/payments/{id}", Name =Constants.RouteNames.GetPaymentByJourney)]
        public async Task<IActionResult> GetPaymentByJourney(string userId, int id, int journeyId)
        {
            if (!_journeyService.JourneyExists(userId, journeyId))
            {
                return NotFound();
            }

            var payment = _paymentService.GetPayment(userId, journeyId, id);

            if (payment == null)
            {
                return NotFound();
            }

            await _linksService.AddLinksAsync(payment);

            return Ok(payment);
        }
        [HttpPost("payments/{id}/accept")]

        public IActionResult AcceptPayment(string userId, int id)
        {
            var paymentToAccept = _paymentService.GetPayment(userId, id);

            if(paymentToAccept == null)
            {
                return NotFound();
            }

            _paymentService.AcceptPayment(paymentToAccept);

            return NoContent();
        }
    }
}
