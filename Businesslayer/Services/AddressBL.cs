using Businesslayer.Interfaces;
using Commonlayer.Model;
using Repositarylayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Businesslayer.Services
{
    public class AddressBL : IAddressBL
    {
        private readonly IAddressRL addressRL;
        public AddressBL(IAddressRL addressRL)
        {
            this.addressRL = addressRL;
        }
        public string AddAddress(AddressModel addressModel, int UserId)
        {
            try
            {
                return this.addressRL.AddAddress(addressModel, UserId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        

        public AddressModel UpdateAddress(AddressModel addressModel, int AddressId, int UserId)
        {

            try
            {
                return this.addressRL.UpdateAddress(addressModel, AddressId, UserId);
            }
            catch (Exception)
            {
                throw;
            }
       
        }

        public string DeleteAddress(int AddressId, int UserId)
        {
            try
            {
                return this.addressRL.DeleteAddress( AddressId, UserId);
            }
            catch (Exception)
            {
                throw;
            }

        }
    }

}
