using System;
using Microsoft.Extensions.DependencyInjection;

namespace app1
{
  class ScopedClass
  {
    public string SomeValue { get; set; }
  }

  class Program
  {
    static void Main(string[] args)
    {
      var sc = new ServiceCollection();

      sc.AddScoped<ScopedClass>();


      var sp = sc.BuildServiceProvider();

      var scc1 = sp.GetService<ScopedClass>();
      var scc2 = sp.GetService<ScopedClass>();

      using (var scp = sp.CreateScope())
      {
        System.Console.WriteLine(object.ReferenceEquals(scc1, scc2));
      }
    }
  }
}
