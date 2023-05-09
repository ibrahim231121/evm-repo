using ALPR.Database;
using Crossbones.Modules.Business.Common;
using Crossbones.Transport.Pipes;
using Microsoft.EntityFrameworkCore;

namespace Corssbones.ALPR.Business
{
    public class ALPRContextCreator : IDbContextCreator
    {
        public Func<DbContext> Creator(string cnnStr, IMessageChannel channel, string tenantServiceId)
        {
            var t = cnnStr.Split(';', StringSplitOptions.RemoveEmptyEntries)
               .Select(x => x.Split('='))
               .Select(x => new KeyValuePair<string, string>(x[0].ToLowerInvariant(), x[1]))
               .ToDictionary(x => x.Key, x => x.Value);

            var key = "multipleactiveresultsets";
            if (!t.ContainsKey(key))
                t.Add(key, "true");

            var fixedStr = string.Join(';', t.Select(x => $"{x.Key}={x.Value}"));

            return new Func<DbContext>(() =>
            {
                var context = ALPRContextFactory.Create(cnnStr, channel, tenantServiceId);
                return context;
            });
        }
    }
}
