using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DriverExpansesTracker.API.Filters;
using DriverExpansesTracker.Repository.Entities;
using DriverExpansesTracker.Services.Helpers;
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
        private IUrlHelper _urlHelper;

        public PaymentsController(IUserService userService, IPaymentService paymentService,IJourneyService journeyService,IUrlHelper urlHelper)
        {
            _userService = userService;
            _paymentService = paymentService;
            _journeyService = journeyService;
            _urlHelper = urlHelper;
        }

        [HttpGet("payments",Name ="GetPayments")]
        //[Route("payments")]
        //[Route("journeys/{journeyId}/payments")]
        public IActionResult GetPayments(string userId,int ?journeyId,PaymentResourceParameters resourceParameters)
        {
            //journeyId can't be null need to fix it
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
                var pagedPayments = _paymentService.GetPayments(userId,resourceParameters);

                var del = new CreateResourceUriDel(CreateResourceUri);

                var pagingHeader = _paymentService.GetPagingHeader(pagedPayments, resourceParameters,del);

                Response.Headers.Add("X-Pagination", pagingHeader.ToJson());

                var payments = _paymentService.GetPayments(pagedPayments);

                return Ok(payments);

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

        private string CreateResourceUri(PaymentResourceParameters resourceParameters, ResourceUriType type)
        {
            switch (type)
            {
                case ResourceUriType.PreviousPage:
                    return _urlHelper.Link("GetPayments", new
                    {
                        pageNumber = resourceParameters.PageNumber - 1,
                        pageSize = resourceParameters.PageNumber
                    });
                case ResourceUriType.NextPage:
                    return _urlHelper.Link("GetPayments", new
                    {
                        pageNumber = resourceParameters.PageNumber + 1,
                        pageSize = resourceParameters.PageNumber
                    });
                default:
                    return _urlHelper.Link("GetPayments", new
                    {
                        pageNumber = resourceParameters.PageNumber,
                        pageSize = resourceParameters.PageNumber
                    });
            }
        }

    }
}
