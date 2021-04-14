using Microsoft.Extensions.Configuration;
using System;

namespace Pograminha.Infra.SQL.Migrations
{
    public class Program
    {
        protected Program()
        {

        }

        public static IConfiguration Configuration { get; set; }
    }
}
