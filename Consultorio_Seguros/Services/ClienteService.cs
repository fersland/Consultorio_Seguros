using Consultorio_Seguros.Models;
using Consultorio_Seguros.Process;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace Consultorio_Seguros.Services
{
    public class ClienteService
    {
        private readonly Cliente_DAL _dal;

        public ClienteService(Cliente_DAL dal)
        {
            this._dal = dal;
        }

        public IEnumerable<Cliente> GetAllService()
        {
            List<Cliente> clientes = new List<Cliente>();
            clientes = _dal.GetAll();

            return clientes;
        }

        public Cliente GetByIdService(int id)
        {
            return _dal.GetById(id);
        }

        public Cliente? Create(Cliente cliente)
        {
            if(cliente == null)
            {
                return null;
            }
            else
            {
                _dal.Insert(cliente);
                return cliente;
            }
        }

        public void UpdateService(Cliente cliente)
        {
            var existe = _dal.GetById(cliente.Id);
            
            if(existe != null)
            {
                existe.Cedula = cliente.Cedula;
                existe.Nombre = cliente.Nombre;
                existe.Telefono = cliente.Telefono;
                existe.Edad = cliente.Edad;

                _dal.Update(existe);
            }
        }

        public void Delete(int id)
        {
            var existe = GetByIdService(id);

            if(existe is not null)
            {
                _dal.Delete(id);
            }
        }

    }
}
