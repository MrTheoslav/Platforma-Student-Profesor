using Microsoft.AspNetCore.Http;
using MODEL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Helper
{
    public class TransferFile
    {
        public UserAssigmnentDTO UserAssigmnentDTO { get; set; }
        public IFormFile file { get; set; }
    }
}
