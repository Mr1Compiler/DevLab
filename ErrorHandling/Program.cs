using ErrorHandling.Error.Entities;
using FluentResults;

var validUser = new User { Id = "1", Name = "ayman", Email = "Hi@gmail.com" };
var InvalidUser = new User { Id = "", Name = "", Email = "Higmail.com" };

Result CreateUser(User user)
{
    var result = new Result();

    if (!int.TryParse(user.Id, out int number))
        result.WithError("you cannot enter this id");

    if (string.IsNullOrEmpty(user.Name))
        result.WithError("Cannot enter empty name");

    if (!user.Email.Contains("@"))
        result.WithError("Not Valid Email");

    if (result.IsFailed)
        return result;

    return result.IsFailed ? result : Result.Ok();
}

string check(User user)
{
    var result = CreateUser(user);

    if (result.IsFailed)
        return string.Join(" | ", result.Errors.Select(e => e.Message)); 
    
    else
        return "Great!";
}

Console.WriteLine(check(validUser));
Console.WriteLine(check(InvalidUser));

// try
// {
//     string email = "aagmail.com";
//     
//     if (!email.Contains("@"))
//         throw new InvalidEmailException("This Is Invalid Email Address");
//     else
//         Console.WriteLine("Email Valid");
// }
// catch (Exception e)
// {
//     Console.WriteLine($"Error : {e.Message}");
// }
