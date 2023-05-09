using ALPR.Database.Entities;
using Crossbones.Transport.Pipes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ALPR.Database
{
    public class ALPRContextFactory
    {
        public static AlprContext Create(string cnnStr, IMessageChannel messageChannel, string tenantServiceId)
        {
            var builder = new DbContextOptionsBuilder<AlprContext>();
            builder.UseSqlServer(cnnStr!);
            var ctx = new AlprContext(builder.Options, messageChannel, tenantServiceId);
            ctx.Database?.OpenConnection();

            return ctx;
        }

        public static void Release(DbContext ctx)
        {
            if (ctx is AlprContext fctx)
            {
                ctx.Database!.CloseConnection();
                fctx.DisposeAsync();
            }
        }
    }
}