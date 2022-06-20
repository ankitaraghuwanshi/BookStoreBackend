using Commonlayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositarylayer.Interfaces
{
    public interface IAddressRL
    {
        public string AddAddress(AddressModel addressModel, int UserId);

        public AddressModel UpdateAddress(AddressModel addressModel, int AddressId, int UserId);

        public string DeleteAddress(int AddressId, int UserId);
    }
}
