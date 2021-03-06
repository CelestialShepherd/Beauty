﻿using Beauty.Data.Models;
using Beauty.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beauty.Data.Interfaces
{
    public interface IWorkerRepository : IRepository<Worker>
    {
        Task<ICollection<Position>> FindPositionsAsync(int? workerId);
        Task<ICollection<Worker>> FindAdministratorsAsync();
    }
}
