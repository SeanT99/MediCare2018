using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

/// <summary>
/// Summary description for PasswordValidator
/// </summary>
public class PasswordValidator
{
    int FewestUppercaseCharactersAllowed = 1;
    int MinimumLength = 6;
    int MaximiumLength = 16;

    private Regex uppercaseCharacterMatcher = new Regex("[A-Z]");
    private Regex digitsMatcher = new Regex("[a-z]");

    public PasswordValidator()
    {

    }

    public int FewestUppercaseCharactersAllowed1 { get => FewestUppercaseCharactersAllowed; set => FewestUppercaseCharactersAllowed = value; }
    public int MinimumLength1 { get => MinimumLength; set => MinimumLength = value; }
    public int MaximiumLength1 { get => MaximiumLength; set => MaximiumLength = value; }

    public bool IsValid(string password)
    {
        return password.Length > MinimumLength //Password more than 6
            && password.Length < MaximiumLength // Password less then 16
        && uppercaseCharacterMatcher.Matches(password).Count >= FewestUppercaseCharactersAllowed // Must have 1 Uppercase
        && password.All(x => char.IsLetterOrDigit(x) || char.IsWhiteSpace(x)); //Password Alphanumeric only

    }


}
