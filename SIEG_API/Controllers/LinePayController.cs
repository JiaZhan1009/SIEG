using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SIEG_API.Domain;
using SIEG_API.Dtos;

namespace SIEG_API.Controllers
{
    [EnableCors("AllowAny")]
    [ApiController]
    [Route("api/[Controller]")]
    public class LinePayController : ControllerBase
    {
        private readonly LinePayService _linePayService;
        public LinePayController()
        {
            _linePayService = new LinePayService();
        }

        [HttpPost("Create")]
        public async Task<PaymentResponseDto> CreatePayment(PaymentRequestDto dto)
        {
            return await _linePayService.SendPaymentRequest(dto);
        }

        [HttpPost("Confirm")]
        public async Task<PaymentConfirmResponseDto> ConfirmPayment([FromQuery] string transactionId, [FromQuery] string orderId, PaymentConfirmDto dto)
        {
            return await _linePayService.ConfirmPayment(transactionId, orderId, dto);
        }

        [HttpGet("Cancel")]
        public async void CancelTransaction([FromQuery] string transactionId)
        {
            _linePayService.TransactionCancel(transactionId);
        }
    }
}


