using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;
using TheNomad.BizLogic.GenericInterfaces;
using TheNomad.EFCore.Data.EfCode;

namespace TheNomad.EFCore.Services.BizRunners
{
    public class RunnerWriteDbAsync<TIn, TOut>
    {
        private readonly IBizActionAsync<TIn, TOut> _actionClass;
        private readonly AppDbContext _context;

        public IImmutableList<ValidationResult> Errors => _actionClass.Errors;
        public bool HasErrors => _actionClass.HasErrors;

        public RunnerWriteDbAsync(IBizActionAsync<TIn, TOut> actionClass, AppDbContext context)
        {
            _actionClass = actionClass;
            _context = context;
        }

        public async Task<TOut> RunActionAsync(TIn dataIn)
        {
            var result = await _actionClass.ActionAsync(dataIn).ConfigureAwait(false);
            if (!HasErrors)
                await _context.SaveChangesAsync();

            return result;
        }
    }
}
