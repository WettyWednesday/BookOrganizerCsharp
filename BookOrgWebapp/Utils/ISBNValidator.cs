public static class ISBNValidator
{
    public static bool IsValidISBN(string isbn)
    {
        if (string.IsNullOrWhiteSpace(isbn))
            return false;

        isbn = isbn.Replace("-", "").Replace(" ", "");

        return isbn.Length switch
        {
            10 => IsValidIsbn10(isbn),
            13 => IsValidIsbn13(isbn),
            _ => false
        };
    }

    private static bool IsValidIsbn10(string isbn)
    {
        if (!isbn.Take(9).All(char.IsDigit))
            return false;

        int sum = 0;

        for (int i = 0; i < 9; i++)
            sum += (isbn[i] - '0') * (10 - i);

        int check = isbn[9] == 'X' ? 10 :
                    char.IsDigit(isbn[9]) ? isbn[9] - '0' : -1;

        if (check == -1)
            return false;

        sum += check;

        return sum % 11 == 0;
    }

    private static bool IsValidIsbn13(string isbn)
    {
        if (!isbn.All(char.IsDigit))
            return false;

        int sum = 0;

        for (int i = 0; i < 12; i++)
        {
            int digit = isbn[i] - '0';
            sum += (i % 2 == 0) ? digit : digit * 3;
        }

        int expectedCheck = (10 - (sum % 10)) % 10;

        return expectedCheck == isbn[12] - '0';
    }
}