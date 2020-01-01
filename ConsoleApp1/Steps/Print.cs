using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace ConsoleApp1
{
    public class Print : StepBody
    {
        public Print()
        {

        }
        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Console.WriteLine("Print Completed.");
            return ExecutionResult.Next();
        }
    }
}
