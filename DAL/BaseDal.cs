﻿using IDAL;
using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class BaseDal<T>:IBaseDal<T> where T : class ,new()
    {
        public DbContext dbContext
        {
            get
            {
                return DbContextFactory.GetDbContext();
            }
        }

        //public NFineBaseEntities Db { get; set; } //通过Autofac 单例模式自动注入

        public bool AddEntity(T entity)
        {            
            dbContext.Set<T>().Add(entity);
            if (dbContext.SaveChanges() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #region 查询
        public IQueryable<T> GetEntitys(Expression<Func<T, bool>> whereLambda)
        {
            return dbContext.Set<T>().Where(whereLambda).AsNoTracking();
        }

        /// <summary>
        /// 修改登录日志信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool UpdateEntityInfo(T entity)
        {
            dbContext.Entry(entity).State =EntityState.Modified;
            if (dbContext.SaveChanges() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="paramters"></param>
        /// <returns></returns>
        public bool ExceptionSql(string sql,object[]paramters)
        {
           int i= dbContext.Database.ExecuteSqlCommand(sql, paramters);
            
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool AddEntitys(IEnumerable<T> entitys)
        {
            dbContext.Set<T>().AddRange(entitys);
            return dbContext.SaveChanges() > 0;
        }
    }
}
