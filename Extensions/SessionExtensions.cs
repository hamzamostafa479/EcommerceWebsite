using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Text.Json;

namespace Ecommerce_Project.Extensions
{
  public static class SessionExtensions
  {
    public static void SetObject<T>(this ISession session, string key, T value)
    {
      var json = JsonSerializer.Serialize(value);
      session.SetString(key, json);
    }

    public static T? GetObject<T>(this ISession session, string key)
    {
      var json = session.GetString(key);
      if(string.IsNullOrEmpty(json))
      {
        return default;
      }
      return JsonSerializer.Deserialize<T>(json);

    }


  }
}
