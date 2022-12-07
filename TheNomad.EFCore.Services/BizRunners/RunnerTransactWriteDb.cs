using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using TheNomad.BizLogic.GenericInterfaces;
using TheNomad.EFCore.Data;

namespace TheNomad.EFCore.Services.BizRunners
{
    public class RunnerTransactWriteDb<TIn, TPass, TOut> //#A
        where TPass : class //#B
        where TOut : class //#B
    {
        private readonly IBizAction<TIn, TPass> _actionPart1; //#C
        private readonly IBizAction<TPass, TOut> _actionPart2; //#C
        private readonly AppDbContext _context;

        public IImmutableList<ValidationResult> Errors { get; private set; } //#D
        public bool HasErrors => Errors.Any(); //#D

        public RunnerTransactWriteDb(
            AppDbContext context, //#E
            IBizAction<TIn, TPass> actionPart1, //#E
            IBizAction<TPass, TOut> actionPart2) //#E
        {
            _context = context;
            _actionPart1 = actionPart1;
            _actionPart2 = actionPart2;
        }

        public TOut RunAction(TIn dataIn)
        {
            using var transaction = _context.Database.BeginTransaction(); //#F
            var passResult = RunPart(_actionPart1, dataIn); //#G

            if (HasErrors) //#H
                return null;

            var result = RunPart(_actionPart2, passResult); //#I

            if (!HasErrors) //#J
            {
                transaction.Commit();
            }
            return result; //#K
        }

        private TPartOut RunPart<TPartIn, TPartOut>(IBizAction<TPartIn, TPartOut> bizPart, TPartIn dataIn) //#L
            where TPartOut : class
        {
            var result = bizPart.Action(dataIn); //#M
            Errors = bizPart.Errors; //#M
            if (!HasErrors)
            {
                _context.SaveChanges(); //#N
            }
            return result; //#O
        }

        //#A Generic RunnerTransactWriteDb takes three types: the initial input, the class passed from Part1 to Part2, and the final output.
        //#B Because the BizRunner returns null if an error occurs, you have to say that the TOut type must be a class.
        //#C Defines the generic BizAction for the two business logic parts.
        //#D Holds the error information returned from the last business logic code that run.
        //#E Takes the two instances of the business logic, and the application DbContext that the business logic is using.
        //#F You start the transaction on the application’s DbContext within a using statement.
        //  When it exits the using statement, unless Commit has been called, it’ll RollBack any changes.
        //#G You use a private method, RunPart, to run the first business part
        //#H If errors exist, you return null (the rollback is handled by the dispose of the transection).
        //#I Because the first part of the business logic was successful, you run the second part of the business logic.
        //#J If no errors occur, you commit the transaction to the database.
        //#K Returns the result from the last business logic
        //#L A private method that handles running each part of the business logic.
        //#M Runs the business logic and copies the business logic’s Errors property to the local Errors property
        //#N If the business logic was successful, you call SaveChanges to apply any add/update/delete commands to the transaction.
        //#O Returns the result that the business logic returned
    }
}
