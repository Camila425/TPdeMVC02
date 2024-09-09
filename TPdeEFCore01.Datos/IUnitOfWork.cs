﻿namespace TPdeEFCore01.Datos
{
    public interface IUnitOfWork
    {
        void BeginTransaction();
        void Commit();
        void Rollback();
        int SaveChanges();
    }
}