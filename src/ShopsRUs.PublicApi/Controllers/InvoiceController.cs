using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopsRUs.Application.Interfaces.Services;
using ShopsRUs.Contracts.Public.Invoice;

namespace ShopsRUs.PublicApi.Controllers;

public class InvoiceController : ApiController
{
    private readonly IInvoiceService _invoiceService;
    private readonly IMapper _mapper;

    public InvoiceController(IInvoiceService invoiceService, IMapper mapper)
    {
        _invoiceService = invoiceService;
        _mapper = mapper;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CreateInvoiceResponse>> CreateInvoice(CreateInvoiceRequest request)
    {
        if (request == null || request.Products == null || request.Customer == null)
            return BadRequest();

        var invoice = _invoiceService.GenerateInvoice(request.Products, request.Customer);

        return Ok(_mapper.Map<CreateInvoiceResponse>(invoice));

    }
}
