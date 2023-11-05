using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Consultorio_Seguros.Data;
using Consultorio_Seguros.Models;
using Consultorio_Seguros.DAL;
using Consultorio_Seguros.Process;

namespace Consultorio_Seguros.Controllers
{
    public class AseguradosController : Controller
    {
        private readonly Asegurado_DAL _dal;
        private readonly Cliente_DAL _dal_cliente;
        private readonly Seguro_DAL _dal_seguro;

        public AseguradosController(Asegurado_DAL dal, Cliente_DAL _dalcliente, Seguro_DAL _dalseguro)
        {
            _dal = dal;
            _dal_cliente = _dalcliente;
            _dal_seguro = _dalseguro;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<AseguradoVM> aseguradosVM = new List<AseguradoVM>();
            try
            {
                aseguradosVM = _dal.GetAll();
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
            }
            return View(aseguradosVM);
        }

            // GET: Asegurados/Create
        public IActionResult Create()
        {
            List<Cliente> listCliente = new List<Cliente>();
            List<Seguro> listSeguro = new List<Seguro>();

            listCliente = _dal_cliente.GetAll();
            listSeguro = _dal_seguro.GetAll();

            ViewBag.Clientes = listCliente.ToList();
            ViewBag.Seguros = listSeguro.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Asegurado seguro)
        {
            List<Cliente> listCliente = new List<Cliente>();
            List<Seguro> listSeguro = new List<Seguro>();

            listCliente = _dal_cliente.GetAll();
            listSeguro = _dal_seguro.GetAll();

            ViewBag.Clientes = listCliente.ToList();
            ViewBag.Seguros = listSeguro.ToList();

            try
            {                
                bool result = _dal.Insert(seguro);

                if (!result)
                {
                    TempData["errorMessage"] = "Este Cliente ya tiene este Seguro en la base de datos.";
                    return View();
                }

                TempData["successMessage"] = "Seguro agregado correctamente.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            List<Cliente> listCliente = new List<Cliente>();
            List<Seguro> listSeguro = new List<Seguro>();

            listCliente = _dal_cliente.GetAll();
            listSeguro = _dal_seguro.GetAll();

            ViewBag.Clientes = listCliente.ToList();
            ViewBag.Seguros = listSeguro.ToList();

            try
            {
                Asegurado asegurado = _dal.GetById(id);
                if (asegurado.Id == 0)
                {
                    TempData["errorMessage"] = $"No existe un Asegurado con la Id : {id}";
                    return RedirectToAction("Index");
                }

                return View(asegurado);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        [HttpPost]
        public IActionResult Edit(Asegurado model)
        {
            List<Cliente> listCliente = new List<Cliente>();
            List<Seguro> listSeguro = new List<Seguro>();

            listCliente = _dal_cliente.GetAll();
            listSeguro = _dal_seguro.GetAll();

            ViewBag.Clientes = listCliente.ToList();
            ViewBag.Seguros = listSeguro.ToList();

            try
            {                
                bool result = _dal.Update(model);

                if (!result)
                {
                    TempData["errorMessage"] = "Error al actualizar los datos del asegurado";
                    return View();
                }

                TempData["successMessage"] = "Los datos del Asegurado han sido actualizados";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        // GET: Asegurados/Delete/5
        [HttpGet]
        public IActionResult Delete(int id)
        {
            List<Cliente> listCliente = new List<Cliente>();
            List<Seguro> listSeguro = new List<Seguro>();

            listCliente = _dal_cliente.GetAll();
            listSeguro = _dal_seguro.GetAll();

            ViewBag.Clientes = listCliente.ToList();
            ViewBag.Seguros = listSeguro.ToList();

            try
            {
                Asegurado asegurado = _dal.GetById(id);
                if (asegurado.Id == 0)
                {
                    TempData["errorMessage"] = $"No existe un Asegurado con la Id : {id}";
                    return RedirectToAction("Index");
                }

                return View(asegurado);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        // POST: Asegurados/Delete/5
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(Asegurado model)
        {
            try
            {
                bool result = _dal.Delete(model.Id);

                if (!result)
                {
                    TempData["errorMessage"] = "Error al eliminar los datos del asegurado";
                    return View();
                }

                TempData["successMessage"] = "Los datos del asegurado han sido eliminados";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }
    }
}