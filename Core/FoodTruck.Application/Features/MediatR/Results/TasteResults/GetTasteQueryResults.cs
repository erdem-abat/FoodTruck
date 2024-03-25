using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTruck.Application.Features.MediatR.Results.TasteResults
{
    public class GetTasteQueryResults
    {
        public int TasteId { get; set; }
        public string Name { get; set; }
    }
}
