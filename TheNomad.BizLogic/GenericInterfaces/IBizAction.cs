using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TheNomad.BizLogic.GenericInterfaces
{
    public interface IBizAction<in TIn, out TOut> //#A
    {
        IImmutableList<ValidationResult> Errors { get; }      //#B
        bool HasErrors { get; }  //#B
        TOut Action(TIn dto);                  //#C
    }
    /****************************************************
    #A The BizAction has both and TIn and an TOut
    #B This returns the error information from the business logic
    #C This is the action that the BizRunner will call
     * *************************************************/

}
