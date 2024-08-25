using Planist.Models;

namespace Planist.Exceptions
{
    public class PlanistResultException : Exception
    {
        public PlanistResult Result { get; }
        public PlanistResultException(string message) : base(message)
        {
            Result = new(false, message);
        }
    }
}
