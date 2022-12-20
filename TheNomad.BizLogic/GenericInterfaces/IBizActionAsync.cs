using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace TheNomad.BizLogic.GenericInterfaces
{
    public interface IBizActionAsync<in TIn, TOut>
    {
        IImmutableList<ValidationResult> Errors { get; }
        bool HasErrors { get; }
        Task<TOut> ActionAsync(TIn dto);
    }
}
