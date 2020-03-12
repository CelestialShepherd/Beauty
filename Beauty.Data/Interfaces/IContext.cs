﻿using Beauty.Data.Models;
using System.Data.Entity;

namespace Beauty.Data.Interfaces
{
    /// <summary>
    /// Интерфейс контекста базы данных
    /// </summary>
    public interface IContext
    {
        /// <summary>
        /// Набор сущностей записей
        /// </summary>
        DbSet<Enrollment> Enrollments { get; set; }
        
        /// <summary>
        /// Набор сущностей должностей
        /// </summary>
        DbSet<Position> Positions { get; set; }

        /// <summary>
        /// Набор сущностей сотрудников
        /// </summary>
        DbSet<Worker> Workers { get; set; }

        /// <summary>
        /// Набор сущностей должностей, связанных с сотрудниками
        /// </summary>
        DbSet<WorkerPosition> WorkerPositions { get; set; }
    }
}
