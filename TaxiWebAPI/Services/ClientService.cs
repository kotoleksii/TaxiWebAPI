using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TaxiWebAPI.Models.DTO;
using TaxiWebAPI.Models.DAL;
using TaxiWebAPI.Models.Entities;
using Mapster;

namespace TaxiWebAPI.Services
{
    public class ClientService
    {
        public async Task<ClientShortDTO> RegisterClient(AddClientDTO newClient)
        {
            Client clientByPhoneNumber = await _DAL.Clients.ByPhoneNumber(newClient.PhoneNumber);

            Client registeredClient = new Client();

            if (clientByPhoneNumber == null)
            {
                Client clientToRegister = newClient.Adapt<Client>();

                registeredClient = await _DAL.Clients.Register(clientToRegister);
            }

            return registeredClient.Adapt<ClientShortDTO>();
        }

        internal async Task<List<ClientShortDTO>> GetAll()
        {
            List<Client> allClients = await _DAL.Clients.All();

            return allClients.Adapt<List<ClientShortDTO>>();
        }

        public async Task<ClientShortDTO> GetById(int id)
        {
            Client clientById = await _DAL.Clients.ById(id);

            if (clientById == null)
                clientById = new Client();

            return clientById.Adapt<ClientShortDTO>();
        }

        public async Task<bool> UpdateClient(EditClientDTO clientToUpdate)
        {
            Client clientToEdit = clientToUpdate.Adapt<Client>();

            bool isClientUpdated = await _DAL.Clients.Update(clientToEdit);

            return isClientUpdated;
        }

        public async Task<bool> DeleteClientById(int id)
        {
            bool isClientDeleted = await _DAL.Clients.DeleteById(id);

            return isClientDeleted;
        }
    }
}