using Demo.API.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using static Demo.API.Helper.SD;

namespace Demo.DAL
{
	public class APIRequest
	{
		public ApiType ApiType { get; set; } = ApiType.GET;
		public string Url { get; set; }
		public object Data { get; set; }
		public string Token { get; set; }
	}
}
