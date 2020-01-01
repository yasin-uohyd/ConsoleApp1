using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace ConsoleApp1
{
    public class Notify : StepBody
    {
        public Notify()
        {

        }
        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Console.WriteLine("Notify Completed.");
            //set last notification sent on to DateTime.Now

            return ExecutionResult.Next();
        }
    }
}
