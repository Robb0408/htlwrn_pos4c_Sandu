using System;
// HAU: ℹ️ remove not used usings
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// HAU: ℹ️ use file scoped namespace (single line) declaration to reduce code nesting
namespace FitnessTracker.App.Models
{
    public class Activity
    {
        public string Sport { get; set; } = string.Empty;
        public DateTime Start { get; set; }
        public TimeSpan Duration { get; set; }
        public double Energy { get; set; }
    }
}
