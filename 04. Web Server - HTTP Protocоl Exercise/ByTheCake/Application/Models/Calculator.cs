namespace WebServer.Application.Models
{
    public static class Calculator
    {
	    public static string Result;

	    public static void Validate(decimal firstNumber, decimal secondNumber, string sign)
	    {
		    if(sign != "-" || sign != "+" || sign != "/" || sign != "*"  )
		    {
			    Result= "Invalid sign.";
		    }

		    if (sign == "+")
		    {
				Result = $"Result: {firstNumber + secondNumber:f2}";
		    }
			else if (sign == "-")
		    {
			    Result = $"Result: {firstNumber - secondNumber:f2}";
			}
		    else if (sign == "/")
		    {
				Result = $"Result: {firstNumber / secondNumber:f2}";
			}
		    else if (sign == "*")
		    {
				Result = $"Result: {firstNumber * secondNumber:f2}";
			}
	    }
    }
}