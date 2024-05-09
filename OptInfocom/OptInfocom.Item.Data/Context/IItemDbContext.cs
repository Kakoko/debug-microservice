using Microsoft.EntityFrameworkCore;
using OptInfocom.Item.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptInfocom.Item.Data.Context
{
    public interface IItemDbContext
    {
        DbSet<ItemMaster> DbItemMaster { get; set; }
    }
}
