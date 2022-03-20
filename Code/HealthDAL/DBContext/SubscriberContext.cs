using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthDAL
{
    public class SubscriberContext : DbContext
    {
        public SubscriberContext()
        {

        }

        public SubscriberContext(DbContextOptions<SubscriberContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<SubscriberDAL> Subscribers { get; set; }
    }
}
