using Courseproject.Common.Interfaces;
using CourseProject.Common.Dtos.Address;
using Microsoft.AspNetCore.Mvc;

namespace CourseProject.API.Controllers
{
    public class AddressController : Controller
    {
        private readonly IAddressService addressService;

        public AddressController(IAddressService _addressService)
        {
            addressService = _addressService;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateAddress(AddressCreate addressCreate)
        {
            var id = await addressService.CreateAddressAsync(addressCreate);
            return Ok(id);
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> UpdateAddress(AddressUpdate obj)
        {
            await addressService.UpdateAddressAsync(obj);
            return Ok();
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(AddressDelete delete)
        {
            await addressService.DeleteAddressAsync(delete);
            return Ok();
        }

        [HttpGet]
        [Route("Get/{id}")]
        public async Task<IActionResult> GedAddress(int id)
        {
            //throw new Exception("Test");
            var address = await addressService.GetAddressAsync(id);
            return Ok(address);
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> GetAddresses()
        {
           var addresses =  await addressService.GetAddressesAsync();
            return Ok(addresses);
        }
    }
}
