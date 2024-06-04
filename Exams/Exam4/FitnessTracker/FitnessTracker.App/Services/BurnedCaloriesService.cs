using System;
// HAU: ℹ️ remove not used usings
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// HAU: ℹ️ use file scoped namespace (single line) declaration to reduce code nesting
namespace FitnessTracker.App.Services
{
    public class BurnedCaloriesService : IBurnedCaloriesService
    {
        public double GetBurnedCalories(string sport, TimeSpan duration)
        { 
            if (sport.ToLower() == "running") // HAU: 👍🏻
            {
                return duration.TotalSeconds * 400 / (30 * 60);
            }
            return duration.TotalSeconds * 350 / (30 * 60);
        }
    }
}
