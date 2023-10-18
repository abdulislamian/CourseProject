using AutoMapper;
using Courseproject.Common.Interfaces;
using Courseproject.Common.Model;
using CourseProject.Buisness.Exceptions;
using CourseProject.Buisness.Validation;
using CourseProject.Common.Dtos.Address;
using CourseProject.Common.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace CourseProject.Buisness.Services
{
    public class AddressService : IAddressService
    {
        private readonly IMapper mapper;
        private readonly AddressCreateValidator createValidator;
        private readonly AddressUpdateValidator updateValidator;

        public AddressService(IMapper mapper,IGenericRepository<Address> addressRepository,AddressCreateValidator _CreateValidator, AddressUpdateValidator _updateValidator)
        {
            this.mapper = mapper;
            AddressRepository = addressRepository;
            createValidator = _CreateValidator;
            updateValidator = _updateValidator;
        }

        public IGenericRepository<Address> AddressRepository { get; }

        public async Task<int> CreateAddressAsync(AddressCreate addressCreate)
        {
            await createValidator.ValidateAndThrowAsync(addressCreate);

            var entity = mapper.Map<Address>(addressCreate);
            await AddressRepository.InsertAsync(entity);
            await AddressRepository.SaveChangesAsync();
            return entity.Id;
        }

        public async Task DeleteAddressAsync(AddressDelete addressDelete)
        {
            var entity = await AddressRepository.GetByIdAsync(addressDelete.Id,(address)=>address.Employees);

            if(entity == null)
                throw new AddressNotFoundException(addressDelete.Id);

            if (entity.Employees.Count > 0)
                throw new DependentEmployeesExistException(entity.Employees); 

            AddressRepository.Delete(entity);
            await AddressRepository.SaveChangesAsync();
        }

        public async Task<AddressGet> GetAddressAsync(int id)
        {
            var entity = await AddressRepository.GetByIdAsync(id);
            return mapper.Map<AddressGet>(entity);
        }

        public async Task<List<AddressGet>> GetAddressesAsync()
        {
            var entities = await AddressRepository.GetAsync(null,null);
            return mapper.Map<List<AddressGet>>(entities);
        }

        public async Task UpdateAddressAsync(AddressUpdate addressUpdate)
        {
            await updateValidator.ValidateAndThrowAsync(addressUpdate);

            var existingEntity = await AddressRepository.GetByIdAsync(addressUpdate.Id);
            if(existingEntity == null)
                throw new AddressNotFoundException(addressUpdate.Id);

            var entity = mapper.Map<Address>(addressUpdate);
            AddressRepository.Update(entity);
            await AddressRepository.SaveChangesAsync();

        }
    }
}
