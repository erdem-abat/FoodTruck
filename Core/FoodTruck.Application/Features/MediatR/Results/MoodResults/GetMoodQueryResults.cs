using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTruck.Application.Features.MediatR.Results.MoodResults
{
    public class GetMoodQueryResults
    {
        public int MoodId { get; set; }
        public string Name { get; set; }
    }
}
