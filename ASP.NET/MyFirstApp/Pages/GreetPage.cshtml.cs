using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyFirstApp.Pages;

public class GreetPage : PageModel
{
    public User currentUser { get; set; }

    public void OnGet()
    {
        currentUser = new User { Name = "ayman", Age = 99};
    }
}

public class User
{
    public string Name { get; set; }
    public int Age { get; set; }
}