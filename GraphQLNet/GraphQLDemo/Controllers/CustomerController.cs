using GraphQLDemo.GraphQlClient;
using GraphQLDemo.Model;
using Microsoft.AspNetCore.Mvc;

namespace GraphQLDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerConsumerService _customerConsumerService;

        public CustomerController(CustomerConsumerService customerConsumerService)
        {
            _customerConsumerService = customerConsumerService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var customers = await _customerConsumerService.GetAllCustomers();
            return Ok(customers);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var customer = await _customerConsumerService.GetCustomer(id);
            return Ok(customer);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(CustomerInput customerToInput)
        {
            var customer = await _customerConsumerService.CreateCustomer(customerToInput);
            return Ok(customer);
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update(CustomerInput customerToInput, long id)
        {
            var customer = await _customerConsumerService.UpdateCustomer(id, customerToInput);
            return Ok(customer);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var result = await _customerConsumerService.DeleteCustomer(id);
            return Ok(result);
        }
    }
}
