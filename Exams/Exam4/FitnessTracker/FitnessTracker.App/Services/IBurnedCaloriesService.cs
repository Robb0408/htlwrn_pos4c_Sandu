using System;
// HAU: ℹ️ remove not used usings
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// HAU: ℹ️ use file scoped namespace (single line) declaration to reduce code nesting
namespace FitnessTracker.App.Services
{
    public interface IBurnedCaloriesService
    {
        double GetBurnedCalories(string sport, TimeSpan duration);
    }
}
