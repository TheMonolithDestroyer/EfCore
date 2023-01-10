using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheNomad.BizLogic.GenericInterfaces;
using TheNomad.EFCore.Data;
using TheNomad.EFCore.Data.EfCode;

namespace TheNomad.EFCore.Services.BizRunners
{
    public class RunnerWriteDbWithValidationAsync<TIn, TOut>
    {
        private readonly IBizActionAsync<TIn, TOut> _actionClass;
        private readonly AppDbContext _context;

        public IImmutableList<ValidationResult> Errors { get; private set; }
        public bool HasErrors => Errors.Any();

        public RunnerWriteDbWithValidationAsync(IBizActionAsync<TIn, TOut> actionClass, AppDbContext context)
        {
            _actionClass = actionClass;
            _context = context;
        }

        public async Task<TOut> RunActionAsync(TIn dataIn)
        {
            var result = await _actionClass.ActionAsync(dataIn).ConfigureAwait(false);

            Errors = _actionClass.Errors;
            if (!HasErrors)
            {
                Errors = (await _context.SaveChangesWithValidationAsync().ConfigureAwait(false)).ToImmutableList();
            }
            return result;
        }
    }
}
