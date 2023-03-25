using PruebaTecnica.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using System.Web;

namespace PruebaTecnica.Logica
{
    public class LogicaEmployee
    {

        private readonly P1700Entities _DbConnection = null;
        /// <summary>
        /// 
        /// </summary>
        public LogicaEmployee()
        {
            _DbConnection = new P1700Entities();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<EEmployee> Mostrar()
        {
            try
            {
                List<EEmployee> Lista = new List<EEmployee>();
                var Objbd = _DbConnection.Employee;
                Lista = Objbd.Select(x => new EEmployee
                {
                    Id = x.Id,
                    Name = x.Name,
                    Type = x.Type,
                    Telephone = x.Telephone,
                    Address = x.Address,
                    EmploymentDate = x.EmploymentDate,
                }).ToList();

                return Lista.ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// Listar tipos de empleado
        /// </summary>
        /// <returns></returns>
        public List<EEmployeeType> ListTypeEmploye()
        {
            try
            {
                List<EEmployeeType> Lista = new List<EEmployeeType>();
                var Objbd = _DbConnection.EmployeeType;
                Lista = Objbd.Select(x => new EEmployeeType
                {
                    Id = x.Id,
                    Name = x.Name,
                    Salary = x.Salary
                }).ToList();

                return Lista.ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<bool> Crear(EEmployee Employe)
        {
            try
            {
                using (TransactionScope Ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var Employee = new Employee
                    {
                        Id = Employe.Id,
                        Name = Employe.Name,
                        Type = Employe.Type,
                        Telephone = Employe.Telephone,
                        Address = Employe.Address,
                        EmploymentDate = Employe.EmploymentDate
                    };
                    _DbConnection.Entry(Employee).State = EntityState.Added;
                    var Resultado = await _DbConnection.SaveChangesAsync();

                    if (Resultado == 1)
                    {
                        Ts.Complete();
                        return true;
                    }
                    else
                    {
                        Ts.Dispose();
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            throw new Exception("Ocurrió un error al procesar su solicitud");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Employe"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> Editar(EEmployee Employe)
        {
            try
            {
                using (TransactionScope Ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    Employee Existente = await _DbConnection.Employee.FirstOrDefaultAsync(x => x.Id == Employe.Id);

                    if (Existente != null)
                    {

                        Existente.Id = Employe.Id;
                        Existente.Name = Employe.Name;
                        Existente.Type = Employe.Type;
                        Existente.Telephone = Employe.Telephone;
                        Existente.Address = Employe.Address;
                        Existente.EmploymentDate = Employe.EmploymentDate;

                        _DbConnection.Entry(Existente).State = EntityState.Modified;
                        var Resultado = await _DbConnection.SaveChangesAsync();

                        if (Resultado == 1)
                        {
                            Ts.Complete();
                            return true;
                        }
                        else
                        {
                            Ts.Dispose();
                            return false;
                        }
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            throw new Exception("Ocurrió un error al procesar su solicitud");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Employe"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> Eliminar(int Employe)
        {
            try
            {
                using (TransactionScope Ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var Tarea = Task.Run(() =>
                    {
                        var TareaModelo = _DbConnection.Employee.Where(x => x.Id == Employe).FirstOrDefault();

                        _DbConnection.Entry(TareaModelo).State = EntityState.Deleted;

                        var TareaResultado = _DbConnection.SaveChanges();

                        if (TareaResultado >= 1)
                        {
                            Ts.Complete();
                            return true;
                        }
                        Ts.Dispose();
                        return false;
                    });
                    return await Tarea;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            throw new Exception("Ocurrió un error al procesar su solicitud");
        }
    }
}