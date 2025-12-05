using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Models
{
    public class APIResponse
    {
        public bool Status {  get; set; }
        public HttpStatusCode StatusCode {  get; set; }
        public dynamic data {  get; set; }
        public List<string> Error { get; set; } = new List<string>();
    }
}
