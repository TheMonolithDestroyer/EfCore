using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TheNomad.BizLogic.GenericInterfaces;
using TheNomad.EFCore.Data.EfCode;

namespace TheNomad.EFCore.Services.BizRunners
{
    public class RunnerWriteDb<TIn, TOut>
    {
        private readonly IBizAction<TIn, TOut> _actionClass;
        private readonly AppDbContext _context;
        public IImmutableList<ValidationResult> Errors => _actionClass.Errors;              //#A
        public bool HasErrors => _actionClass.HasErrors;//#A

        public RunnerWriteDb(                 //#B
            IBizAction<TIn, TOut> actionClass,//#B
            AppDbContext context)            //#B
        {
            _context = context;
            _actionClass = actionClass;
        }

        public TOut RunAction(TIn dataIn) //#C
        {
            var result = _actionClass.Action(dataIn); //#D
            if (!HasErrors)            //#E
                _context.SaveChanges();//#E
            return result; //#F
        }

        /*********************************************************
        #A The error information from the business logic is passed back to the user of the BizRunner
        #B This BizRunner handles business logic that conforms to the IBizAction<TIn, TOut> interface
        #C I call RunAction in my Service Layer, or in my Presentation Layer if the data comes back in the right form
        #D It runs the business logic I gave it
        #E If there aren't any errors it calls SaveChanges to execute any add, update or delete methods
        #F Finally it returns the result that the business logic returned
         * ******************************************************/
    }
}
