using Cyber_Kitchen.Interface;
using Cyber_Kitchen.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace Cyber_Kitchen.Entities
{
    public class EntityRepository : IDataRepository
    {
        private readonly DbContext _dbContext;

        public EntityRepository(DbContext context)
        {
            _dbContext = context;
        }

        public IQueryable<T> Query<T>() where T : class
        {
            return _dbContext.Set<T>();
        }

        public IQueryable<T> Query<T>(params System.Linq.Expressions.Expression<Func<T, object>>[] includeProperties) where T : class
        {
            IQueryable<T> query = Query<T>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public T GetByID<T>(int id) where T : class
        {
            return _dbContext.Set<T>().Find(id);
        }

        public void Add<T>(T item) where T : class
        {
            _dbContext.Set<T>().Add(item);
        }

        public void Delete<T>(T item) where T : class
        {
            _dbContext.Set<T>().Remove(item);
        }

        public void Update<T>(T entity) where T : class
        {
            if (_dbContext.Entry(entity).State == EntityState.Detached)
            {
                _dbContext.Set<T>().Attach(entity);
            }
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public IEnumerable<T> Execute<T>(string sprocname, object args) where T : class
        {
            var argProperties = args.GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

            //Get SQL Parameters Using Reflection
            var parameters = argProperties.Select(propertyInfo => new System.Data.SqlClient.SqlParameter(
                                string.Format("@{0}", propertyInfo.Name),
                                propertyInfo.GetValue(args, new object[] { })))
                            .ToList();


            //Build SQL Query to Execute Query using Parameters
            string queryString = string.Format("{0}", sprocname);
            parameters.ForEach(x => queryString = string.Format("{0} {1},", queryString, x.ParameterName));
            string format = queryString.TrimEnd(',');

            //Finally Execute Query
            var sql = $"exec {format}";
            var ret = _dbContext.Database.SqlQuery<T>(sql, parameters.Cast<object>().ToArray());
            return ret;
        }

        public IEnumerable<T> Execute<T>(string sql) where T : class
        {
            return _dbContext.Database.SqlQuery<T>(sql);
        }

        public void Execute(string sql, object args)
        {
            var argProperties = args.GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

            //Get SQL Parameters Using Reflection
            var parameters = argProperties.Select(propertyInfo => new System.Data.SqlClient.SqlParameter(
                                string.Format("@{0}", propertyInfo.Name),
                                propertyInfo.GetValue(args, new object[] { })))
                            .ToList();
            //Finally Execute Query
            _dbContext.Database.ExecuteSqlCommand(sql, parameters.Cast<object>().ToArray());
        }

        public void ExecuteProcedure(string procName, object args)
        {
            var argProperties = args.GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

            //Get SQL Parameters Using Reflection
            var parameters = argProperties.Select(propertyInfo => new System.Data.SqlClient.SqlParameter(
                                string.Format("@{0}", propertyInfo.Name), propertyInfo.GetValue(args, new object[] { })))
                            .ToList();
            //Finally Execute Query

            var sql = $"exec {procName}";

            if (parameters.Any())
            {
                var argStrings = parameters.Select(p => p.ParameterName).Aggregate((ag, p) => ag + "," + p);
                sql = sql + $" {argStrings}";
            }

            _dbContext.Database.ExecuteSqlCommand(sql, parameters.Cast<object>().ToArray());
        }

        public void Execute(string sql)
        {
            _dbContext.Database.ExecuteSqlCommand(sql);
        }

        public Operation SaveChanges()
        {
            var operation = new Operation();
            try
            {
                _dbContext.SaveChanges();
                operation.Succeeded = true;
                operation.Message = "Changes were Saved Successfully";
            }
            catch (DbEntityValidationException dbe)
            {
                operation = new Operation(dbe);
                string message = "An Error occured Saving: ";
                foreach (var ex in dbe.EntityValidationErrors)
                {
                    //Aggregate Errors
                    string errors = ex.ValidationErrors.Select(e => e.ErrorMessage).Aggregate((ag, e) => ag + " " + e);
                    message += errors;
                }
                operation.Message = message;
                operation.Succeeded = false;
            }
            catch (DbUpdateException uex)
            {
                operation = new Operation(uex);
                Exception ex = uex;
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }

                var message = "Could not save: " + ex.Message;
                operation.Message = message;
                operation.Succeeded = false;
            }
            catch (Exception ex)
            {
                operation = new Operation(ex);
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }
                var message = "Could not save: " + ex.Message;
                operation.Message = message;
                operation.Succeeded = false;
            }
            return operation;
        }
    }
}