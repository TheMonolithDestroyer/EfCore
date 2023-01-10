using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using TheNomad.BizLogic.GenericInterfaces;
using TheNomad.EFCore.Data;
using TheNomad.EFCore.Data.EfCode;

namespace TheNomad.EFCore.Services.BizRunners
{
    public class RunnerWriteDbWithValidation<TIn, TOut>
    {
        private readonly IBizAction<TIn, TOut> _actionClass;
        private readonly AppDbContext _context;
        public IImmutableList<ValidationResult> Errors { get; private set; }
        public bool HasErrors => Errors.Any();

        public RunnerWriteDbWithValidation(
            IBizAction<TIn, TOut> actionClass,
            AppDbContext context)
        {
            _context = context;
            _actionClass = actionClass;
        }

        public TOut RunAction(TIn dataIn)
        {
            var result = _actionClass.Action(dataIn);
            Errors = _actionClass.Errors;
            if (!HasErrors)
            {
                Errors = _context.SaveChangesWithValidation().ToImmutableList();
            }

            return result;
        }
    }
}
