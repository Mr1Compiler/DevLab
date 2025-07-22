using ErrorHandling.Error;

string email = "aagmail.com";

if (!email.Contains("@"))
{
    throw new InvalidEmailException("Invalid Email Address");
}