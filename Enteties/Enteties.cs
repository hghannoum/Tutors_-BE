using System;
using System.Data;
namespace Enteties
{
	
	public class User
	{
		public string pass { get; set; }
		public string username { get; set; }
		public string fullname { get; set; }
		public bool tutor { get; set; }
		public string img { get; set; }
		public string country { get; set; }
		public string bio { get; set; }
		public string cat { get; set; }
		public string email { get; set; }
		public string mat { get; set; }
		public string way { get; set; }

	}

		public class filterObj
	{
		public string [] arr { get; set; }
		public DataTable dt { get; set; }

	}
	public class Request
	{
		public string f { get; set; }
		public string t { get; set; }
		public bool acccepted { get; set; }

	}
	public class data {
		public string email { get; set; }
	}
	public class schedule{
		
		public string emailto { get; set; }
		public string emailfrom{ get; set; }
		public string end { get; set; }
		public string start{ get; set; }
		public string descp{ get; set; }
		public string title { get; set; }


	}
	public class User2
	{
		public string pass { get; set; }
		public string username { get; set; }
	}
	public class validation
	{
		public string email { get; set; }
		public DataTable data { get; set; }
		public string password { get; set; }
	}

	public class response
    {
		public string error { get; set; }
		public DataTable dt { get; set; }
		public object [] row { get; set; }
	}
}
