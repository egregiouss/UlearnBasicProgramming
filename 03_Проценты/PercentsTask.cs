public static double Calculate(string userInput)
{
    string[] numbers = userInput.Split();
    double money = double.Parse(numbers[0], CultureInfo.InvariantCulture);
    double persent = double.Parse(numbers[1]) * 0.01 / 12;
    int months = int.Parse(numbers[2]);
    double sumDeposit = money * Math.Pow(1 + persent, months);
    return sumDeposit;
}
