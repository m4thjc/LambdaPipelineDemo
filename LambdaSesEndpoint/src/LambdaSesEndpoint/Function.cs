using Amazon.Lambda.Core;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace LambdaSesEndpoint;

public class Function
{
    
    /// <summary>
    /// A simple function that takes a string and does a ToUpper
    /// </summary>
    /// <param name="input"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public string FunctionHandler(string input, ILambdaContext context)
    {
        context.Logger.LogInformation("Function called");

        var usernameAndPassword = input.Split(',');
        var username = usernameAndPassword[0];
        var password = usernameAndPassword[1];


        //Create SMTP client
        var emailSender = new EmailService(context, username, password, 587, "email-smtp.us-east-1.amazonaws.com");
        emailSender.Send(context, "john.mathias3@gmail.com", "Test subject", "Test body", "johnmathias3@gmail.com");
        return input.ToUpper();
    }
}
