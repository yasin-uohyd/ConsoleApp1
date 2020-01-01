using System;
using System.Collections.Generic;
using System.Text;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace ConsoleApp1
{
    public class EscalateCorrectiveAction : StepBody
    {
        public EscalateCorrectiveAction()
        {

        }

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            //Increase the escalation level.
            //Start the new workflow (if context.Data.EscalationLevel<5 then start 
            //termitate the current workflow

            return ExecutionResult.Next();
        }
    }
}
